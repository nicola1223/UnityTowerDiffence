using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
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

			if (moveTo <= 0)
			{
				movementDirection = 1;
			}
			if (moveTo == PathElements.Length - 1)
			{
				movementDirection = 0;
			}

			moveTo += movementDirection;
		}
	}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
