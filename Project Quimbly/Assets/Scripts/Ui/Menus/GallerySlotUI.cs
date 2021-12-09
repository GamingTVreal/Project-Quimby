using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectQuimbly.UI.Menus
{
    public class GallerySlotUI : MonoBehaviour
    {
        [SerializeField] Image image = null;
        [SerializeField] Button button = null;
        [SerializeField] Sprite defaultSprite = null;
        [SerializeField] Image imageOverlay = null;

        internal void SetGalleryImage(Sprite newSprite)
        {
            image.sprite = newSprite;
            image.type = Image.Type.Simple;
            image.preserveAspect = true;
            button.enabled = true;
        }

        public void ZoomImage()
        {
            imageOverlay.sprite = image.sprite;
            imageOverlay.gameObject.SetActive(true);
        }

        private void OnDisable() 
        {
            image.sprite = defaultSprite;
            image.type = Image.Type.Sliced;
            image.preserveAspect = false;
            button.enabled = false;
        }
    }
}
