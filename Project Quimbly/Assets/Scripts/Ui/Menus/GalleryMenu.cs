using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.UI.Menus
{
    public class GalleryMenu : MonoBehaviour
    {
        [SerializeField] CGPhotoDB photoDB = null;
        [SerializeField] Transform galleryContainer = null;
        List<bool> isUnlockedList = new List<bool>();

        private void OnEnable() 
        {
            PopulateGalleryImages();
        }

        private void PopulateGalleryImages()
        {
            int i = 0;
            foreach (var cgName in photoDB.GetCGTitles())
            {
                // Skip first title, it is empty.
                if(i == 0)
                {
                    i++;
                    continue;
                }
                GameObject childGO = galleryContainer.GetChild(i).gameObject;
                childGO.SetActive(true);
                if(photoDB.IsSpriteUnlocked(cgName))
                {
                    childGO.GetComponent<GallerySlotUI>().SetGalleryImage(photoDB.GetSprite(cgName));
                }
                i++;
            }
        }
    }
}
