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
        [SerializeField] float sprintingSpeed = 6.5f;
        [SerializeField] float rotationSpeed = 15;
        [SerializeField] int sprintingStaminaCost = 2;

        [Header("Jump")]
        [SerializeField] float jumpStaminaCost = 25;
        [SerializeField] float jumpHeight = 4;
        [SerializeField] float jumpForwardSpeed = 5;
        [SerializeField] float freeFallSpeed = 2;
        private Vector3 jumpDirection;

        [Header("Dodge")]
        private Vector3 rollDirection;
        [SerializeField] float dodgeStaminaCost = 25;

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
                player.characterNetworkManager.verticalMovement.Value = verticalMovement;
                player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
                player.characterNetworkManager.moveAmount.Value = moveAmount;
            }
            else
            {
                verticalMovement = player.characterNetworkManager.verticalMovement.Value;
                horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
                moveAmount = player.characterNetworkManager.moveAmount.Value;

                //  IF NOT LOCKED ON, PASS MOVE AMOUNT
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);

                //  IF LOCKED ON, PASS HORZ AND VERT
            }
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
            HandleJumpingMovement();
            HandleFreeFallMovement();
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.Singleton.verticalInput;
            horizontalMovement = PlayerInputManager.Singleton.horizontalInput;
            moveAmount = PlayerInputManager.Singleton.moveAmount;
            //  CLAMP THE MOVEMENTS
        }

        private void HandleGroundedMovement()
        {
            if (!player.canMove)
                return;

            //  Walk/run/sprint only when grounded; air control uses HandleJumpingMovement / HandleFreeFallMovement.
            if (!player.isGrounded)
                return;

            GetMovementValues();
            //  OUR MOVE DIRECTION IS BASED ON OUR CAMERAS FACING PERSPECTIVE & OUR MOVEMENT INPUTS
            moveDirection = PlayerCamera.Singleton.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.Singleton.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (player.playerNetworkManager.isSprinting.Value)
            {
                player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else
            {
                if (PlayerInputManager.Singleton.moveAmount > 0.5f)
                {
                    player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
                }
                else if (PlayerInputManager.Singleton.moveAmount <= 0.5f)
                {
                    player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }
        }

        private void HandleJumpingMovement()
        {
            if (player.isJumping)
            {
                player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
            }
        }

        private void HandleFreeFallMovement()
        {
            if (!player.isGrounded)
            {
                Vector3 freeFallDirection;

                freeFallDirection = PlayerCamera.Singleton.transform.forward * PlayerInputManager.Singleton.verticalInput;
                freeFallDirection = freeFallDirection + PlayerCamera.Singleton.transform.right * PlayerInputManager.Singleton.horizontalInput;
                freeFallDirection.y = 0;

                player.characterController.Move(freeFallDirection * freeFallSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            if (!player.canRotate)
                return;

            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.Singleton.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.Singleton.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void HandleSprinting()
        {
            if (player.isPerformingAction)
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }

            if (player.playerNetworkManager.currentStamina.Value <= 0)
            {
                player.playerNetworkManager.isSprinting.Value = false;
                return;
            }

            //  IF WE ARE MOVING, SPRINTING IS TRUE
            if (moveAmount >= 0.5)
            {
                player.playerNetworkManager.isSprinting.Value = true;
            }
            //  IF WE ARE STATIONARY/MOVING SLOWLY SPRINTING IS FALSE
            else
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }

            if (player.playerNetworkManager.isSprinting.Value)
            {
                player.playerNetworkManager.currentStamina.Value -= sprintingStaminaCost * Time.deltaTime;
            }
        }

        public void AttemptToPerformDodge()
        {
            if (player.isPerformingAction)
                return;

            if (player.playerNetworkManager.currentStamina.Value <= 0)
                return;

            //  IF WE ARE MOVING WHEN WE ATTEMPT TO DODGE, WE PERFORM A ROLL
            if (PlayerInputManager.Singleton.moveAmount > 0)
            {
                rollDirection = PlayerCamera.Singleton.cameraObject.transform.forward * PlayerInputManager.Singleton.verticalInput;
                rollDirection += PlayerCamera.Singleton.cameraObject.transform.right * PlayerInputManager.Singleton.horizontalInput;
                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);
            }
            //  IF WE ARE STATIONARY, WE PERFORM A BACKSTEP
            else
            {
                player.playerAnimatorManager.PlayTargetActionAnimation("Back_Step_01", true, true);
            }

            player.playerNetworkManager.currentStamina.Value -= dodgeStaminaCost;
        }

        public void AttemptToPerformJump()
        {
            //  IF WE ARE PERFORMING A GENERAL ACTION, WE DO NOT WANT TO ALLOW A JUMP (WILL CHANGE WHEN COMBAT IS ADDED)
            if (player.isPerformingAction)
                return;

            //  IF WE ARE OUT OF STAMINA, WE DO NOT WISH TO ALLOW A JUMP
            if (player.playerNetworkManager.currentStamina.Value <= 0)
                return;

            //  IF WE ARE ALREADY IN A JUMP, WE DO NOT WANT TO ALLOW A JUMP AGAIN UNTIL THE CURRENT JUMP HAS FINISHED
            if (player.isJumping)
                return;

            //  IF WE ARE NOT GROUNDED, WE DO NOT WANT TO ALLOW A JUMP
            if (!player.isGrounded)
                return;

            //  IF WE ARE TWO HANDING OUR WEAPON, PLAY THE TWO HANDED JUMP ANIMATION, OTHERWISE PLAY THE ONE HANDED ANIMATION ( TO DO )
            //  Allow move/rotate while jump/land plays; grounded movement is still gated on isGrounded above.
            player.playerAnimatorManager.PlayTargetActionAnimation("Main_Jump_01", false, true, true, true);

            player.isJumping = true;

            player.playerNetworkManager.currentStamina.Value -= jumpStaminaCost;

            jumpDirection = PlayerCamera.Singleton.cameraObject.transform.forward * PlayerInputManager.Singleton.verticalInput;
            jumpDirection += PlayerCamera.Singleton.cameraObject.transform.right * PlayerInputManager.Singleton.horizontalInput;
            jumpDirection.y = 0;

            if (jumpDirection != Vector3.zero)
            {
                //  IF WE ARE SPRINTING, JUMP DIRECTION IS AT FULL DISTANCE
                if (player.playerNetworkManager.isSprinting.Value)
                {
                    jumpDirection *= 1;
                }
                //  IF WE ARE RUNNING, JUMP DIRECTION IS AT HALF DISTANCE
                else if (PlayerInputManager.Singleton.moveAmount > 0.5)
                {
                    jumpDirection *= 0.5f;
                }
                //  IF WE ARE WALKING, JUMP DIRECTION IS AT QUARTER DISTANCE
                else if (PlayerInputManager.Singleton.moveAmount <= 0.5)
                {
                    jumpDirection *= 0.25f;
                }
            }
        }

        public void ApplyJumpingVelocity()
        {
            yVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
        }
    }
}
