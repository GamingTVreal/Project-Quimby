using ProjectQuimbly.Saving;
using UnityEngine;

namespace ProjectQuimbly.Schedules
{
    public class Scheduler : MonoBehaviour, ISaveable
    {
        [SerializeField] protected string location = "Park";

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
