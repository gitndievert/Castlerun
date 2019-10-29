// ********************************************************************
// CONFIDENTIAL - DO NOT DISTRIBUTE
// COPYRIGHT 2019-2020 Wacky Potato Games, LLC. All Rights Reserved.
// 
// If you send, receive, or use this file for any purpose other than
// internal use by Wacky Potato Games, it is without permission and an act of theft.
// Report any misuse of this file immediately to contact@wackypotato.com
// Misuse or failure to report misuse will subject you to legal action.
// 
// The intellectual and technical concepts contained herein are
// proprietary and are protected by trade secret and/or copyright law.
// Dissemination or reproduction of this material is forbidden.
// ********************************************************************

using Photon.Pun;
using UnityEngine;

public class BattleCursor : MonoBehaviourPun
{
    public GameObject BattleCursorObj;
    public float Distance = 0f;
    public bool CursorOn;

    private Vector3 _mousePosition;
    private Vector3 _targetPosition;    
    private GameObject _cursor;    
    
    // Start is called before the first frame update
    void Start()
    {        
        CursorOn = false;
        if (photonView.IsMine || Global.DeveloperMode)
        {
            _cursor = Instantiate(BattleCursorObj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!CursorOn) return;
        MoveCurrentObjectToMouse();
    }
      
    public void On()
    {
        MakeActive(true);
    }

    public void Off()
    {
        MakeActive(false);
    }

    public void Toggle()
    {
        CursorOn = !CursorOn;
        MakeActive(CursorOn);
    }

    private void MakeActive(bool set)
    {
        CursorOn = set;
        _cursor.SetActive(set);
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == Global.GROUND_LAYER)
            {
                //_cursor.transform.position = hit.point;
                _cursor.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y + 5f, Mathf.Round(hit.point.z));
            }
            else if(hit.transform.gameObject.layer == Global.DEFAULT_LAYER)
            {
                _cursor.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y + 5f, Mathf.Round(hit.point.z));
                //Figure out how to rotate
                //_cursor.transform.rotation = Quaternion.LookRotation(hit.point);
            }
        }
    }

    /*private float GetDistToGround()
    {
        return (_placeObjectMeshRend != null) ? _placeObjectMeshRend.bounds.extents.y : 0f;
    }*/
}
