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
    public float targetVolume = 0.4f;
    public float pauseVolume = 0.2f;
    public ChefScript chefScript;

    public PauseMenu pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        chefScript = GameObject.FindGameObjectWithTag("Chef").GetComponent<ChefScript>();
        musicState = MusicState.InGame;
        prevMusicState = MusicState.InGame;
        musicSourceMenu.GetComponent<AudioSource>();
        musicSourceBGM.GetComponent<AudioSource>();
        musicSourceChasing.GetComponent<AudioSource>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        //lower volume when paused
        if (pauseMenu.gamePaused)
        {
            //this check is correct but the methods don't work
            Debug.Log("Paused, Fading Music!");
            FadeLowForPause(musicSourceBGM);
            FadeLowForPause(musicSourceChasing);
            Debug.Log(musicSourceBGM.volume);
        }
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
                    //Debug.Log("Starting Background Music!");
                    musicSourceBGM.GetComponent<AudioSource>().volume = 0;
                    FadeIn(musicSourceBGM);
                }
                if (musicSourceBGM.isPlaying && musicSourceBGM.volume < .2f && !pauseMenu.gamePaused)
                {
                    FadeIn(musicSourceBGM);
                    //Debug.Log("BG Music Volume is " + musicSourceBGM.volume);
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
                    //Debug.Log("Starting Chase Music!");
                    musicSourceChasing.GetComponent<AudioSource>().volume = 0;
                    FadeIn(musicSourceChasing);
                }
                if (musicSourceBGM.isPlaying)
                {
                    FadeOut(musicSourceBGM);
                }
                if (musicSourceChasing.isPlaying && musicSourceChasing.volume < .2f && !pauseMenu.gamePaused)
                {
                    FadeIn(musicSourceChasing);
                    //Debug.Log("Music Volume is " + musicSourceChasing.volume);
                }
                break;
            case MusicState.Menu:
                if (!musicSourceMenu.isPlaying)
                {
                    musicSourceMenu.Play();
                    musicSourceMenu.GetComponent<AudioSource>().volume = 0;
                }
                if (musicSourceChasing.isPlaying)
                {
                    FadeOut(musicSourceChasing);
                }
                if (musicSourceBGM.isPlaying)
                {
                    FadeOut(musicSourceBGM);
                }
                if (musicSourceMenu.isPlaying && musicSourceMenu.volume < .2f && !pauseMenu.gamePaused)
                {
                    FadeIn(musicSourceMenu);
                }
                break;
        }

        //stop main menu and chase music if they're not supposed to play
        if (musicSourceMenu.volume == 0 && musicState != MusicState.Menu)
        {
            musicSourceMenu.Stop();
        }
        if (musicSourceChasing.volume == 0 && musicState != MusicState.Chasing)
        {
            musicSourceChasing.Stop();
        }
    }

    //Fades tracks in and out as appropriate
    public void FadeIn(AudioSource audioSource)
    {
        //Debug.Log("Fading In " + audioSource);
        if (audioSource.volume < .22f)
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

    public void FadeLowForPause(AudioSource audioSource)
    {
        if (audioSource.volume > 0.05f)
        {
            audioSource.volume -= .02f * Time.deltaTime;
        }
    }

}