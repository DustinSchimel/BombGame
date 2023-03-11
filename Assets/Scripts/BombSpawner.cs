using System.Collections;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Vector2[] directions;
    [SerializeField] private GameObject blackBomb;
    [SerializeField] private GameObject pinkBomb;
    [SerializeField] private GameObject blackBorder;
    [SerializeField] private GameObject pinkBorder;
    private BoxCollider2D blackBorderCollider;
    private BoxCollider2D pinkBorderCollider;
    private bool spawning;
    [SerializeField] private float spawnTimer = 1.5f;
    public GameManager gameManager;
    [SerializeField] private int bombCountStart;
    private int bombCount;

    void Start()
    {
        spawning = true;

        pinkBorderCollider = pinkBorder.GetComponent<BoxCollider2D>();
        blackBorderCollider = blackBorder.GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();

        StartCoroutine("BombSpawnTimer");
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
        bombScript.SetColliders(pinkBorderCollider, blackBorderCollider);
        bombScript.SetPlayer(gameManager);
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