using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aref.Com.GameJam
{
    public class PlayerMovingAround : MonoBehaviour
    {
        //--------------------------------variables & references-----------------------------------------
        #region variables and references:

        [Header("VARIABLES AND REFERENCES")]

        [SerializeField] private string _horizontalInputName, _verticalInputName;
        [SerializeField] private KeyCode _runKey;
        [SerializeField] private KeyCode _jumpKey;

        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _runSpeedMultiplier;
        [SerializeField] private float _jumpMultiplier;
        [SerializeField] private float _slopeForce, _slopeForceRayLenght;

        [SerializeField] private AnimationCurve _jumpFallOff;

        [Header("BOOLS")]

        [SerializeField] private bool _isRunning;
        [SerializeField] private bool _isJumping;
        [SerializeField] private bool _carryingHeavy = false;

        public bool CarryingHeavy
        {
            get => _carryingHeavy;
            set => _carryingHeavy = value;
        }

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }

        private CharacterController _playerController;

        #endregion
        //--------------------------------main functions-------------------------------------------------
        #region main functions:

        private void Awake()
        {
            _playerController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            PlayerMovement();
        }

        #endregion
        //--------------------------------private functions-------------------------------------------------
        #region private functions:

        private void PlayerMovement()
        {
            float t_horizontalInput = Input.GetAxis(_horizontalInputName);
            float t_verticalInput = Input.GetAxis(_verticalInputName);
            float t_movementSpeed = _movementSpeed;

            _isRunning = Input.GetKey(_runKey) && _playerController.isGrounded;

            if (_isRunning) t_movementSpeed *= _runSpeedMultiplier;

            Vector3 t_forwardMovement = transform.forward * t_verticalInput;
            Vector3 t_rightMovement = transform.right * t_horizontalInput;

            _playerController.SimpleMove(Vector3.ClampMagnitude(t_forwardMovement + t_rightMovement, 1.0f) * t_movementSpeed);

            if ((t_verticalInput != 0 || t_horizontalInput != 0) && OnSlope())
                _playerController.Move(Vector3.down * _playerController.height / 2 * _slopeForce * Time.deltaTime);

            JumpInput();
        }

        private bool OnSlope()
        {
            if (_isJumping) return false;

            RaycastHit t_hit;

            if (Physics.Raycast(transform.position, Vector3.down, out t_hit, _playerController.height / 2 * _slopeForceRayLenght))
                if (t_hit.normal != Vector3.up)
                    return true;
            return false;
        }

        private void JumpInput()
        {
            if (Input.GetKeyDown(_jumpKey) && !_isJumping && !_carryingHeavy)
            {
                _isJumping = true;
                StartCoroutine(JumpEvent());
            }
        }

        private IEnumerator JumpEvent()
        {
            _playerController.slopeLimit = 90.0f;
            float t_timeInAir = 0.0f;

            do
            {
                float t_jumpForce = _jumpFallOff.Evaluate(t_timeInAir);
                _playerController.Move(Vector3.up * t_jumpForce * _jumpMultiplier * Time.deltaTime);
                t_timeInAir += Time.deltaTime;
                yield return null;
            } while (!_playerController.isGrounded && _playerController.collisionFlags != CollisionFlags.Above);

            _playerController.slopeLimit = 45.0f;
            _isJumping = false;
        }
        #endregion
    }
}