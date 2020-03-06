using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnTriggerEnter2D(Collider2D other)
    {

        //Check if the tag of the trigger collided with is Exit.
        if (other.tag == "Exit")
        {
            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            //Invoke("Restart", restartLevelDelay);

            //Disable the player object since level is over.
            enabled = false;
        }

        // <-------------------------------------------------------------------------------------------------->
        //This is important information
        // <-------------------------------------------------------------------------------------------------->

        /*
        //Check if the tag of the trigger collided with is Food.
        else if (other.tag == "Food")
        {
            Add pointsPerFood to the players current health total.
            if (health >= 100)
            {
                //health = 100;
            }

            else
                health += pointsPerFood;

            //Update healthText to represent current total and notify player that they gained points
            healthText.text = "+" + pointsPerFood + " Health: " + health;

            //Call the RandomizeSfx function of SoundManager and pass in two eating sounds to choose between to play the eating sound effect.

            //Disable the food object the player collided with.
            other.gameObject.SetActive(false);
        }

        //Check if the tag of the trigger collided with is Soda.
        else if (other.tag == "Soda")
        {
            //Add pointsPerSoda to players health points total
            health += pointsPerSoda;

            //Update healthText to represent current total and notify player that they gained points
            healthText.text = "+" + pointsPerSoda + " HP: " + health;

            //Call the RandomizeSfx function of SoundManager and pass in two drinking sounds to choose between to play the drinking sound effect.
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);

            //Disable the soda object the player collided with.
            other.gameObject.SetActive(false);
        }
    }
    */
}
}

