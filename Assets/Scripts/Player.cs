using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public LayerMask mask;
    public Animator anim;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 direction;
    private float playerRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.right;
    }

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
        else if (movement.x < 0)
        {
            direction = Vector2.left;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

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

