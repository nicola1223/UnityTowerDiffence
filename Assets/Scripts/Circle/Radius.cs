using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour
{

	public float theta_scale = 0.05f;        //Set lower to add more points
  	int size; //Total number of points in circle
  	public float radius = 20f;
  	LineRenderer lineRenderer;
  	private bool activated = true;

  	void Awake () {       
    	float sizeValue = (2.0f * Mathf.PI) / theta_scale; 
    	size = (int)sizeValue;
    	size++;
    	lineRenderer = gameObject.AddComponent<LineRenderer>();
//    	lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    	lineRenderer.SetWidth(0.5f, 0.5f); //thickness of line
    	lineRenderer.SetVertexCount(size);      
  	}

  	void Update () { 
	  	if (activated)
	  	{
	  		//lineRenderer.SetActive(true);
	  		lineRenderer.enabled = true;
	    	Vector3 pos;
	    	float theta = 0f;
	    	for(int i = 0; i < size; i++)
	    	{          
	      		theta += (2.0f * Mathf.PI * theta_scale);         
	      		float x = radius * Mathf.Cos(theta);
	      		float z = radius * Mathf.Sin(theta);          
	      		x += gameObject.transform.position.x;
	      		z += gameObject.transform.position.z;
	      		pos = new Vector3(x, 0, z);
	      		lineRenderer.SetPosition(i, pos);
	      	}
    	}
    	else
    	{
    		//lineRenderer.SetActive(false);
    		lineRenderer.enabled = false;
    	}
    	//Debug.Log(gameObject.AddComponent<LineRenderer>());
    }

    void IPointerClickHandler()
    {
    	Debug.Log("Yes");
    }

    void OnMouseDown()
    {
    	if (activated)
    	{
    		activated = false;
    	}
    	else
    	{
    		activated = true;
    	}
    }
}
