using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private GameObject gameUIParent;
    [SerializeField] private GameObject menuUIParent;
    [SerializeField] private GameObject playerDeathUIParent;
    
    private PlayerCameraController pcc;

    private void Awake() {
        pcc = FindObjectOfType<PlayerCameraController>();
        
        gameUIParent.SetActive(false);
        menuUIParent.SetActive(true);
        playerDeathUIParent.SetActive(false);

        if (PlayerPrefs.GetString("PlayMode", "Default") == "PlayOnAwake") {
            Play();
        }
    }

    public void ActiveGameUI() {
        gameUIParent.SetActive(true);
    }

    public void PlayerDeath() {
        GameController.instance.GameActive = false;
        GameController.instance.ToggleCursor(true);

        Time.timeScale = 0f;
        
        playerDeathUIParent.SetActive(true);
        gameUIParent.SetActive(false);
    }

    public void Play() {
        pcc.LerpToFollowObjectPosition();
        menuUIParent.SetActive(false);
    }

    public void RestartLevel() {
        PlayerPrefs.SetString("PlayMode", "PlayOnAwake");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

public void Quit() {
        Application.Quit();
    }

    private void OnApplicationQuit() {
        PlayerPrefs.SetString("PlayMode", "");
    }
}
