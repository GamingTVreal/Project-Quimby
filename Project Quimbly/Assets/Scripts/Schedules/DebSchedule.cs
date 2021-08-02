using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Saving;
using UnityEngine;

namespace ProjectQuimbly.Schedules
{
    public class DebSchedule : MonoBehaviour, IScheduler, ISaveable
    {
        [SerializeField] string location = "Park";

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
