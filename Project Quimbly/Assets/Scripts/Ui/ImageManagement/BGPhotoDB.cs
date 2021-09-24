using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.UI
{
    [CreateAssetMenu(fileName = "BGPhotoDB", menuName = "Project Quimbly/BGPhotoDB", order = 0)]
    public class BGPhotoDB : ScriptableObject
    {
        [SerializeField] BGSprite[] bgSprites;
        Dictionary<string, Sprite> spriteLookup = null;

        public Sprite GetSprite(string location)
        {
            BuildLookup();
            Sprite bgSprite = null;
            spriteLookup.TryGetValue(location, out bgSprite);
            return bgSprite;
        }

        public List<string> GetAllLocationNames()
        {
            BuildLookup();

            List<string> locationList = new List<string>();
            locationList.Add("None");
            foreach (string location in spriteLookup.Keys)
            {
                locationList.Add(location);
            }
            return locationList;
        }

        private void BuildLookup()
        {
            // if(spriteLookup != null) return;

            spriteLookup = new Dictionary<string, Sprite>();
            foreach (BGSprite background in bgSprites)
            {
                spriteLookup[background.location] = background.sprite;
            }
        }

        [System.Serializable]
        private class BGSprite
        {
            public string location;
            public Sprite sprite;
        }
    }
}
