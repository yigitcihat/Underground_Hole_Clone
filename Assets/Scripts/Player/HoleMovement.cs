using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Transform holeFrame;

    [Header("Borders")] [SerializeField] private float maxX = 10f;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxZ = 10f;
    [SerializeField] private float minZ = -10f;

    [Header("Movement")] [SerializeField] private float moveSpeed = 5f;

    [Header("Hole")] [SerializeField] float radius;
    [SerializeField] Transform holeCenter;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    private Mesh mesh;
    private List<int> holeVertices;
    private List<Vector3> offsets;
    private int holeVerticesCount;
    private float x, y;
    private const float ROTATION_SPEED = 500;
    private Vector3 touch, targetPos,originalScale;
    private float currentRadius;
    private void Start()
    {
        Game.isMoving = false;
        Game.isGameover = false;
        holeVertices = new List<int>();
        offsets = new List<Vector3>();
        mesh = meshFilter.mesh;
        FindHoleVertices();
    }

    private void Update()
    {
        var horizontalInput = joystick.Horizontal;
        var verticalInput = joystick.Vertical;
        var moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
       
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            Game.isMoving = true;
        }
        else
        {
            Game.isMoving = false;
        }
        

        if (!Game.isGameover && Game.isMoving)
        {
            MovePlayer(moveDirection);
            UpdateHoleVerticesPosition();
        }
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        Debug.Log("ExpandHole");
        ExpandHole();

    
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        var forward = Camera.main.transform.forward;
        var right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        var desiredMoveDirection = forward * moveDirection.z + right * moveDirection.x;

        var newPos = transform.position + desiredMoveDirection * (moveSpeed * Time.deltaTime);

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);

        holeCenter.position = newPos;

        var targetAngle = Mathf.Atan2(desiredMoveDirection.x, desiredMoveDirection.z) * Mathf.Rad2Deg;
        holeFrame.rotation = Quaternion.RotateTowards(holeFrame.rotation, Quaternion.Euler(0f, targetAngle, 0f),
            ROTATION_SPEED * Time.deltaTime);
    }
  

    private void ExpandHole()
    {
        const float expansionAmount = 0.01f;

        for (var i = 0; i < holeVerticesCount; i++)
        {
            offsets[i] += offsets[i].normalized * expansionAmount;
        }
        holeFrame.localScale += new Vector3(expansionAmount,expansionAmount,expansionAmount);
        UpdateHoleVerticesPosition();
    }
    

    private void UpdateHoleVerticesPosition()
    {
        var vertices = mesh.vertices;
        for (var i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }

        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    private void FindHoleVertices()
    {
        for (var i = 0; i < mesh.vertices.Length; i++)
        {
            var distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);

            if (!(distance < radius)) continue;
            holeVertices.Add(i);
            offsets.Add(mesh.vertices[i] - holeCenter.position);
        }

        holeVerticesCount = holeVertices.Count;
    }


}
       
        