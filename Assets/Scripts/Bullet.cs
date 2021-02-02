using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public float range;

	private float current_speed;
	private float start_point_x;
	private bool flying;

	void Start()
	{
		flying = false;
		current_speed = 0;
		gameObject.SetActive(false);
	}

    void Update()
    {
    	if (flying == false)
    	{
    		gameObject.SetActive(false);
    	}
        else
        {
            transform.position += new Vector3(current_speed, 0, 0);
            float current_range = transform.position.x - start_point_x;
            if (current_range < 0) current_range *= -1;
            if (current_range > range)
            {
                current_speed = 0;
                flying = false;
            }
        }
    }

    public void Fire(float x, float y, int direction)
    {
    	if (!flying)
    	{
    		transform.position = new Vector3(x, y, 0);
    		current_speed = speed * direction;
    		start_point_x = x;
    		flying = true;
    	}
    } 
}
