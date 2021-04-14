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
  	private bool activated = false;
  	public GameObject[] objects;
  	public bool triger = false;
  	public bool moving = false;
    private Vector3 pos;
    private Camera _camera;
    Transform gun;

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
  		_camera = Camera.main;
  		foreach(Transform child in transform)
		{
			if(child.tag == "gun")
			{
				gun = child;
			}
		}
  		StartCoroutine(UpdateArr());
  	}

  	void FixedUpdate () { 
  		if (moving)
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
	        lineRenderer.enabled = true;
		   	Vector3 pos1;
	    	float theta = 0f;
	    	for(int i = 0; i < size; i++)
	    	{          
	      		theta += (2.0f * Mathf.PI * theta_scale);         
	      		float x = radius * Mathf.Cos(theta);
	      		float z = radius * Mathf.Sin(theta);          
	      		x += gameObject.transform.position.x;
	      		z += gameObject.transform.position.z;
	      		pos1 = new Vector3(x, 0, z);
	      		lineRenderer.SetPosition(i, pos1);
	      	}
        	gun.GetComponent<Rotation>().shooted = false;
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
            transform.position = pos;
            if(Input.GetMouseButtonDown(0))
        	{
        		moving = false;
        		gun.GetComponent<Rotation>().shooted = true;	
        	}
        }

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

    public void Moving()
    {
    	activated = true;
    	GameObject tower_n = Instantiate(gameObject, new Vector3(0,0,0), Quaternion.identity);
    	tower_n.SetActive(true);
    	tower_n.GetComponent<Radius>().moving = true;
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
