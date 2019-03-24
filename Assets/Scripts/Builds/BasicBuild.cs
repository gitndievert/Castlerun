using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuild : Build
{
    //Basic Builds are Instant
    protected override float BuildTime { get { return 0f; } }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Build")
        {
            Debug.Log(col.transform.position);
        }
    }

}
