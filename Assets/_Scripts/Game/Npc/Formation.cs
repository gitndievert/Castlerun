using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Formation : MonoBehaviour
{
    public bool IsTouching = false;

    private Collider _formationCollider;

    private void Awake()
    {
        _formationCollider = GetComponent<Collider>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name != "FormationsCollider") return;
        IsTouching = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsTouching = false;
    }
}
