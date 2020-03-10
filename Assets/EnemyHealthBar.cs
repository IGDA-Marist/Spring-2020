using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	public GameObject enemy;
	private int healthLeft;
	private Vector2 pos;
	private Vector2 point;

    // Update is called once per frame
    void Update()
    {
    	//if enemy is destroyed destory the healthbar as well
    	if (this.GetComponent<Slider>().value == 0)
    	{
    		Destroy(this.gameObject);
    	}

    	//set position of health bar above enemy's head at all times
    	point = new Vector2(enemy.transform.position.x, enemy.transform.position.y + 1f);
    	pos = Camera.main.WorldToScreenPoint (point);
        transform.position = pos;

        //set the value of the bar to enemy's health
        healthLeft = enemy.GetComponent<EnemyControl>().enemyHealth;
        this.GetComponent<Slider>().value = healthLeft;
    }
}
