using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    [SerializeField]private FloatingJoystick joystick;
    [SerializeField]private Transform holeFrame;
    
    [Header("Borders")]
    [SerializeField]private float maxX = 10f; 
    [SerializeField]private float minX = -10f;
    [SerializeField]private float maxZ = 10f; 
    [SerializeField]private float minZ = -10f;
    
    [Header("Movement")]
    [SerializeField]private float moveSpeed = 5f;

    private const float ROTATION_SPEED = 500; 
    private void Update()
    {
        var horizontalInput = joystick.Horizontal;
        var verticalInput = joystick.Vertical;

        var moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        MovePlayer(moveDirection);
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

        transform.position = newPos;
        
        var targetAngle = Mathf.Atan2(desiredMoveDirection.x, desiredMoveDirection.z) * Mathf.Rad2Deg;
        holeFrame.rotation = Quaternion.RotateTowards(holeFrame.rotation, Quaternion.Euler(0f, targetAngle, 0f), ROTATION_SPEED * Time.deltaTime);

    }
}
