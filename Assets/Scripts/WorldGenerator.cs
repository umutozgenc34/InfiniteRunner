using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] float envMoveSpeed = 4f;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endPoint;

    [SerializeField] GameObject[] roadBlocks;
    [SerializeField] GameObject[] buildings;

    [SerializeField] Transform[] buildingSpawnPoints;
    [SerializeField] Vector2 buildingSpawnScaleRange = new Vector2(0.6f,0.9f);

    Vector3 MoveDirection;

    void Start()
    {
        Vector3 nextBlockPosition = startingPoint.position;
        float endPointDistance = Vector3.Distance(startingPoint.position, endPoint.position);
        MoveDirection = (endPoint.position - startingPoint.position).normalized;
        while (Vector3.Distance(startingPoint.position, nextBlockPosition) < endPointDistance)
        {
            GameObject newBlock = SpawnNewBlock(nextBlockPosition,MoveDirection);
            float blockLength = newBlock.GetComponent<Renderer>().bounds.size.z;  
            nextBlockPosition += MoveDirection * blockLength;
            
        }
        

    }

    GameObject SpawnNewBlock(Vector3 SpawnPosition, Vector3 MoveDir)
    {
        int pick = Random.Range(0, roadBlocks.Length);
        GameObject pickedBlock = roadBlocks[pick];
        GameObject newBlock = Instantiate(pickedBlock);
        newBlock.transform.position = SpawnPosition;
        MovementComp moveComp = newBlock.GetComponent<MovementComp>();
        if (moveComp != null)
        {
            moveComp.SetMoveSpeed(envMoveSpeed);
            moveComp.SetDestination(endPoint.position);
            moveComp.SetMoveDir(MoveDir);
        }

        foreach (Transform BuildingSpawnPoint in buildingSpawnPoints)
        {
            Vector3 BuildingSpawnLoc = SpawnPosition + (BuildingSpawnPoint.position - startingPoint.position);
            int rotationOffsetBy90 = Random.Range(0, 3);
            Quaternion BuildingSpawnRotation = Quaternion.Euler(0, rotationOffsetBy90 * 90, 0);
            Vector3 BuildingSpawnSize = Vector3.one * Random.Range(buildingSpawnScaleRange.x, buildingSpawnScaleRange.y);

            int buildPick = Random.Range(0, buildings.Length);
            GameObject newBuilding = Instantiate(buildings[buildPick], BuildingSpawnLoc, BuildingSpawnRotation,newBlock.transform);
            newBuilding.transform.localScale = BuildingSpawnSize;
        }

        return newBlock;


    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.gameObject.name}ayrýldý");
        if (other.gameObject != null)
        {
            GameObject newBlock = SpawnNewBlock(other.transform.position, MoveDirection);
            float newBlockHalfwidth = newBlock.GetComponent<Renderer>().bounds.size.z/2f;
            float previousBlockHalfwidth =other.GetComponent<Renderer>().bounds.size.z/2f;

            Vector3 newBlockSpawnOffset = -(newBlockHalfwidth + previousBlockHalfwidth) * MoveDirection;
            newBlock.transform.position += newBlockSpawnOffset;
            newBlock.name = "yeni spawnlandi";
        }
    }
}
