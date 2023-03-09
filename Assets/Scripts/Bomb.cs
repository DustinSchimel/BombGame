using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 difference;
    private Vector2 savedVelocity;
    private bool isBlackBomb;
    private BoxCollider2D pinkCollider;
    private BoxCollider2D blackCollider;
    private float currentTime;
    private bool beingHeld;
    private bool defused;
    private PlayerState player;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        currentTime = 0;
        defused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!beingHeld && !defused)
        {
            currentTime = currentTime + Time.deltaTime;

            if (currentTime >= 8.5f)
            {
                Destroy(gameObject);
                //Debug.Log("Lose");
            }
        }
    }

    private void OnMouseDown() 
    {
        if (!defused)
        {
            beingHeld = true;
            savedVelocity = rb.velocity;
            rb.velocity = new Vector2(0f, 0f);
            difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private void OnMouseUp()
    {
        beingHeld = false;
        rb.velocity = savedVelocity;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (gameObject.GetComponent<CircleCollider2D>().IsTouching(other))
        {
            if ((isBlackBomb && other == blackCollider) || (!isBlackBomb && other == pinkCollider))
            {
                defused = true;
                player.IncrementScore();
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
            else if ((isBlackBomb && other == pinkCollider) || (!isBlackBomb && other == blackCollider))
            {
                Destroy(gameObject);
                Debug.Log("Lose");
            }
        }
    }

    public void SetBombType(bool isBlack)
    {
        isBlackBomb = isBlack;
    }

    public void SetColliders(BoxCollider2D pinkCollider, BoxCollider2D blackCollider)
    {
        this.pinkCollider = pinkCollider;
        this.blackCollider = blackCollider;
    }

    public void SetPlayer(PlayerState player)
    {
        this.player = player;
    }
}
