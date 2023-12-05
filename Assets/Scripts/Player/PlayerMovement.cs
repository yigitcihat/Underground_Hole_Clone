using UnityEngine;


    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed =5;
        public Joystick joystick;
        private Animator _animator;
        private Rigidbody _rb;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        internal bool isMoving;

        // private void OnEnable()
        // {
        //     EventManager.AddListener(GameEvent.OnSpeedUpgrade, UpgradePlayerSpeed);
        // }
        //
        // private void OnDisable()
        // {
        //     EventManager.RemoveListener(GameEvent.OnSpeedUpgrade, UpgradePlayerSpeed);
        // }

        private void Start()
        {
            moveSpeed = PlayerPrefs.GetFloat("PlayerSpeed", 5);
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var horizontalInput = joystick.Horizontal;
            var verticalInput = joystick.Vertical;

            var moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

            var movement = new Vector3(moveDirection.x * moveSpeed, 0.0f, moveDirection.z * moveSpeed);

            _rb.velocity = movement;

            if (moveDirection != Vector3.zero)
            {
                var newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
                _rb.rotation = Quaternion.Slerp(_rb.rotation, newRotation, Time.deltaTime * 10.0f);

                isMoving = true;
                _animator.SetBool(IsRunning, isMoving);
              

            }
            else
            {
                isMoving = false;
                _animator.SetBool(IsRunning, isMoving);
              
            }
        }

       
        private void UpgradePlayerSpeed()
        {
            moveSpeed *= 1.05f;
            PlayerPrefs.SetFloat("PlayerSpeed", moveSpeed);
            moveSpeed = PlayerPrefs.GetFloat("PlayerSpeed", 10);
        }
    }
