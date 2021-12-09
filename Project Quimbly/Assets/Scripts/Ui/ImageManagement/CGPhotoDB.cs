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
        Dictionary<string, bool> cgUnlockLookup = null;

        public Sprite GetSprite(string location)
        {
            BuildLookup();
            Sprite bgSprite = null;
            bool isUnlocked = false;
            spriteLookup.TryGetValue(location, out bgSprite);
            cgUnlockLookup.TryGetValue(location, out isUnlocked);
            if(!isUnlocked)
            {
                cgUnlockLookup[location] = true;
                foreach (BGSprite background in CGs)
                {
                    if(location == background.cgName)
                    {
                        background.isUnlocked = true;
                    }
                }
            }
            return bgSprite;
        }

        public bool IsSpriteUnlocked(string cgName)
        {
            BuildLookup();

            bool isUnlocked = false;
            cgUnlockLookup.TryGetValue(cgName, out isUnlocked);
            return isUnlocked;
        }

        public List<string> GetCGTitles()
        {
            BuildLookup();

            List<string> locationList = new List<string>();
            foreach (string location in spriteLookup.Keys)
            {
                locationList.Add(location);
            }
            return locationList;
        }

        private void BuildLookup()
        {
            if(spriteLookup != null) return;

            spriteLookup = new Dictionary<string, Sprite>();
            cgUnlockLookup = new Dictionary<string, bool>();
            foreach (BGSprite background in CGs)
            {
                spriteLookup[background.cgName] = background.cgSprite;
                cgUnlockLookup[background.cgName] = background.isUnlocked;
            }
        }

        [System.Serializable]
        private class BGSprite
        {
            public string cgName;
            public Sprite cgSprite;
            public bool isUnlocked;
        }
    }
}
