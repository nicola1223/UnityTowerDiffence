using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
	public LayerMask lm;

    void Start()
    {
        
    }

    void Update()
    {
    	RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, lm))
		{
			transform.position = hit.point;
		}
    }
}
