using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Dialogue;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectQuimbly.Inflation
{
    public class GirlInflation : MonoBehaviour
    {
        [SerializeField] Image characterImage;
        [SerializeField] Sprite[] characterAirSprites;
        [SerializeField] Sprite[] characterWaterSprites;

        [SerializeField] AudioClip[] bellyRubVoiceLines;
        [SerializeField] AudioClip[] pressureVoiceLines;

        int currentSprite = 0;
        AIConversant conversant = null;

        private void Start() 
        {
            conversant = GetComponent<AIConversant>();
        }

        public void UpdateFullnessSprite(float fullness, bool isAirPump)
        {
            int spriteLevel = Mathf.FloorToInt(fullness / 5);
            if (spriteLevel > 19)
            {
                spriteLevel = 19;
            }
            if (spriteLevel > currentSprite)
            {
                currentSprite = spriteLevel;
                if (isAirPump)
                {
                    characterImage.sprite = characterAirSprites[spriteLevel];
                }
                else
                {
                    characterImage.sprite = characterWaterSprites[spriteLevel];
                }
            }
        }

        public AudioClip GetBellyRubVoiceLine()
        {
            if(bellyRubVoiceLines != null)
            {
                int lineNum = Random.Range(0, bellyRubVoiceLines.GetUpperBound(0));
                return bellyRubVoiceLines[lineNum];
            }
            return null;
        }

        public AudioClip GetPressureVoiceLine()
        {
            if (pressureVoiceLines != null)
            {
                int lineNum = Random.Range(0, pressureVoiceLines.GetUpperBound(0));
                return pressureVoiceLines[lineNum];
            }
            return null;
        }

        public void StartDialogue(string convoName)
        {
            Debug.Log("Leaving with convo: " + convoName + " and AIConversant is null?: " + (conversant == null).ToString());
            conversant.StartDialogue(convoName);
        }
    }
}
