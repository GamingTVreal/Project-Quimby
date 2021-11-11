using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.UI
{
    [CreateAssetMenu(fileName = "CGPhotoDB", menuName = "Project Quimbly/CGPhotoDB", order = 0)]
    public class CGPhotoDB : ScriptableObject
    {
        [SerializeField] BGSprite[] CGs;
        Dictionary<string, Sprite> spriteLookup = null;

        public Sprite GetSprite(string location)
        {
            BuildLookup();
            Sprite bgSprite = null;
            spriteLookup.TryGetValue(location, out bgSprite);
            return bgSprite;
        }
        public List<string> GetCGTitles()
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
            foreach (BGSprite background in CGs)
            {
                spriteLookup[background.CGname] = background.CGsprite;
            }
        }

        [System.Serializable]
        private class BGSprite
        {
            public string CGname;
            public Sprite CGsprite;
        }
    }
}
