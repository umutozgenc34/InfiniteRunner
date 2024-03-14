using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrainSegments : MonoBehaviour
{
    [SerializeField] Mesh headMesh;
    [SerializeField] Mesh[] segmentMeshes;

    [SerializeField] MeshFilter trainMesh;

    void Start()
    {
        RandomTrainMesh();
    }

    private void RandomTrainMesh()
    {
        int pick = Random.Range(0, segmentMeshes.Length);
        trainMesh.mesh = segmentMeshes[pick];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
