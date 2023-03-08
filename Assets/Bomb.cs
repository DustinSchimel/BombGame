using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        StartCoroutine("ExplosionTimer");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(8.5f);

        Destroy(gameObject);
    }
}
