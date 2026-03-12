using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace MySoulsProject
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Singleton;
        
        public PlayerManager player;
        
        PlayerControls playerControls;
        
        [Header("Movement Input")]
        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;
        
        [Header("Camera Movement Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

        private void Awake()
        {
            if(Singleton == null)
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

            SceneManager.activeSceneChanged += OnSceneChange;
            
            Singleton.enabled = false;
        }
        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            // IF THE NEW SCENE IS THE WORLD SCENE, ENABLE THE PLAYER INPUT MANAGER
            // SO THE PLAYER CAN MOVE AROUND IN FUTURE SCENES(character creation, selector, etc.)
            if(newScene.buildIndex == WorldSaveGameManager.Singleton.GetWorldSceneIndex())
            {
                Singleton.enabled = true;
            }
            else
            {
                Singleton.enabled = false;
            }
        }

        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        // doar cel focusat preia input, ajuta atat la testing dar si nice to have in general
        private void OnApplicationFocus(bool hasFocus)
        {
            if (enabled)
            {
                if (hasFocus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }

        private void Update()
        {
            HandlePlayerMovementInput();
            HandeleCameraMovementInput();
        }
    
        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            // totally optional  dar am impresia ca si alte SOULS like dau clamp la movement, ca fie 0, 0,5 sau 1
            if(moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if(moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            if (player == null)
                return;
            
            // Dc avem 0 pe horizontalValue?, pentru ca daca nu avem lock-on-target ON,
            // mergem in doar in fata si camera intoarce player ul
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
        }

        private void HandeleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
            
            
        }
    }
}
