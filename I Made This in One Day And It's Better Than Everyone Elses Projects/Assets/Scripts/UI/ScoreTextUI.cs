using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextUI : MonoBehaviour {
    TextMeshProUGUI text;

    ScoreController sc;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
        sc = FindObjectOfType<ScoreController>();
    }

    private void Update() {
        text.text = "Score: " + Mathf.RoundToInt(sc.Score);
    }
}
