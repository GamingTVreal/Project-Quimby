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
    [SerializeField] AudioClip[] SFX,EatingSFX;
    public AudioClip[] BellySlaps;
    public AudioSource source;
    [SerializeField] Animator FeedingRoomAnimator, CameraAnimator;
    [SerializeField] PlateSelector plate;
    [SerializeField] SelectedFood FoodItem;
    [SerializeField] Item CurrentItem;
    // [SerializeField] Sprite[] BellyLevels;
    // [SerializeField] SpriteController SpriteController;
    // static int BellyCompacity = 25;
    // float spritecheck = 0;
    // public CharacterController Character;
    // private TextEvents TextEvent;
    [SerializeField] TextMeshProUGUI fullnessText;
    GirlFeeding feedingScript = null;
    // Used for cursor change
    [SerializeField] LayerMask grabbableLayers;

    private void Start() 
    {
        feedingScript = GameObject.FindWithTag("GirlContainer").GetComponentInChildren<GirlFeeding>();
        if(feedingScript != null)
        {
            feedingScript.OnItemCombo += ItemCombinationEvent;
        }
        // SpriteController.GetBelly();
        Audio.volume = 0.100f;
        // Character = FindObjectOfType<CharacterController>();
        // TextEvent = FindObjectOfType<TextEvents>();
    }

    private void Update()
    {
        checkBellySlap();
    }

    private void checkBellySlap()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, grabbableLayers);
        Debug.Log(hit.collider);
        if (hit.collider != null && hit.collider.name == "BellySlapArea")
        {
            Debug.Log("HitDaBelly");
            source.PlayOneShot(BellySlaps[Random.Range(0, 5)]);
        }
    }

    // From Leave button onClick
    public void LeaveFeedingRoom()
    {
        feedingScript.TryLeaving();
    }

    // From MouthTrigger event
    public void ConsumeItem()
    {
        CurrentItem = FoodItem.GetItem();
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
        GetSFX(0,0);
    }

    // From GirlFeeding item combination check
    public void ItemCombinationEvent()
    {
        StartCoroutine(CombinationSFX());
    }

    // Pause music for sfx
    private IEnumerator CombinationSFX()
    {
        yield return null;

        Audio.Pause();
        Audio2.Stop();

        //Insert burp sfx selection here
        Audio2.PlayOneShot(SFX[21]);
        yield return new WaitForSeconds(1f);
        CameraAnimator.SetBool("ScreenShake", true);
        Audio2.PlayOneShot(SFX[20]);
        yield return new WaitForSeconds(2f);
        CameraAnimator.SetBool("ScreenShake", false);
        Audio.UnPause();
    }

    // EatingSFX 0-2 drinking sfx 3-5 eating sfx
    // 1 - 10 = gurgles, 11 - 14 = sloshes, 15 - 19 = burps :)
    public void GetSFX(int SFXChance,int GurgleChance)
    {

        if(CurrentItem.isdrink == false)
        {
            Audio2.PlayOneShot(EatingSFX[Random.Range(3, 5)]);
        }
        else if (CurrentItem.isdrink == true)
        {
            Audio2.PlayOneShot(EatingSFX[Random.Range(0, 2)]);
        }
        //Audio2.PlayOneShot(EatingSFX[Random.Range(15, 19)]);

        if(feedingScript.GetFullness() > 15)
        {
            if (!Audio.isPlaying)
            {
                SFXChance = Random.Range(1, 5);
                GurgleChance = Random.Range(1, 5);
               //Debug.Log("GurgleChance = " + GurgleChance + "BurpChance =" + SFXChance);
                if (SFXChance == 1)
                {
                    Audio2.PlayOneShot(SFX[Random.Range(15, 19)]);
                }
                if(GurgleChance == 1)
                {
                    Audio2.PlayOneShot(SFX[Random.Range(0, 10)]);
                }
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
