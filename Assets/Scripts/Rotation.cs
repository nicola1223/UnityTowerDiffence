using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rotation : MonoBehaviour
{
	public GameObject[] objects;
	private float MinDist;
	private GameObject target;
	private GameObject castle;
	public GameObject lastObj;
	public int speed = 10;
	private float dist;
	private Transform spawn;
	public GameObject bullet;
	public int ShootDist;

    // Start is called before the first frame update
    void Start()
    {
    	spawn = transform.Find("spawn");
        bullet.transform.position = spawn.transform.position;
    	castle = GameObject.FindGameObjectsWithTag("Castle")[0];
    	StartCoroutine(UpdateArr());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    	MinDist = 0;
    	//objects.Clear();
    	//objects.AddRange(GameObject.FindGameObjectsWithTag("object"));
    	for (var i = 0; i < objects.Length; i++)
    	{
    		var obj = objects[i];
    		if (obj == null)
    		{
    			continue;
    		}

//    		var dist = obj.transform.position - castle.transform.position;

    		dist = (obj.transform.position - castle.transform.position).sqrMagnitude;
//  		dist = Mathf.Sqrt(dist);
    		if (dist < MinDist || MinDist == 0)
    		{
    			MinDist = dist;
    			target = obj;
    		}
    	}
    	if (target != lastObj || lastObj == null)
    	{
    		lastObj = target;
    	}    	
    	transform.LookAt(target.transform.position);
    	if (dist < ShootDist)
    	{
    		Shoot(target.transform.position);
    	}
    }

    void Shoot(Vector3 targetPos)
    {
    	bullet.transform.position = spawn.transform.position;
    	bullet.transform.localRotation = transform.localRotation;
    	bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetPos, Time.deltaTime * speed);
    }

    IEnumerator UpdateArr()
    {
    	while (true)
    	{
    		if (!objects.SequenceEqual(GameObject.FindGameObjectsWithTag("object")))
    		{
    			objects = GameObject.FindGameObjectsWithTag("object");
    		}
    		else 
    		{
    			yield return null;
    		}
    	}
    }
}
