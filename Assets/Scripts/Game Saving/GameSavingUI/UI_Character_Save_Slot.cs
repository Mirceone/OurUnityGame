using TMPro;
using UnityEngine;

namespace MySoulsProject
{
    public class UI_Character_Save_Slot : MonoBehaviour
    {
        SaveFileDataWriter saveFileWriter;

        [Header("Game Slot")]
        public CharacterSlot characterSlot;

        [Header("Character Info")]
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timedPlayed;

        private void OnEnable()
        {
            LoadSaveSlots();
        }

        private void LoadSaveSlots()
        {
            saveFileWriter = new SaveFileDataWriter();
            saveFileWriter.saveDataDirectoryPath = Application.persistentDataPath;

            //  SAVE SLOT 01
            if (characterSlot == CharacterSlot.CharacterSlot_01)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot01.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 02
            else if (characterSlot == CharacterSlot.CharacterSlot_02)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot02.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 03
            else if (characterSlot == CharacterSlot.CharacterSlot_03)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot03.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 04
            else if (characterSlot == CharacterSlot.CharacterSlot_04)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot04.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 05
            else if (characterSlot == CharacterSlot.CharacterSlot_05)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot05.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 06
            else if (characterSlot == CharacterSlot.CharacterSlot_06)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot06.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 07
            else if (characterSlot == CharacterSlot.CharacterSlot_07)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot07.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 08
            else if (characterSlot == CharacterSlot.CharacterSlot_08)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot08.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 09
            else if (characterSlot == CharacterSlot.CharacterSlot_09)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot09.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //  SAVE SLOT 10
            else if (characterSlot == CharacterSlot.CharacterSlot_10)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.Singleton.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                //  IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Singleton.characterSlot10.characterName;
                }
                //  IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void LoadGameFromCharacterSlot()
        {
            WorldSaveGameManager.Singleton.currentCharacterSlotBeingUsed = characterSlot;
            WorldSaveGameManager.Singleton.LoadGame();
        }

        public void SelectCurrentSlot()
        {
            TitleScreenManager.Singleton.SelectCharacterSlot(characterSlot);
        }
    }
}
