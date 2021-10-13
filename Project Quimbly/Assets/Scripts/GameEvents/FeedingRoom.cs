using ProjectQuimbly.Feeding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeedingRoom : MonoBehaviour
{
    Texture2D Hand;
    CursorMode Cursor;
    [SerializeField] AudioSource Audio, Audio2;
    [SerializeField] AudioClip[] Songs;
    [SerializeField] AudioClip[] SFX;
    [SerializeField] Animator FeedingRoomAnimator;
    [SerializeField] PlateSelector plate;
    // [SerializeField] Sprite[] BellyLevels;
    // [SerializeField] SpriteController SpriteController;
    // static int BellyCompacity = 25;
    // float spritecheck = 0;
    // public CharacterController Character;
    // private TextEvents TextEvent;
    [SerializeField] TextMeshProUGUI fullnessText;
    GirlFeeding feedingScript = null;

    private void Start() 
    {
        feedingScript = GameObject.FindWithTag("GirlContainer").GetComponentInChildren<GirlFeeding>();
        // SpriteController.GetBelly();
        Audio.volume = 0.100f;
        // Character = FindObjectOfType<CharacterController>();
        // TextEvent = FindObjectOfType<TextEvents>();
    }

    // From Leave button onClick
    public void LeaveFeedingRoom()
    {
        feedingScript.TryLeaving();
    }

    // From MouthTrigger event
    public void ConsumeItem()
    {
        plate.OnItemConsumeEvent();
    }

    // From MouthTrigger event
    public void UpdateFullnessText()
    {
        float fullness = feedingScript.GetFullness();
        fullnessText.text = fullness.ToString("f2");
    }

    // From MouthTrigger event
    public void PlaySFX()
    {
        GetSFX(0);
    }

    // 1 - 10 = gurgles, 11 - 14 = sloshes, 15 - 19 = burps :)
    public void GetSFX(int SFXChance)
    {
        Audio2.PlayOneShot(SFX[Random.Range(15, 19)]);

        if(feedingScript.GetFullness() > 15)
        {
            if (!Audio.isPlaying)
            {
                Audio2.PlayOneShot(SFX[Random.Range(0,10)]);
            }
            
        }
        if (plate.GetSelectedItem().isdrink == true)
        {
            Audio2.clip = (SFX[Random.Range(11, 14)]);
            Audio2.Play();
            Audio2.loop = false;
        }
    }

    public void Jukebox(int SongChoice)
    {
        switch (SongChoice)
        {
            case 0:
                Audio.Stop();
                Audio.clip = Songs[0];
                Audio.Play();
                break;
            case 1:
                Audio.Stop();
                Audio.clip = Songs[1];
                Audio.Play();
                break;

            case 2:
                Audio.Stop();
                Audio.clip = Songs[2];
                Audio.Play();
                break;
            case 3:
                Audio.Stop();
                break;
        }
    }
    public void SecretJukebpx(int SongChoice)
    {
        switch (SongChoice)
        {
            case 0:
                Audio.Stop();
                Audio.clip = Songs[3];
                Audio.Play();
                break;
            case 1:
                Audio.Stop();
                Audio.clip = Songs[4];
                Audio.Play();
                break;

            case 2:
                Audio.Stop();
                Audio.clip = Songs[5];
                Audio.Play();
                break;
            case 3:
                Audio.Stop();
                break;
        }
    }

    // public void DebugIncreaseCapacity()
    // {
    //     BellyCompacity = 100;
    // }

    // public void DebugIncreaseFullness()
    // {
    //     Character.fullness += 5;
    //     GetFullnessSprite(5.01f);
    // }
}
