using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Saving;
using ProjectQuimbly.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectQuimbly.UI.Menus
{
    public class SaveSlotUI : MonoBehaviour
    {
        [SerializeField] SaveSlotDB saveSlotDB;
        [SerializeField] ChangeSceneButton sceneChangeObj;
        // [SerializeField] Image icon = null;
        [SerializeField] TextMeshProUGUI slotText;
        [SerializeField] TextMeshProUGUI playerName;
        [SerializeField] TextMeshProUGUI locationText;
        [SerializeField] TextMeshProUGUI moneyText;
        [SerializeField] TextMeshProUGUI energyText;
        [SerializeField] Image energyIcon;
        
        SaveMenu saveMenu;
        Button button;
        
        int saveSlot;
        string slotName = "Save Slot ";
        string saveFile = "save";
        bool isSaving = false;
        string locationToLoad;
        int sceneToLoad = -1;

        private void Awake() 
        {
            button = GetComponent<Button>();
            saveSlot = transform.GetSiblingIndex();
            if(saveSlot == 0)
            {
                slotName = "Auto Save";
                saveFile = "auto";
            }
            else
            {
                slotName += saveSlot;
                saveFile += saveSlot;
            }
            slotText.text = slotName;
            saveMenu = GetComponentInParent<SaveMenu>();
        }

        private void OnEnable()
        {
            Dictionary<string, string> infoLookup = saveSlotDB.GetSaveRecord(saveFile);
            if(infoLookup != null)
            {
                locationToLoad = infoLookup["location"];
                playerName.text = infoLookup["name"];
                locationText.text = locationToLoad;
                moneyText.text = "$" + infoLookup["money"];
                energyText.text = infoLookup["energy"];
                energyIcon.enabled = true;
                sceneToLoad = int.Parse(infoLookup["scene"]);
            }
            else
            {
                playerName.text = "";
                moneyText.text = "";
                energyText.text = "";
                energyIcon.enabled = false;
                locationText.text = "";
                sceneToLoad = -1;
            }
            button.onClick.RemoveAllListeners();
            SetSlotFunction();
        }

        public void SetSlotFunction()
        {
            isSaving = saveMenu.GetComponent<SaveMenu>().IsSaving();
            if (isSaving)
            {
                button.onClick.AddListener(() => Save());
            }
            else
            {
                button.onClick.AddListener(() => Load());
            }
        }

        private void Save()
        {
            SavingWrapper savingWrapper = (SavingWrapper)GameObject.FindObjectOfType(typeof(SavingWrapper));
            if (savingWrapper != null)
            {
                // saveSlotDB.AddSaveRecord(saveFile);
                savingWrapper.Save(saveFile);
            }
            saveMenu.CloseMenu();
        }

        private void Load()
        {
            if(sceneToLoad < 0)
            {
                saveMenu.CloseMenu();
                return;
            }
            sceneChangeObj.SetDestination(locationToLoad);
            sceneChangeObj.SetSceneToLoad(sceneToLoad);
            sceneChangeObj.ChangeScene(saveFile);
        }
    }
}
