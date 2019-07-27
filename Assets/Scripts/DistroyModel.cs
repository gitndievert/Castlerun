using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyModel : MonoBehaviour
{
    public GameObject crackedModel;
    private void OnMouseDown()
    {
        Instantiate(crackedModel, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
