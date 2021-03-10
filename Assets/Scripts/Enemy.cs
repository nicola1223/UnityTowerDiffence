using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public EnemyPath MyPath;
	public float speed = 10;
	public float maxDistance = .1f;

	private IEnumerator<Transform> pointInPath;
	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody>();

     	if (MyPath == null)
        {
        	return;
        }

        pointInPath = MyPath.GetNextPathPoint();

        pointInPath.MoveNext();

        if (pointInPath.Current == null)
        {
        	return;
        }

        transform.position = pointInPath.Current.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
        	return;
        }

        LookAtXZ(pointInPath.Current.position);

        rb.AddForce(new Vector3(0, 0, 1) * speed);
    }

    void LookAtXZ(Vector3 point)
    {
        var direction = (point - transform.position).normalized;
        direction.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(direction);
    }

}
