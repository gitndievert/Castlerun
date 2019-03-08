using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePrefab : MonoBehaviour
{
    protected Renderer rend;

    protected virtual void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    protected virtual void Start()
    {

    }
}
