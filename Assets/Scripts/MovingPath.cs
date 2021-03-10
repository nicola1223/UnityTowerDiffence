using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPath : MonoBehaviour
{
	public enum PathTypes
	{
		linear,
		loop
	}

	public PathTypes PathType;
	public int movementDirection = 1;
	public int moveTo = 0;
	public Transform[] PathElements;

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

}
