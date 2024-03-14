using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySpawner : MonoBehaviour
{
    [SerializeField] int amount = 10;
    [SerializeField] float gap = 1f;
    void Start()
    {
        for (int i = 1; i <= amount; i++)
        {
            Vector3 spawnPosition =new Vector3 (transform.position.x,0,transform.position.z) + transform.forward * gap * i;
            GameObject nextCoin = Instantiate(gameObject, spawnPosition, Quaternion.identity);
            nextCoin.GetComponent<ArraySpawner>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
