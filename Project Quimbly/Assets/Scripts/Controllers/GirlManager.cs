using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Saving;
using ProjectQuimbly.Schedules;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectQuimbly.Controllers
{
    public class GirlManager : MonoBehaviour, ISaveable
    {
        [SerializeField] CharacterDB characterDB = null;
        Dictionary<string, CharacterRecord> characterLookup = null;
        List<SaveableClone> saveables = null;
        int charactersInScene = 0;

        private void Awake() 
        {
            BuildLookup();
            // SpawnCharacters();
        }

        private void SpawnCharacters()
        {
            // Check scene location against character location
            string curScene = SceneManager.GetActiveScene().name;

            saveables = new List<SaveableClone>();

            foreach (string girl in characterLookup.Keys)
            {
                // Get schedule from save
                string scheduleName = "ProjectQuimbly.Schedules." + girl + "Schedule";
                string girlLocation;
                Dictionary<string, object> stateDict = (Dictionary<string, object>)characterLookup[girl].state;
                if(stateDict == null)
                {
                    Scheduler defaultScheduler = characterDB.GetBasePrefab(girl).GetComponent<Scheduler>();
                    girlLocation = defaultScheduler.GetLocation();
                }
                else
                {
                    Scheduler scheduler = new Scheduler();
                    scheduler.RestoreState(stateDict[scheduleName]);
                    girlLocation = scheduler.GetLocation();
                }
                
                if (curScene == girlLocation)
                {
                    // Spawn from prefab
                    GameObject characterGO = null;
                    switch (curScene)
                    {
                        case "Dates":
                            characterGO = characterDB.GetDatePrefab(girl);
                            break;
                        case "Feeding Minigame":
                            characterGO = characterDB.GetFeedingPrefab(girl);
                            break;
                        case "Inflation Minigame":
                            characterGO = characterDB.GetInflationPrefab(girl);
                            break;
                        case "INF_Dates":
                            characterGO = characterDB.GetInfDatePrefab(girl);
                            break;
                        default:
                            characterGO = characterDB.GetBasePrefab(girl);
                            break;
                    } 
                    GameObject charInstance = Instantiate(characterGO, transform);

                    // Offset position if multiple
                    if(charactersInScene > 0)
                    {
                        Vector3 charPosition = charInstance.transform.GetChild(0).position;
                        int xOffset = -590 * charactersInScene;
                        charPosition += new Vector3(xOffset, 0, 0);
                        charInstance.transform.GetChild(0).position = charPosition;
                    }

                    // Restore saved variables
                    SaveableClone saveableClone = charInstance.GetComponent<SaveableClone>();
                    saveables.Add(saveableClone);
                    if(characterLookup[girl].state != null)
                    {
                        saveableClone.RestoreState(characterLookup[girl].state);
                    }

                    charactersInScene++;
                }
            }
        }

        private void BuildLookup()
        {
            if(characterLookup != null) return;

            characterLookup = new Dictionary<string, CharacterRecord>();
            string[] characters = characterDB.GetNames();
            foreach (string character in characters)
            {
                GameObject characterGO = characterDB.GetBasePrefab(character);
                if(characterGO != null)
                {
                    CharacterRecord record = new CharacterRecord();
                    record.identifier = characterGO.GetComponent<SaveableClone>().GetUniqueIdentifier();
                    characterLookup[character] = record;
                }
            }
        }

        // Modify saved state to change character location
        public void ChangeInactiveCharacterLocation(string character, string newLocation)
        {
            if(!characterLookup.ContainsKey(character)) return;
            
            // Get schedule from save
            string scheduleName = "ProjectQuimbly.Schedules." + character + "Schedule";

            Scheduler scheduler = new Scheduler();
            Dictionary<string, object> stateDict = (Dictionary<string, object>)characterLookup[character].state;
            if (stateDict == null)
            {
                scheduler = characterDB.GetBasePrefab(character).GetComponent<Scheduler>();
                stateDict = new Dictionary<string, object>();
            }
            else
            {
                scheduler.RestoreState(stateDict[scheduleName]);
            }

            scheduler.ChangeLocation(newLocation);
            stateDict[scheduleName] = scheduler.CaptureState();
        }

        public object CaptureState()
        {
            BuildLookup();

            if(saveables != null)
            {
                foreach (SaveableClone saveableClone in saveables)
                {
                    characterLookup[saveableClone.GetUniqueIdentifier()].state = saveableClone.CaptureState();
                }
            }
            
            return characterLookup;
        }

        public void RestoreState(object state)
        {
            characterLookup = (Dictionary<string, CharacterRecord>)state;
            SpawnCharacters();
        }

        [System.Serializable]
        private class CharacterRecord
        {
            public string identifier;
            public object state;
        }
    }
}
