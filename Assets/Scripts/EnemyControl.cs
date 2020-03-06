using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
	public int playerDamage; 							//The amount of HP to subtract from the player when attacking.
	public LayerMask playerMask;						//layermask the player is on
	//private Animator animator;						/Variable of type Animator to store a reference to the enemy's Animator component.
	private GameObject target;							//target game object (player)
	private float distance = 6f;						//distance between enemy and player that causes enemy to chase after player
	private float speed = 3f;							//Enemy speed
	private bool pause = false;							//whether or not the enemy will pause
	private float timer = 1f;							//timer for how long the enemy pauses
	private Vector2 direction;							//direction enemy is facing
	private float enemyRange = 0.5f;					//how close the player needs to be for the enemy to attack

    // Start is called before the first frame update
    void Start()
    {
        //Find the Player GameObject using it's tag and store a reference to its transform component.
		target = GameObject.FindGameObjectWithTag ("Player");
		direction = Vector2.left; //enemy starts facing left
    }//start

    // Update is called once per frame
    void FixedUpdate()
    {
    	//check where the player is to adjust enemy direction
    	if (target.transform.position.x < transform.position.x)
    	{
    		direction = Vector2.left;
    	}//if
    	else
    	{
    		direction = Vector2.right;
    	}//else

    	//use a raycast to detect the player (and other things)
    	RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, enemyRange, playerMask);
    	Debug.DrawRay(transform.position, direction, Color.green);

    	if (hit.collider != null)
   		{
			//If the player is detected
    		if (hit.collider == target.GetComponent<BoxCollider2D>())
    		{
    			pause = true; //have the enemy pause after contact with player
    			Debug.Log("This is where the animation will go");
    			Debug.Log("This is where the enemy will attack the player and take away player health");
    		}//if
   		}//if

    	//if player is within a certain distance of enemy
        if (Vector2.Distance(transform.position, target.transform.position) <= distance && pause == false)
        {
        	float step = speed * Time.deltaTime;
        	// move enemy towards the target location
        	transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
        }//if
        else if (pause == true)
        {
        	//set a timer for how long the enmy should pause
        	timer -= Time.deltaTime;
        	
        	if (timer <= 0)
        	{
        		pause = false;
        		timer = 1f;
        	}//if
        }//else if
    }//Update

}//Enemy
