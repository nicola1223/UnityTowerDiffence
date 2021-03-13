using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rotation : MonoBehaviour
{
    private float lasttime = 0;
    public float cooldown = 1;
	public GameObject[] objects;
	private float MinDist;
	private GameObject target;
	private GameObject castle;
	public int speed = 10;
	private Transform spawn;
	public GameObject bullet;
	public int ShootDist = 20;

    // Start is called before the first frame update
    void Start()
    {
    	spawn = transform.Find("spawn");
        bullet.transform.position = spawn.transform.position;
    	castle = GameObject.FindGameObjectsWithTag("Castle")[0];
    	StartCoroutine(UpdateArr());
        lasttime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = null;
        MinDist = 0;
    	//objects.Clear();
    	//objects.AddRange(GameObject.FindGameObjectsWithTag("object"));
    	
        for (var i = 0; i < objects.Length; i++)
    	{
    		var obj = objects[i];

//    		var dist = obj.transform.position - castle.transform.position;

    		var dist = (obj.transform.position - castle.transform.position).sqrMagnitude;
//  		dist = Mathf.Sqrt(dist);
    		var distP = (obj.transform.position - transform.position).sqrMagnitude;
            if ((dist < MinDist || MinDist == 0) && distP <= (ShootDist * ShootDist))
    		{
                MinDist = dist;
                target = obj; 
    		}
    	}    

    	transform.LookAt(target.transform.position);

        //Input.GetKeyDown(KeyCode.Space) && 
        //(target.transform.position - transform.position).sqrMagnitude <= (ShootDist * ShootDist) &&
    	
        if (Time.time - lasttime >= cooldown)
    	{
            lasttime = Time.time;
    		Shoot();
    	}
    }

    void Shoot()
    {
        GameObject bullet1 = Instantiate(bullet, spawn.transform.position, Quaternion.identity);
        bullet1.SetActive(true);
//    	bullet1.transform.position = spawn.transform.position;
    	bullet1.transform.localRotation = transform.localRotation;
    	bullet1.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
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
