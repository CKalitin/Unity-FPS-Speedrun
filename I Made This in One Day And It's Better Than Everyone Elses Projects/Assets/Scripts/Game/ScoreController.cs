using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    [SerializeField] private float score;
    [Header("Score")]
    [SerializeField] private float scorePerDamageDealt = 1f;
    [SerializeField] private float scorePerEnemyKill = 5f;

    public float Score { get => score; set => score = value; }
    public float ScorePerDamageDealt { get => scorePerDamageDealt; set => scorePerDamageDealt = value; }
    public float ScorePerEnemyKill { get => scorePerEnemyKill; set => scorePerEnemyKill = value; }

    private void Update() {
        if (score > PlayerPrefs.GetFloat("Highscore", 0)) {
            PlayerPrefs.SetFloat("Highscore", score);
        }
    }
}
