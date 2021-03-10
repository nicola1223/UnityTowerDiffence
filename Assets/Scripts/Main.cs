using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

	public GameObject obj;

	public float speed = 10f;
	public int force;

    // Start is called before the first frame update
    void Start()
    {
    	Rigidbody rigid = obj.GetComponent<Rigidbody>();
        Debug.Log("Hello, world");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
        	obj.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if(Input.GetKey(KeyCode.S)){
        	obj.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if(Input.GetKey(KeyCode.A)){
        	obj.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if(Input.GetKey(KeyCode.D)){
        	obj.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        if(Input.GetKey(KeyCode.Space)){
        	obj.GetComponent<Rigidbody>().AddForce(0,force,0);
        }
    }
}
