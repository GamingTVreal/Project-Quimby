using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.UI.Dragging
{
    /// <summary>
    /// Components that implement this interfaces can act as the destination for
    /// dragging a `DragItem`.
    /// </summary>
    public interface IDragDestination
    {
        /// <summary>
        /// How much of the given item can be accepted.
        /// </summary>
        /// <returns>Based on NPC, max return value should be 1.</returns>
        float MaxAcceptable();
    }
}
