using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject blackBomb;
    public GameObject pinkBomb;
    public Vector2[] directions;
    private bool spawning;
    public float spawnTimer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        spawning = true;

        StartCoroutine("BombSpawnTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBomb()
    {
        int choice = Random.Range(0, 2);
        GameObject bomb;
        if (choice == 0)
        {
            bomb = Instantiate(blackBomb, GetComponent<Transform>().position, Quaternion.identity);
        }
        else
        {
            bomb = Instantiate(pinkBomb, GetComponent<Transform>().position, Quaternion.identity);
        }

        bomb.GetComponent<Rigidbody2D>().velocity = directions[Random.Range(0, directions.Length)];
    }

    IEnumerator BombSpawnTimer()
    {
        while (spawning)
        {
            SpawnBomb();

            yield return new WaitForSeconds(spawnTimer);
        }
    }
}