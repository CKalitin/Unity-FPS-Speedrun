using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    
    private bool gameActive = false;

    public bool GameActive { get => gameActive; set => gameActive = value; }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    public void ToggleCursor(bool _toggle) {
        Cursor.visible = _toggle;
        if (_toggle)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void ActivateGame() {
        gameActive = true;
        
        ToggleCursor(false);
        FindObjectOfType<PlayerMovementController>().TogglePlayerBodyVisible(false);

        if (FindObjectOfType<EndlessGameController>())
            FindObjectOfType<EndlessGameController>().StartGame();
    }
}
