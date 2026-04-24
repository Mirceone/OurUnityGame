using UnityEngine;

namespace MySoulsProject
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera Singleton;
        public PlayerManager player;
        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        // Change THESE to tweak camera performance
        [Header("Camera Settings")] 
        private float cameraSmoothSpeed = 1; // the bigger the value, the bigger the delay for the camera to actually follow
        [SerializeField] float leftAndRightRotationSpeed = 220;
        [SerializeField] float upAndDownRotationSpeed = 220;
        [SerializeField] float minimumPivot = -50; // lowest angle you can lower camera
        [SerializeField] float maximumPivot = 60;  // !lowest angle you can !lower camera
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask collideWithLayers;
        
        
        // Just displays camera values
        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition; // used for camera collisions (move camera obj to this position upon colliding)
        [SerializeField] private float leftAndRightLookAngle;
        [SerializeField] private float upAndDownLookAngle;
        private float cameraZPosition; // VALUES used for camera collisions
        private float targetCameraZPosition;  // VALUES used for camera collisions
        
        private void Awake()
        {
            if (Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            cameraZPosition = cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                HandleFollowTarget(); // Follow the player
                HandleRotations(); // Rotate around the player
                HandleCollisions(); // Collide with objects
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(
                transform.position, 
                player.transform.position,
                ref cameraVelocity, 
                cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotations()
        {
            // If locked ON, we force rotation towards target
            // Else rotate normally
            
            // Normal Rotation
            // rotate left and right based on horizontal movement
            leftAndRightLookAngle += (PlayerInputManager.Singleton.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            
            // Rotate up and down based on vertical movement
            upAndDownLookAngle -= (PlayerInputManager.Singleton.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            
            // Clamp UP and Down look angle between set pivots(minimum and maximum)
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);
            
            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;
            
            // Rotate this pivot game object Left and Right
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;
            
            // Rotate this pivot game object Up and Down
            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;

        }

        private void HandleCollisions()
        {
            targetCameraZPosition = cameraZPosition;
            RaycastHit hit;
            // DIRECTION for Collision check
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            // we check if there is an obj in front of our desired direction (^ see above ^)
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit,
                    Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                // if obj exists, pass distance from it
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }
            // if ur targetPosition is less than our collisionRadius, we subtract our collisionRadius (making it snap back)
            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }
            
            // we then apply our final position using a Lerp over a time of 0.2f
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }

        
    }
}