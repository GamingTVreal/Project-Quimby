using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GirlSpriteDB", menuName = "Project Quimbly/GirlSpriteDB", order = 0)]
public class GirlSpriteDB : ScriptableObject 
{
    [SerializeField] GirlSprites[] girlSprites;
    Dictionary<string, List<Sprite>> spriteLookup = null;

    public List<string> GetGirlNames()
    {
        BuildLookup();
        List<string> girlList = new List<string>();
        girlList.Add("None");
        foreach (var girlSprite in girlSprites)
        {
            girlList.Add(girlSprite.girlName);
        }
        return girlList;
    }

    public bool GetSprite(string girlName, int index, out Sprite girlSprite)
    {
        BuildLookup();

        List<Sprite> spriteList;
        if(spriteLookup.TryGetValue(girlName, out spriteList))
        {
            if(spriteList.Count > index)
            {
                girlSprite = spriteList[index];
                return true;
            }
        }
        girlSprite = null;
        return false;
    }

    public bool GetAllSpriteNames(string girlName, out string[] girlSpriteNames)
    {
        BuildLookup();

        List<string> girlSpriteArray = new List<string>();
        if(spriteLookup.ContainsKey(girlName))
        {
            foreach (var sprite in spriteLookup[girlName])
            {
                girlSpriteArray.Add(sprite.ToString().Replace(" (UnityEngine.Sprite)", ""));
            }
            girlSpriteNames = girlSpriteArray.ToArray();
            return true;
        }
        girlSpriteNames = null;
        return false;
    }

    private void BuildLookup()
    {
        if(spriteLookup != null) return;

        spriteLookup = new Dictionary<string, List<Sprite>>();

        foreach (var girlSprite in girlSprites)
        {
            spriteLookup[girlSprite.girlName] = girlSprite.girlSprites;
        }
    }
    
    [System.Serializable]
    private class GirlSprites
    {
        public string girlName;
        public List<Sprite> girlSprites;
    }
};