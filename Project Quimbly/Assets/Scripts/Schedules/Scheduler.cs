using ProjectQuimbly.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectQuimbly.Schedules
{
    public class Scheduler : MonoBehaviour, ISaveable
    {
        [SerializeField] protected string location = "PhoneMenu";
        protected bool isDefaultLocation = true;

        // Cache
        protected string defaultLocation;
        protected string currentLocation;

        //Placeholder for now until time/date system can be put in place to determine normal locations
        //Default location for everyone is PhoneMenu so they can be pulled in and out of scenes
        private void Awake() 
        {
            //defaultLocation = location; 
        }

        //Change location to any scene except phonemenu
        public void ChangeLocation(string newLocation)
        {
            location = newLocation;
            isDefaultLocation = false;
        }

        //Change Location to the current scene
        public void addToScene()
        {
            string curScene = SceneManager.GetActiveScene().name;
            location = curScene;
            isDefaultLocation = false;
        }

        /// <summary>
        /// Resets character back to the phone menu.
        /// </summary>
        public void ResetLocation()
        {
            location = defaultLocation;
            isDefaultLocation = true;
        }

        public string GetLocation()
        {
            return currentLocation;
        }

        public object CaptureState()
        {
            return currentLocation;
        }

        public void RestoreState(object state)
        {
            location = (string)state;
        }

        [System.Serializable]
        private class ScheduleRecord
        {
            public string location;
            public bool isDefaultLocation;
        }
    }
}
