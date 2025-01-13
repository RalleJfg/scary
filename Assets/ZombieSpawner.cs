using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject[] zombiesInScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnZombie());
    }

   
    private IEnumerator SpawnZombie()
    {
        int random = Random.Range(5, 13);
        yield return new WaitForSeconds(random);
        Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        zombiesInScene = GameObject.FindGameObjectsWithTag("Enemy");

        if(zombiesInScene.Length < 10)
        {
            StartCoroutine(SpawnZombie());
        }
    }
}
