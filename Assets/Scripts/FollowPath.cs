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

	public enum PathTypes
	{
		linear,
		loop
	}

	public PathTypes PathType;
	public int movementDirection = 1;
	public int moveTo = 0;
	public Transform[] PathElements;

	public MovementType Type = MovementType.Moveing;
//	public MovingPath MyPath;
	public float speed = 10;
	public float maxDistance = .1f;

	private IEnumerator<Transform> pointInPath;

    // Start is called before the first frame update
    void Start()
    {

        pointInPath = GetNextPathPoint();

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

    public IEnumerator<Transform> GetNextPathPoint()
	{
		if (PathElements == null || PathElements.Length < 1)
		{
			yield break;
		}

		while (true) 
		{
			yield return PathElements[moveTo];

			if (PathElements.Length == 1)
			{
				continue;
			}

			if (PathType == PathTypes.linear)
			{
				if (moveTo <= 0)
				{
					movementDirection = 1;
				}
				else if (moveTo >= PathElements.Length - 1)
				{
					movementDirection = -1;
				}
			}

			moveTo += movementDirection;

			if (PathType == PathTypes.loop)
			{
				if (moveTo >= PathElements.Length)
				{
					moveTo = 0;
				}

				if (moveTo < 0)
				{
					moveTo = PathElements.Length - 1;
				}
			}
		}
	}

	public void OnDrawGizmos()
	{
		if(PathElements == null || PathElements.Length < 2)
		{
			return;
		}
	
		for(var i = 1; i < PathElements.Length; i++)
		{
			Gizmos.DrawLine(PathElements [i - 1].position, PathElements [i].position);
		}

		if(PathType == PathTypes.loop)
		{
			Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
		}
	}
}
