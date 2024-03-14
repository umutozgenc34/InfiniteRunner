using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Spawnable
{
    [SerializeField] int scoreEffect;
    [SerializeField] float speedEffect;
    [SerializeField] float speedEffectDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpeedController speedController = FindObjectOfType<SpeedController>();
            if (speedController!=null && speedEffect !=0)
            {
                speedController.ChangeGlobalSpeed(speedEffect, speedEffectDuration);
            }

            ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
            if (scoreKeeper != null && scoreEffect != 0)
            {
                scoreKeeper.ChangeScore(scoreEffect);
            }

            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Threat")
        {
            Collider col = gameObject.GetComponent<Collider>();
            if (col != null)
            {
                transform.position = col.bounds.center +( col.bounds.extents.y + gameObject.GetComponent<Collider>().bounds.center.y) * Vector3.up;
            }
        }
    }
}
