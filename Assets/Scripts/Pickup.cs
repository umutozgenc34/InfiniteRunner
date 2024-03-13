using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int scoreEffect;
    [SerializeField] float speedEffect;
    [SerializeField] float speedEffectDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpeedController speedController = FindObjectOfType<SpeedController>();
            if (speedController!=null)
            {
                speedController.ChangeGlobalSpeed(speedEffect, speedEffectDuration);
            }

            ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
            if (scoreKeeper != null)
            {
                scoreKeeper.ChangeScore(scoreEffect);
            }
        }
    }
}
