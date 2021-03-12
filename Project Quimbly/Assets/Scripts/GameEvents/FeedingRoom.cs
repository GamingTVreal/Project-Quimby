using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingRoom : MonoBehaviour
{
    Texture2D Hand;
    CursorMode Cursor;
    [SerializeField] AudioSource Audio;
    [SerializeField] AudioClip[] Songs;
    [SerializeField] Animator FeedingRoomAnimator;
    
    public CharacterController Character;
    
    
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
        Audio.volume = 0.100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
