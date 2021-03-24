using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
	public Camera _camera;
	public Transform curObj;
	Vector3 pos;
	
	void Start()
	{
		curObj = transform;
	}

	void FixedUpdate () 
	{
		RaycastHit hit;
		Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
//			curObj = hit.transform;
//			curObj.position += new Vector3(0, 0.5f, 0); // немного приподымаем выбранный объект
			pos = new Vector3(hit.point.x, 0.5f, hit.point.z);
		}

		if(curObj)
		{
			curObj.position = pos;
//			Vector3 mousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.transform.position.y ));
//			var pos = ray.origin + ray.direction;
//			curObj.position = new Vector3(pos.x, curObj.position.y, pos.z);
//
		}
	}
}
