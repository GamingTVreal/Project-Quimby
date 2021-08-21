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
                GameObject characterGO = characterDB.GetPrefab(girl);
                if (curScene == characterGO.GetComponent<Scheduler>().GetLocation())
                {
                    // Spawn from prefab
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
                GameObject characterGO = characterDB.GetPrefab(character);
                if(characterGO != null)
                {
                    CharacterRecord record = new CharacterRecord();
                    record.identifier = characterGO.GetComponent<SaveableClone>().GetUniqueIdentifier();
                    characterLookup[character] = record;
                }
            }
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
