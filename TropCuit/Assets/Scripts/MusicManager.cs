using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //each state corresponds to a different music track played
    public enum MusicState
    {
        Menu,
        Chasing,
        InGame
    }

    //tracks current and previous states (so we can fade quickly to panic jazz)
    public MusicState musicState, prevMusicState;
    public AudioSource musicSourceMenu;
    public AudioSource musicSourceBGM;
    public AudioSource musicSourceChasing;
    public float targetVolume = 0.5f;
    public ChefScript chefScript;




    // Start is called before the first frame update
    void Start()
    {
        chefScript = GameObject.FindGameObjectWithTag("Chef").GetComponent<ChefScript>();
        musicState = MusicState.InGame;
        prevMusicState = MusicState.InGame;
        musicSourceMenu.GetComponent<AudioSource>();
        musicSourceBGM.GetComponent<AudioSource>();
        musicSourceChasing.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //switch to a different track when caught

        if (chefScript.playerInSight)
        {
            if (musicState == MusicState.InGame)
            {
                musicState = MusicState.Chasing;
            }
        }
        else
        {
            musicState = MusicState.InGame;
        }

        //Turns off any previous tracks and starts playing the new one, basic fade of 2 seconds between
        switch (musicState)
        {
            case MusicState.InGame:
                Debug.Log("In Game!");
                if (musicSourceMenu.isPlaying)
                {
                    FadeOut(musicSourceMenu);
                }
                if (musicSourceChasing.isPlaying)
                {
                    FadeOut(musicSourceChasing);
                }
                if (!musicSourceBGM.isPlaying)
                {
                    musicSourceBGM.Play();
                    Debug.Log("Starting Background Music!");
                    musicSourceBGM.GetComponent<AudioSource>().volume = 0;
                    FadeIn(musicSourceBGM);
                }
                if (musicSourceBGM.isPlaying && musicSourceBGM.volume < 100f)
                {
                    FadeIn(musicSourceBGM);
                    Debug.Log("BG Music Volume is " + musicSourceBGM.volume);
                }
                break;
            case MusicState.Chasing:
                Debug.Log("Being Chased!");
                if (musicSourceMenu.isPlaying)
                {
                    FadeOut(musicSourceMenu);
                }
                if (!musicSourceChasing.isPlaying)
                {
                    musicSourceChasing.Play();
                    Debug.Log("Starting Chase Music!");
                    musicSourceChasing.GetComponent<AudioSource>().volume = 0;
                    FadeIn(musicSourceChasing);
                }
                if (musicSourceBGM.isPlaying)
                {
                    FadeOut(musicSourceBGM);
                }
                if (musicSourceChasing.isPlaying && musicSourceChasing.volume < 50f)
                {
                    FadeIn(musicSourceChasing);
                    Debug.Log("Music Volume is " + musicSourceChasing.volume);
                }
                break;
            case MusicState.Menu:
                if (!musicSourceMenu.isPlaying)
                {
                    musicSourceMenu.Play();
                    musicSourceMenu.GetComponent<AudioSource>().volume = 0;
                    StartFade(musicSourceMenu, 2000, 0.3f);
                }
                if (musicSourceChasing.isPlaying)
                {
                    FadeOut(musicSourceChasing);
                }
                if (musicSourceBGM.isPlaying)
                {
                    FadeOut(musicSourceBGM);
                }
                if (musicSourceMenu.isPlaying && musicSourceMenu.volume < 100f)
                {
                    FadeIn(musicSourceMenu);
                }
                break;
        }

        //stop any music that has been faded out
        if (musicSourceMenu.volume == 0 && musicState != MusicState.Menu)
        {
            musicSourceMenu.Stop();
        }
        if (musicSourceChasing.volume == 0 && musicState != MusicState.Chasing)
        {
            musicSourceChasing.Stop();
        }
        if (musicSourceBGM.volume == 0 && musicState != MusicState.InGame)
        {
            musicSourceBGM.Stop();
        }
    }


    //This fades a source over a specified time to a specified volume
    public void StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        Debug.Log("Fading Volume");
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
        }
    }

    //Fades tracks in and out as appropriate
    public void FadeIn(AudioSource audioSource)
    {
        Debug.Log("Fading In " + audioSource);
        if (audioSource.volume < .3f)
        {
            audioSource.volume += .12f * Time.deltaTime;
        }
    }

    public void FadeOut(AudioSource audioSource)
    {
        if (audioSource.volume > 0f)
        {
            audioSource.volume -= .2f * Time.deltaTime;
        }
    }

}