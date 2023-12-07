using System;
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

    private Mesh _mesh;
    private List<int> _holeVertices;
    private List<Vector3> _offsets;
    private int _holeVerticesCount;
    private const float ROTATION_SPEED = 500;
    private Vector3 _startPosition;
    private List<Vector3> _initialVertexPositions;
    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnTransportBottomStage,ResetHolePosition);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnTransportBottomStage,ResetHolePosition);
    }

    private void Start()
    {
        Game.IsMoving = false;
        Game.IsGameOver = false;
        _holeVertices = new List<int>();
        _offsets = new List<Vector3>();
        _mesh = meshFilter.mesh;
        _startPosition = transform.position;
        _initialVertexPositions = new List<Vector3>(_mesh.vertices);
        FindHoleVertices();
    }

    private void Update()
    {
        if (!Game.IsHole ||  !Game.IsGameStart) return;
        var horizontalInput = joystick.Horizontal;
        var verticalInput = joystick.Vertical;
        var moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
       
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            Game.IsMoving = true;
        }
        else
        {
            Game.IsMoving = false;
        }
        

        if (!Game.IsGameOver && Game.IsMoving)
        {
            MovePlayer(moveDirection);
            UpdateHoleVerticesPosition();
        }
        if (!Input.GetKeyDown(KeyCode.Space)) return;
       
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

      
    }
  

    private void ExpandHole()
    {
        const float expansionAmount = 0.01f;
        const float expansionAmountForFrame = 0.1f;

        for (var i = 0; i < _holeVerticesCount; i++)
        {
            _offsets[i] += _offsets[i].normalized * expansionAmount;
        }
        holeFrame.localScale += new Vector3(expansionAmountForFrame,expansionAmountForFrame,expansionAmountForFrame);
        UpdateHoleVerticesPosition();
    }
    

    private void UpdateHoleVerticesPosition()
    {
        var vertices = _mesh.vertices;
        for (var i = 0; i < _holeVerticesCount; i++)
        {
            vertices[_holeVertices[i]] = holeCenter.position + _offsets[i];
        }

        _mesh.vertices = vertices;
        meshFilter.mesh = _mesh;
        meshCollider.sharedMesh = _mesh;
    }

    private void FindHoleVertices()
    {
        for (var i = 0; i < _mesh.vertices.Length; i++)
        {
            var distance = Vector3.Distance(holeCenter.position, _mesh.vertices[i]);

            if (!(distance < radius)) continue;
            _holeVertices.Add(i);
            _offsets.Add(_mesh.vertices[i] - holeCenter.position);
        }

        _holeVerticesCount = _holeVertices.Count;
    }
    private void ResetHolePosition()
    {
        holeCenter.position = _startPosition;

        for (var i = 0; i < _holeVerticesCount; i++)
        {
            _offsets[i] = _initialVertexPositions[_holeVertices[i]] - holeCenter.position;
        }

        _mesh.vertices = _mesh.vertices; // Unity'ye güncelleme bildir

        UpdateHoleVerticesPosition();
    }

}
       
        