using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private GameObject gameUIParent;
    [SerializeField] private GameObject menuUIParent;

    private PlayerCameraController pcc;

    private void Awake() {
        pcc = FindObjectOfType<PlayerCameraController>();
        
        Time.timeScale = 0f;
        gameUIParent.SetActive(false);
        menuUIParent.SetActive(true);
    }

    public void ActiveGameUI() {
        gameUIParent.SetActive(true);
    }

    public void Play() {
        pcc.LerpToFollowObjectPosition();
        menuUIParent.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }
}
