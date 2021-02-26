using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator canvasAnimator;
    int gamePausedHash;
    public bool gamePaused;

    void OnEnable()
    {
        gamePausedHash = Animator.StringToHash("gamePaused");
        gamePaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        gamePaused = !canvasAnimator.GetBool(gamePausedHash);
        canvasAnimator.SetBool(gamePausedHash, gamePaused);


    }

    // A bit redundant, but this prevents the resume button from being activated
    // with the space bar when trying to jump
    public void Resume()
    {
        if(gamePaused)
        {
            gamePaused = false;
            canvasAnimator.SetBool(gamePausedHash, gamePaused);
        }
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        //Application.Quit();
        SceneManager.LoadScene("MainMenu");
    }
}
