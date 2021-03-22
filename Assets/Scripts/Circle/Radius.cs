using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Radius : MonoBehaviour
{

	public float theta_scale = 0.05f;        //Set lower to add more points
  	int size; //Total number of points in circle
  	public float radius = 20f;
  	LineRenderer lineRenderer;
  	private bool activated = true;
  	public GameObject[] objects;
  	public bool triger = false;

  	void Awake () {       
    	float sizeValue = (2.0f * Mathf.PI) / theta_scale; 
    	size = (int)sizeValue;
    	size++;
    	lineRenderer = gameObject.AddComponent<LineRenderer>();
//    	lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    	lineRenderer.SetWidth(0.5f, 0.5f); //thickness of line
    	lineRenderer.SetVertexCount(size);      
  	}

  	void Start()
  	{
  		StartCoroutine(UpdateArr());
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
    	if(Input.GetMouseButtonDown(0))
    	{
    		if(activated && !triger)
    		{
    			activated = false;
    		}
    	}
    	triger = false;
    	//Debug.Log(gameObject.AddComponent<LineRenderer>());
    }

    void OnMouseDown()
    {
    	activated = true;
    	foreach (var obj in objects)
    	{
    		if (obj != gameObject)
    		{
    			obj.GetComponent<Radius>().activated = false;
    		}
    	}
    	triger = true;
    }

    IEnumerator UpdateArr()
    {
    	while (true)
    	{
    		if (!objects.SequenceEqual(GameObject.FindGameObjectsWithTag("tower")))
    		{
    			objects = GameObject.FindGameObjectsWithTag("tower");
    		}
    		else 
    		{
    			yield return null;
    		}
    	}
    }
}
