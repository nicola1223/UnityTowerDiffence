using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBullet : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
    	//if(coll.tag == "bullet")
    	//{
    		Debug.Log("Ты попал");
    	//}
    }
}
