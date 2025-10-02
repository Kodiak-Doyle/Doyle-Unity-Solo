using UnityEngine;
using System.Collections;

public class RandomFireballSpawn : MonoBehaviour
{ 

public GameObject fireballPrefab;

public bool CanSpawn = true;

public Vector3 spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSpawn)
        {
            StartCoroutine(SpawnFireball());
            CanSpawn = false;
        }
    }

    IEnumerator SpawnFireball()
    {
        while(true)
        {
            CanSpawn = false;
            float randomX = Random.Range(spawnPoint.x - 20f ,spawnPoint.x + 20f);
            float randomZ = Random.Range(spawnPoint.z - 20f,spawnPoint.z + 20f);
            Vector3 spawnPosition = new Vector3(randomX, spawnPoint.y, randomZ);
            Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.75f);
            CanSpawn = true;
            //yield return new WaitForSeconds(5f);
            //DestroyImmediate(fireballPrefab, true);
        }
    }

}
