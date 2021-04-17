using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_Enemy : MonoBehaviour
{

	public int lifes = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifes == 0){
        	Destroy(gameObject);
        }
    }
}
