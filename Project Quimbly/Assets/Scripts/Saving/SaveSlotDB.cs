using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectQuimbly.Saving
{
    [CreateAssetMenu(fileName = "SaveSlotDB", menuName = "Stats/SaveSlotDB")]
    public class SaveSlotDB : ScriptableObject
    {
        Dictionary<string, SaveRecord> saveLookup = null;

        [System.Serializable]
        class SaveRecord
        {
            public string playerName;
            public string money;
            public string energy;
            public string location;
            public string scene;
        }

        public Dictionary<string, string> GetSaveRecord(string saveFile)
        {
            if (saveLookup == null)
            {
                BuildLookup();
            }

            if(!saveLookup.ContainsKey(saveFile))
            {
                return null;
            }

            Dictionary<string, string> saveInfo = new Dictionary<string, string>();
            saveInfo["name"] = saveLookup[saveFile].playerName;
            saveInfo["money"] = saveLookup[saveFile].money;
            saveInfo["energy"] = saveLookup[saveFile].energy;
            saveInfo["location"] = saveLookup[saveFile].location;
            saveInfo["scene"] = saveLookup[saveFile].scene;

            return saveInfo;
        }

        public void AddSaveRecord(string saveFile)
        {
            if(saveLookup == null)
            {
                BuildLookup();
            }

            ISlotInfo slotInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ISlotInfo>();
            var state = (Dictionary<string, string>)slotInfo.CaptureState();

            SaveRecord newRecord = new SaveRecord();

            newRecord.playerName = state["name"];
            newRecord.money = state["money"];
            newRecord.energy = state["energy"];
            newRecord.location = state["location"];
            newRecord.scene = state["scene"];

            saveLookup[saveFile] = newRecord;
        }

        private void BuildLookup()
        {
            saveLookup = new Dictionary<string, SaveRecord>();

            List<string> filePaths = Directory.GetFiles(Application.persistentDataPath, "*.dat").ToList();
            // List<string> saveFiles = new List<string>();
            // saveFiles.Add("auto");
            // for (int i = 1; i < 20; i++)
            // {
            //     if(PlayerPrefs.HasKey("save" + i))
            //     {
            //         saveFiles.Add("save" + i);
            //     }
            // }
            foreach (var filePath in filePaths)
            {
                // if(!PlayerPrefs.HasKey(filePath))
                // {
                //     continue;
                // }

                string fileName = filePath.Replace(Application.persistentDataPath + Path.DirectorySeparatorChar, "");
                fileName = fileName.Replace(".dat", "");

                // string saveString = PlayerPrefs.GetString(saveFile);
                // var stateDict = JsonUtility.FromJson<Dictionary<string, object>>(saveString);
                var stateDict = new Dictionary<string, object>();
                using (FileStream stream = File.Open(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    stateDict = (Dictionary<string, object>)formatter.Deserialize(stream);
                }
                
                stateDict = (Dictionary<string, object>)stateDict["ScriptController"];
                var state = (Dictionary<string, string>)stateDict["BasicFunctions"];
                SaveRecord saveRecord = new SaveRecord();

                saveRecord.playerName = state["name"];
                saveRecord.money = state["money"];
                saveRecord.energy = state["energy"];
                saveRecord.location = state["location"];
                saveRecord.scene = state["scene"];

                saveLookup[fileName] = saveRecord;
            }
        }
    }
}
