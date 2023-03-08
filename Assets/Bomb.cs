using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Vector2[] directions;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        RandomizeVelocity();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RandomizeVelocity()
    {
        rb.velocity = directions[Random.Range(0, directions.Length)];
    }
}
