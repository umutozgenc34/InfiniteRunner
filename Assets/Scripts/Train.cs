
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] TrainSegments segmentPrefab;
    [SerializeField] Vector2 SegmentCountRange;
    [SerializeField] Threat threat;
    void Start()
    {
        GenerateTrainBody();
    }

    private void GenerateTrainBody()
    {
        int bodyCount = Random.Range((int)SegmentCountRange.x,(int) SegmentCountRange.y);
        for (int i = 0; i < bodyCount; i++)
        {
            Vector3 spawnPosition = transform.position + transform.forward * segmentPrefab.GetSegmentLength() * i;
            TrainSegments newSegment = Instantiate(segmentPrefab, spawnPosition, Quaternion.identity);
            if (i == 0)
            {
                newSegment.SetHead();
            }
            newSegment.GetMovementComponent().CopyFrom(threat.GetMovementComponent());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
