using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;

namespace MySoulsProject
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        protected override void Awake()
        {
            base.Awake();

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        }

        protected override void Update()
        {
            base.Update();
            
            // if dont own gameobject, dont control, edit, etc.
            if (!IsOwner)
            {
                return;
            }

            playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            if(!IsOwner)
                return;
            
            base.LateUpdate();
            
            PlayerCamera.Singleton.HandleAllCameraActions();
            
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            // isOwner, adica daca player-ul este al clientului, al jucatorului.
            if (IsOwner)
            {
                PlayerCamera.Singleton.player = this;
                PlayerInputManager.Singleton.player = this;
            }
        }
    }

}