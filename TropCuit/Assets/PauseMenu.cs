using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator canvasAnimator;
    int gamePausedHash;
    public static bool gamePaused;

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

    public void Quit()
    {
        Application.Quit();
    }
}
