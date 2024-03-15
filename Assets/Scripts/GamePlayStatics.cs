using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayStatics
{
   public static bool isPositionOccupied(Vector3 position,Vector3 DetectionHalfExtend, string occupationCheckTag)
    {
        Collider[] cols = Physics.OverlapBox(position, DetectionHalfExtend);
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == occupationCheckTag)
            {
                return true;
            }
        }
        return false;
    }
}
