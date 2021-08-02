using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Saving
{
    public interface ISlotInfo
    {
        object CaptureState();
    }
}
