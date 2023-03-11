using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb;
    [SerializeField] public Animator animator;
    private Vector2 difference;
    private Vector2 savedVelocity;
    private BoxCollider2D pinkCollider;
    private BoxCollider2D blackCollider;
    private float currentTime;
    private bool exploding;
    private bool beingHeld;
    private bool defused;
    private bool isBlackBomb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager.IncrementBombInField(gameObject);
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
                gameManager.GameOver(0);    // 0 for timeout
            }
            else if (currentTime >= 7.5f && exploding == false)
            {
                exploding = true;
                animator.SetBool("Exploding", true);
            }
        }
    }

    private void OnMouseDown() 
    {
        beingHeld = true;
        savedVelocity = rb.velocity;
        rb.velocity = new Vector2(0f, 0f);
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
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

    // This was trigger stay before, just in case a bug shows up
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (gameObject.GetComponent<CircleCollider2D>().IsTouching(other))
        {
            if ((isBlackBomb && other == blackCollider) || (!isBlackBomb && other == pinkCollider))
            {
                animator.SetBool("Exploding", false);
                animator.SetBool("Defused", true);
                defused = true;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;

                if (isBlackBomb)
                {
                    gameManager.IncrementBlackInCage(gameObject);
                }
                else
                {
                    gameManager.IncrementPinkInCage(gameObject);
                }
            }
            else if ((isBlackBomb && other == pinkCollider) || (!isBlackBomb && other == blackCollider))
            {
                Destroy(gameObject);

                if (isBlackBomb)
                {
                    gameManager.GameOver(1);    // black in pink cage, destory all pink
                }
                else
                {
                    gameManager.GameOver(2);    // pink in black cage, destory all black
                }
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

    public void SetPlayer(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}