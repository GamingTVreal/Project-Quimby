using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectQuimbly.UI.Dragging
{
    // Uses Unity's EventSystem to drag a UI element.
    // During dragging, the item is reparented to the parent canvas.
    public class DragItem<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
        where T : class
    {
        // state
        Vector3 startPosition;
        Vector2 offset;
        private PointerEventData _lastPointerData;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
                startPosition = transform.position;
                _lastPointerData = eventData;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(_lastPointerData != null)
            {
                transform.position = eventData.position - offset;
                return;
            }

            CancelDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _lastPointerData = null;
            transform.position = startPosition;
        }

        public void CancelDrag()
        {
            if (_lastPointerData != null)
            {
                _lastPointerData.pointerDrag = null;
                transform.position = startPosition;
            }
        }
    }
}
