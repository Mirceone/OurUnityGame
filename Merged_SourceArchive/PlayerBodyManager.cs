using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerBodyManager : MonoBehaviour
    {
        PlayerManager player;

        [Header("Hair Object")]
        [SerializeField] public GameObject hair;
        [SerializeField] public GameObject facialHair;

        [Header("Male")]
        [SerializeField] public GameObject maleObject;      // THE MASTER MALE GAMEOBJECT PARENT
        [SerializeField] public GameObject maleHead;        // DEFAULT HEAD MODEL WHEN UNEQUIPPING ARMOR
        [SerializeField] public GameObject[] maleBody;      // DEFAULT UPPERBODY MODELS WHEN UNEQUIPPING ARMOR (CHEST, UPPER RIGHT ARM, UPPER LEFT ARM)
        [SerializeField] public GameObject[] maleArms;      // DEFAULT UPPERBODY MODELS WHEN UNEQUIPPING ARMOR (LOWER RIGHT ARM, RIGHT HAND, LOWER LEFT ARM, LEFT HAND)
        [SerializeField] public GameObject[] maleLegs;      // DEFAULT UPPERBODY MODELS WHEN UNEQUIPPING ARMOR (HIPS, RIGHT LEG, LEFT LEG)
        [SerializeField] public GameObject maleEyebrows;    // FACIAL FEATURE
        [SerializeField] public GameObject maleFacialHair;  // FACIAL FEATURE

        [Header("Female")]
        [SerializeField] public GameObject femaleObject;
        [SerializeField] public GameObject femaleHead;
        [SerializeField] public GameObject[] femaleBody;
        [SerializeField] public GameObject[] femaleArms;
        [SerializeField] public GameObject[] femaleLegs;
        [SerializeField] public GameObject femaleEyebrows;

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        //  ENABLE BODY FEATURES
        public void EnableHead()
        {
            // ENABLE HEAD OBJECT
            maleHead.SetActive(true);
            femaleHead.SetActive(true);

            // ENABLE ANY FACIAL OBJECTS (EYEBROWS, LIPS, NOSE ECT)
            maleEyebrows.SetActive(true);
            femaleEyebrows.SetActive(true);
        }

        public void DisableHead()
        {
            // DISABLE HEAD OBJECT
            maleHead.SetActive(false);
            femaleHead.SetActive(false);

            // DISABLE ANY FACIAL OBJECTS (EYEBROWS, LIPS, NOSE ECT)
            maleEyebrows.SetActive(false);
            femaleEyebrows.SetActive(false);
        }

        public void EnableHair()
        {
            hair.SetActive(true);
        }

        public void DisableHair()
        {
            hair.SetActive(false);
        }

        public void EnableFacialHair()
        {
            facialHair.SetActive(true);
        }

        public void DisableFacialHair()
        {
            facialHair.SetActive(false);
        }

        public void EnableBody()
        {
            foreach (var model in maleBody)
            {
                model.SetActive(true);
            }

            foreach (var model in femaleBody)
            {
                model.SetActive(true);
            }
        }

        public void EnableArms()
        {
            foreach (var model in maleArms)
            {
                model.SetActive(true);
            }

            foreach (var model in femaleArms)
            {
                model.SetActive(true);
            }
        }

        public void EnableLowerBody()
        {
            foreach (var model in maleLegs)
            {
                model.SetActive(true);
            }

            foreach (var model in femaleLegs)
            {
                model.SetActive(true);
            }
        }

        public void DisableBody()
        {
            foreach (var model in maleBody)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleBody)
            {
                model.SetActive(false);
            }
        }

        public void DisableArms()
        {
            foreach (var model in maleArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleArms)
            {
                model.SetActive(false);
            }
        }

        public void DisableLowerBody()
        {
            foreach (var model in maleLegs)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLegs)
            {
                model.SetActive(false);
            }
        }

        public void ToggleBodyType(bool isMale)
        {
            if (isMale)
            {
                maleObject.SetActive(true);
                femaleObject.SetActive(false);
            }
            else
            {
                maleObject.SetActive(false);
                femaleObject.SetActive(true);
            }

            player.playerEquipmentManager.EquipArmor();
        }
    }
}
