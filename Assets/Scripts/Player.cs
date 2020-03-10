using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public LayerMask mask;
    public Animator anim;
    public int playerHealthPoints = 100;						//Starting value for Player health.
    public Text healthText;						//UI Text to display current player health total.
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 direction;
    private float playerRange = 2f;
    private int playerAttack = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        direction = Vector2.right;

        //Set the healthText to reflect the current player HP total.
		healthText.text = "HP: " + playerHealthPoints;


    }//Start

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        //have player sprite switch according to direction
        if (movement.x > 0)
        {
            direction = Vector2.right;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (movement.x < 0)
        {
            direction = Vector2.left;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        healthText.text = "HP: " + playerHealthPoints;
    }//Update

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        //use a raycast to detect the enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, playerRange, mask);
        Debug.DrawRay(transform.position, direction, Color.blue);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy" && Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("playerChop");
                hit.collider.GetComponent<EnemyControl>().EnemyHit(playerAttack);
            }
        }
    }

    public void Hit(int damage)
    {
        anim.SetTrigger("playerHit");
        playerHealthPoints -= damage;
    }

}