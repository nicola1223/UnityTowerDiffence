using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
	public bool InvertX = false;
	public bool InvertY = false;
	public bool Invert_X_to_Y = false; 
	public int speedM = 10;
	public int otstup = 50;
	private int xinv = 1;
	private int yinv = 1;
	// Start is called before the first frame update
    void Start()
    {
    	if (InvertX)
    	{
    		xinv = -1;	
    	} 
    	else
    	{
    		xinv = 1;
    	}
    	if (InvertY)
    	{
    		yinv = -1;	
    	} 
    	else
    	{
    		yinv = 1;
    	}
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if ((Input.mousePosition.x > Screen.width - otstup) && (Input.mousePosition.x <= Screen.width))
        {
        	var speed = speedM / scale(0, otstup, 0, 5, (Screen.width - Input.mousePosition.x));
        	if (Invert_X_to_Y)
        	{
        		transform.Translate(Vector3.forward * yinv * speed * Time.fixedDeltaTime, Space.World);
        	}
        	else
        	{
        		transform.Translate(Vector3.right * xinv * speed * Time.fixedDeltaTime, Space.World);
        	}
        }
        else if ((Input.mousePosition.x < otstup) && (Input.mousePosition.x >= 0))
        {
        	var speed = speedM / scale(0, otstup, 0, 5, Input.mousePosition.x);
        	if (Invert_X_to_Y)
        	{
        		transform.Translate(Vector3.back * speed * yinv * Time.fixedDeltaTime, Space.World);
        	}
        	else
        	{
        		transform.Translate(Vector3.left * xinv * speed * Time.fixedDeltaTime, Space.World);
        	}
        }

        if ((Input.mousePosition.y > Screen.height - otstup) && (Input.mousePosition.y <= Screen.height))
        {
        	var speed = speedM / scale(0, otstup, 0, 5, (Screen.height - Input.mousePosition.y));
        	if (Invert_X_to_Y)
        	{
        		transform.Translate(Vector3.right * xinv * speed * Time.fixedDeltaTime, Space.World);
        	}
        	else
        	{
        		transform.Translate(Vector3.forward * speed * yinv * Time.fixedDeltaTime, Space.World);
        	}
        }
        else if ((Input.mousePosition.y < otstup) && (Input.mousePosition.y >= 0))
        {
        	var speed = speedM / scale(0, otstup, 0, 5, Input.mousePosition.y);
        	if (Invert_X_to_Y)
        	{
        		transform.Translate(Vector3.left * xinv * speed * Time.fixedDeltaTime, Space.World);
        	}
        	else
        	{
        		transform.Translate(Vector3.back * speed * yinv * Time.fixedDeltaTime, Space.World);
        	}
        }
    }

    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
    	float OldRange = (OldMax - OldMin);
    	float NewRange = (NewMax - NewMin);
    	float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
 
    	return(NewValue);
	}
}
