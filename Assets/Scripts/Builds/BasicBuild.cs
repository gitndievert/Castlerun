using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuild : Build
{


    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Build")
        {
            Debug.Log(col.transform.position);
        }
    }

}
