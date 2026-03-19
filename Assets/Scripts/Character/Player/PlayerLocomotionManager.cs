using MyNamespace;
using Unity.VisualScripting;
using UnityEngine;

namespace MySoulsProject
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;
        
        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 5;
        [SerializeField] float rotationSpeed = 15;
        
        [Header("Dodge Settings")]
        private Vector3 rollDirection;
        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();

            if (player.IsOwner)
            {
                player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
                player.characterNetworkManager.verticalMovement.Value = verticalMovement;
                player.characterNetworkManager.moveAmount.Value = moveAmount;
            }
            else
            {
                horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
                verticalMovement = player.characterNetworkManager.verticalMovement.Value;
                moveAmount = player.characterNetworkManager.moveAmount.Value;
                
                // din nou, inca nu avem lock-on-target, deci horizontalValue = 0
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
            }
        }
        
        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
            // aerial movement
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.Singleton.verticalInput;
            horizontalMovement = PlayerInputManager.Singleton.horizontalInput;
            moveAmount = PlayerInputManager.Singleton.moveAmount;
            
            // CLAMP THE MOVEMENT, in the future
        }

        private void HandleGroundedMovement()
        {
            GetMovementValues();
            // movement based on camera perspective + our inputs
            var cam = PlayerCamera.Singleton.transform;

            moveDirection = cam.forward * verticalMovement;
            moveDirection += cam.right * horizontalMovement;

            moveDirection.y = 0f;
            if (moveDirection.sqrMagnitude > 0f)
                moveDirection.Normalize();

            float speed = (PlayerInputManager.Singleton.moveAmount > 0.5f) ? runningSpeed : walkingSpeed;

            Vector3 motion = moveDirection * speed;
            motion *= Time.deltaTime;

            player.characterController.Move(motion);
            
            // moveDirection = PlayerCamera.Singleton.transform.forward * verticalMovement;
            // moveDirection = moveDirection + PlayerCamera.Singleton.transform.right * horizontalMovement;
            //
            // moveDirection.Normalize();
            // moveDirection.y = 0;
            //
            // if (PlayerInputManager.Singleton.moveAmount > 0.5f)
            // {
            //     player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            // }
            // else if (PlayerInputManager.Singleton.moveAmount <= 0.5f)
            // {
            //     player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            // }
        }

        private void HandleRotation()
        {
            if (PlayerInputManager.Singleton.moveAmount <= 0f) return;
            
            targetRotationDirection = Vector3.zero;

            var camT = PlayerCamera.Singleton.cameraObject.transform;

            targetRotationDirection = camT.forward * verticalMovement;
            targetRotationDirection += camT.right * horizontalMovement;

            targetRotationDirection.y = 0f;

            if (targetRotationDirection.sqrMagnitude > 0f)
                targetRotationDirection.Normalize();
            else
                targetRotationDirection = transform.forward;

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        public void AttemptToPerformDodge()
        {
            if (PlayerInputManager.Singleton.moveAmount > 0)
            {
                // Performing a roll, because we are not stationary
                rollDirection = PlayerCamera.Singleton.cameraObject.transform.forward * PlayerInputManager.Singleton.verticalInput;
                rollDirection +=  PlayerCamera.Singleton.cameraObject.transform.right * PlayerInputManager.Singleton.horizontalInput;
           
                rollDirection.y = 0;
                rollDirection.Normalize();
           
                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;
            }
            else
            {
                // Performing a backstep, because we are stationary
                
            }
        }

    }
}
