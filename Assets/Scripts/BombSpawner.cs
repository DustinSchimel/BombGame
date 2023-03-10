using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject blackBomb;
    public GameObject pinkBomb;
    public Vector2[] directions;
    public GameObject pinkBorder;
    public GameObject blackBorder;
    private BoxCollider2D pinkCollider;
    private BoxCollider2D blackCollider;
    private bool spawning;
    public float spawnTimer = 1.5f;
    public PlayerState player;
    public int bombCountStart;
    public int bombCount;

    // Start is called before the first frame update
    void Start()
    {
        spawning = true;

        pinkCollider = pinkBorder.GetComponent<BoxCollider2D>();
        blackCollider = blackBorder.GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerState>();

        StartCoroutine("BombSpawnTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBomb()
    {
        int choice = Random.Range(0, 2);
        GameObject bombObject;
        Bomb bombScript;
        if (choice == 0)
        {
            bombObject = Instantiate(blackBomb, GetComponent<Transform>().position, Quaternion.identity);
            bombScript = bombObject.GetComponent<Bomb>();
            bombScript.SetBombType(true);
        }
        else
        {
            bombObject = Instantiate(pinkBomb, GetComponent<Transform>().position, Quaternion.identity);
            bombScript = bombObject.GetComponent<Bomb>();
        }

        bombObject.GetComponent<Rigidbody2D>().velocity = directions[Random.Range(0, directions.Length)];
        bombScript.SetColliders(pinkCollider, blackCollider);
        bombScript.SetPlayer(player);
        bombObject.GetComponent<SpriteRenderer>().sortingOrder = bombCount + bombCountStart;
        bombCount++;
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