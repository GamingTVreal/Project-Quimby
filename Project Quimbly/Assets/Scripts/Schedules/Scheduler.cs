using ProjectQuimbly.Saving;
using UnityEngine;

namespace ProjectQuimbly.Schedules
{
    public class Scheduler : MonoBehaviour, ISaveable
    {
        [SerializeField] protected string location = "Park";

        public void ChangeLocation(string newLocation)
        {
            location = newLocation;
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
    }
}
