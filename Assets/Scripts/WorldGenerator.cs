using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endPoint;

    [SerializeField] GameObject[] roadBlocks;

    void Start()
    {
        Vector3 nextBlockPosition = startingPoint.position;
        float endPointDistance = Vector3.Distance(startingPoint.position, endPoint.position);
        while (Vector3.Distance(startingPoint.position, nextBlockPosition) < endPointDistance)
        {
            int pick = Random.Range(0, roadBlocks.Length);
            GameObject pickedBlock = roadBlocks[pick];
            GameObject newBlock = Instantiate(pickedBlock);
            newBlock.transform.position = nextBlockPosition;
            float blockLength = newBlock.GetComponent<Renderer>().bounds.size.z;
            nextBlockPosition += (endPoint.position - startingPoint.position).normalized * blockLength;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
