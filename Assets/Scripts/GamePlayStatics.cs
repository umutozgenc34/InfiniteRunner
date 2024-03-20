using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayStatics
{
    static GameMode gameMode;
   public static bool isPositionOccupied(Vector3 position,Vector3 DetectionHalfExtend, string occupationCheckTag)
    {
        Collider[] cols = Physics.OverlapBox(position, DetectionHalfExtend);
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == occupationCheckTag || col.gameObject.tag == "NoSpawn")
            {
                return true;
            }
        }
        return false;
    }

    public static GameMode GetGameMode()
    {
        if (gameMode ==null)
        {
            gameMode = GameObject.FindObjectOfType<GameMode>();
        }
        return gameMode;
    }
}
