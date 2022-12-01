using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject pauseMenuParent;
    [SerializeField] private GameObject gameUIParent;

    private void Awake() {
        pauseMenuParent.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && GameController.instance.GameActive) {
            if (pauseMenuParent.activeSelf) ClosePauseMenu();
            else OpenPauseMenu();
        }
    }

    public void OpenPauseMenu() {
        GameController.instance.ToggleCursor(true);
        
        pauseMenuParent.SetActive(true);
        gameUIParent.SetActive(false);
        
        Time.timeScale = 0f;
    }

    public void ClosePauseMenu() {
        GameController.instance.ToggleCursor(false);

        pauseMenuParent.SetActive(false);
        gameUIParent.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Quit() {
        Application.Quit();
    }
    
    public void RestartLevel() {
        PlayerPrefs.SetString("PlayMode", "PlayOnAwake");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
