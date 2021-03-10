using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
	public enum MovementType
	{
		Moveing,
		Lerping
	}

	public MovementType Type = MovementType.Moveing;
	public MovingPath MyPath;
	public float speed = 1;
	public float maxDistance = .1f;

	private IEnumerator<Transform> pointInPath;

    // Start is called before the first frame update
    void Start()
    {
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

        Vector3 pos = new Vector3(pointInPath.Current.position.x, transform.position.y, pointInPath.Current.position.z);

        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
        	return;
        }

        if (Type == MovementType.Moveing)
        {
        	Vector3 end = new Vector3(pointInPath.Current.position.x, transform.position.y, pointInPath.Current.position.z);
        	Vector3 direction = pointInPath.Current.position - transform.position;
        	direction.y = 0;
        	Quaternion qqq1 = Quaternion.LookRotation(direction);
        	transform.localRotation = Quaternion.Lerp(transform.localRotation, qqq1, 10 * Time.deltaTime);
        	transform.position = Vector3.MoveTowards(transform.position, new Vector3(pointInPath.Current.position.x, transform.position.y, pointInPath.Current.position.z), Time.deltaTime * speed);
        }
		else if (Type == MovementType.Lerping)
		{
			Vector3 end = new Vector3(pointInPath.Current.position.x, transform.position.y, pointInPath.Current.position.z);
			Vector3 direction = pointInPath.Current.position - transform.position;
        	direction.y = 0;
        	Quaternion qqq1 = Quaternion.LookRotation(direction);
        	transform.localRotation = Quaternion.Lerp(transform.localRotation, qqq1, 10 * Time.deltaTime);
			transform.position = Vector3.Lerp(transform.position, new Vector3(pointInPath.Current.position.x, transform.position.y, pointInPath.Current.position.z), Time.deltaTime * speed);
		}       

		var distanceSquare = (transform.position - new Vector3(pointInPath.Current.position.x, transform.position.y, pointInPath.Current.position.z)).sqrMagnitude;
		if (distanceSquare < maxDistance * maxDistance)
		{
			pointInPath.MoveNext();
		}
    }
}
