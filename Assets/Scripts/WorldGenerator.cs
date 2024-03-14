using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Road Blocks")]
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject[] roadBlocks;

    [Header("Buildings")]
    [SerializeField] GameObject[] buildings;
    [SerializeField] Transform[] buildingSpawnPoints;
    [SerializeField] Vector2 buildingSpawnScaleRange = new Vector2(0.6f,0.9f);

    [Header("Street Lights")]
    [SerializeField] GameObject streetLight;
    [SerializeField] Transform[] streetLightSpawnPoints;

    [Header("Threats")]
    [SerializeField] Vector3 OccupationDetectionHalfExtend;
    [SerializeField] Threat[] threats;
    [SerializeField] Transform[] lanes;

    [Header("Pickups")]
    [SerializeField] Pickup[] pickups;

    Vector3 MoveDirection;

    bool GetRandomSpawnPoint(out Vector3 spawnPoint , string occupationCheckTag)
    {
        Vector3[] spawnPoints = GetAvaliableSpawnPoints(occupationCheckTag);
        if (spawnPoints.Length == 0)
        {
            spawnPoint = new Vector3(0, 0, 0);
            return false;
            
        }
        int pick = Random.Range(0, spawnPoints.Length);
        spawnPoint = spawnPoints[pick];
        return true;
        
    }

    Vector3[] GetAvaliableSpawnPoints(string occupationCheckTag)
    {
        List<Vector3> AvaliableSpawnPoints = new List<Vector3>();
        foreach (Transform spawnTrans in lanes)
        {
            Vector3 spawnPoint = spawnTrans.position + new Vector3(0, 0, startingPoint.position.z);
            if (!isPositionOccupied(spawnPoint , occupationCheckTag))
            {
                AvaliableSpawnPoints.Add(spawnPoint);
            }
        }
        return AvaliableSpawnPoints.ToArray();
    }

    bool isPositionOccupied(Vector3 position , string occupationCheckTag)
    {
        Collider[] cols = Physics.OverlapBox(position, OccupationDetectionHalfExtend);
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == occupationCheckTag)
            {
                return true;
            }
        }
        return false;
    }

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

        StartSpawnElements();

        Pickup newPickup = Instantiate(pickups[0], startingPoint.position, Quaternion.identity);
        newPickup.GetComponent<MovementComp>().SetDestination(endPoint.position);
        newPickup.GetComponent<MovementComp>().SetMoveDir(MoveDirection);

    }

    private void StartSpawnElements()
    {
        foreach (Threat threat in threats)
        {
            StartCoroutine(SpawnElement(threat));
        }
        foreach (Pickup pickup in pickups)
        {
            StartCoroutine(SpawnElement(pickup));
        }
    }

    IEnumerator SpawnElement(Spawnable elementToSpawn)
    {
        while (true)
        {
            if (GetRandomSpawnPoint(out Vector3 spawnPoint ,elementToSpawn.gameObject.tag))
            {
                Spawnable newThreat = Instantiate(elementToSpawn, spawnPoint, Quaternion.identity);

                newThreat.GetMovementComponent().SetDestination(endPoint.position);
                newThreat.GetMovementComponent().SetMoveDir(MoveDirection);
            }
            

            yield return new WaitForSeconds(elementToSpawn.SpawnInterval);
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
            
            moveComp.SetDestination(endPoint.position);
            moveComp.SetMoveDir(MoveDir);
        }

        SpawnBuildings(newBlock);
        SpawnStreetLights(newBlock);

        return newBlock;


    }

    private void SpawnStreetLights(GameObject parentBlock)
    {
        foreach (Transform StreetLightSpawnPoint in streetLightSpawnPoints)
        {
            Vector3 SpawnLoc = parentBlock.transform.position + (StreetLightSpawnPoint.position - startingPoint.position);
            Quaternion SpawnRot = Quaternion.LookRotation((StreetLightSpawnPoint.position - startingPoint.position).normalized, Vector3.up);
            Quaternion SpawnRotOffset = Quaternion.Euler(0, 90, 0);
            GameObject newStreetLight = Instantiate(streetLight, SpawnLoc, SpawnRot*SpawnRotOffset, parentBlock.transform);
        }
        
    }

    private void SpawnBuildings(GameObject parentBlock)
    {
        foreach (Transform BuildingSpawnPoint in buildingSpawnPoints)
        {
            Vector3 BuildingSpawnLoc = parentBlock.transform.position + (BuildingSpawnPoint.position - startingPoint.position);
            int rotationOffsetBy90 = Random.Range(0, 3);
            Quaternion BuildingSpawnRotation = Quaternion.Euler(0, rotationOffsetBy90 * 90, 0);
            Vector3 BuildingSpawnSize = Vector3.one * Random.Range(buildingSpawnScaleRange.x, buildingSpawnScaleRange.y);

            int buildPick = Random.Range(0, buildings.Length);
            GameObject newBuilding = Instantiate(buildings[buildPick], BuildingSpawnLoc, BuildingSpawnRotation, parentBlock.transform);
            newBuilding.transform.localScale = BuildingSpawnSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject != null && other.gameObject.tag == "Road")
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
