using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Test Action")]
    public class WeaponItemAction : ScriptableObject
    {
        public int actionID;

        public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.IsOwner)
            {
                playerPerformingAction.playerNetworkManager.currentWeaponBeingUsed.Value = weaponPerformingAction.itemID;
            }

            //  AFTER YOU PASS ALL THE CHECKS AS THE OWNER, IF YOU ARE THE OWNER SEND THIS RPC IF NEEDED
            //  YOU ONLY NEED TO DO THIS IF YOU HAVE SPECIAL LOGIC THAT REQUIRES THE WEAPON ACTION BE PERFORMED ON THE NETWORK
            //  (IF YOU ARE FOLLOWING THE TUTORIAL WAY OF DOING THINGS THIS IS NOT NEEDED)
            //player.playerNetworkManager.NotifyTheServerOfWeaponActionServerRpc(NetworkManager.Singleton.LocalClientId, weaponAction.actionID, weaponPerformingAction.itemID);
        }
    }
}
