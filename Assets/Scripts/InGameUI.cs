using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    void Start()
    {
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (scoreKeeper != null)
        {
            scoreKeeper.onScoreChanged += UpdateScoreText;
        }
    }

    private void UpdateScoreText(int newVal)
    {
        scoreText.SetText($"Score : {newVal}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
