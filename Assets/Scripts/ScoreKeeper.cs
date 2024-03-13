using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public delegate void OnScoreChanged(int newVal);

    public event OnScoreChanged onScoreChanged;
    private int score;

    public void ChangeScore(int amt)
    {
        score += amt;
        Debug.Log($"skor de�i�tirildi{score}");
        onScoreChanged?.Invoke(score);
    }
}
