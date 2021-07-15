using ProjectQuimbly.Feeding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingRoom : MonoBehaviour
{
    Texture2D Hand;
    CursorMode Cursor;
    [SerializeField] AudioSource Audio, Audio2;
    [SerializeField] AudioClip[] Songs;
    [SerializeField] AudioClip[] SFX;
    [SerializeField] Animator FeedingRoomAnimator;
    [SerializeField]  SelectedFood Food;
    [SerializeField] Sprite[] BellyLevels;
    [SerializeField] SpriteController SpriteController;
    static int BellyCompacity = 25;
    float spritecheck = 0;
    public CharacterController Character;
    private TextEvents TextEvent;
    public void Jukebox(int SongChoice)
    {
        switch (SongChoice) {
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
    void Start()
    {
        SpriteController.GetBelly();
        Audio.volume = 0.100f;
        Character = FindObjectOfType<CharacterController>();
        TextEvent = FindObjectOfType<TextEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Character.fullness > BellyCompacity)
        {
            if (BellyCompacity >= 100)
            {
                TextEvent.TooFullToContinue();
            }
            else
            {
                TextEvent.TooFullToContinue();
                BellyCompacity += 5;
            }
            
            
        }
    }
    public void GetFullnessSprite()
    {

        if (Food != null)
        {
            spritecheck = spritecheck + Food.GetItem().filling;
            if(spritecheck > 5)
            {
                spritecheck = spritecheck - 5;
                SpriteController.y = SpriteController.y + 1;
                SpriteController.GetBelly();
            }
        }
        
            
        
    }
    // 1 - 10 = gurgles, 11 - 14 = sloshes, 15 - 19 = burps :)
    public void GetSFX(int SFXChance)
    {
        Audio2.PlayOneShot(SFX[Random.Range(15, 19)]);

        if (Character.fullness > 15)
        {
            if (!Audio.isPlaying)
            {
                Audio2.PlayOneShot(SFX[Random.Range(0,10)]);
            }
            
        }
        if (Food.GetItem().isdrink == true)
        {
            Audio2.clip = (SFX[Random.Range(11, 14)]);
            Audio2.Play();
            Audio2.loop = false;
        }
    
        

    }
}
