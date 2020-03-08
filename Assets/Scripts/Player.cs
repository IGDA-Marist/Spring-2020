using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
<<<<<<< HEAD
    public LayerMask mask;
    public Animator anim;
=======

    public int playerHealthPoints = 100;						//Starting value for Player health.

    public Text healthText;						//UI Text to display current player health total.

>>>>>>> e57827f00b1b25617b0de4bdfba1e7de50818dd9
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 direction;
    private float playerRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        direction = Vector2.right;
=======

    

        //Set the healthText to reflect the current player HP total.
		healthText.text = "HP: " + playerHealthPoints;

>>>>>>> e57827f00b1b25617b0de4bdfba1e7de50818dd9
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

<<<<<<< HEAD
        //have player sprite switch according to direction
        if (movement.x > 0)
        {
            direction = Vector2.right;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement.x < 0)
        {
            direction = Vector2.left;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
=======
		}
    
>>>>>>> e57827f00b1b25617b0de4bdfba1e7de50818dd9

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
                hit.collider.GetComponent<EnemyControl>().Hit();
            }
        }
    }

    public void Hit()
    {
        anim.SetTrigger("playerHit");
    }

}