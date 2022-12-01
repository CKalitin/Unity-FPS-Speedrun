using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTextUI : MonoBehaviour {
    TextMeshProUGUI text;

    ScoreController sc;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
        sc = FindObjectOfType<ScoreController>();
    }

    private void Update() {
        text.text = "Highscore: " + Mathf.RoundToInt(PlayerPrefs.GetFloat("Highscore"));
    }
}
