using ProjectQuimbly.Saving;
using UnityEngine;

namespace ProjectQuimbly.Schedules
{
    public class Scheduler : MonoBehaviour, ISaveable
    {
        [SerializeField] protected string location = "Park";
        protected bool isDefaultLocation = true;

        // Cache
        protected string defaultLocation;

        // Placeholder for now until time/date system can be put in place to determine normal locations
        private void Awake() 
        {
            defaultLocation = location;
        }

        public void ChangeLocation(string newLocation)
        {
            location = newLocation;
            isDefaultLocation = false;
        }

        /// <summary>
        /// Resets character to default location.
        /// </summary>
        public void ResetLocation()
        {
            location = defaultLocation;
            isDefaultLocation = true;
        }
        public void OrderFood()
        {
            location = "Home";
            isDefaultLocation = false;
        }

        public string GetLocation()
        {
            return location;
        }

        public object CaptureState()
        {
            return location;
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
