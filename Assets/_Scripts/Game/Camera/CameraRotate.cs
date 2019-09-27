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

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraRotate : MonoBehaviour
{
    public static Transform target;
        
    //Camera Settings
    public float targetHeight = 1.7f;
    public float distance = 5.0f;
    public float offsetFromWall = 0.1f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public float speedDistance = 5;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -40;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float rotationDampening = 3.0f;
    public float zoomDampening = 5.0f;

    public LayerMask collisionLayers = -1;
        
    private float _xDeg = 0.0f;
    private float _yDeg = 0.0f;
    private float _currentDistance;
    private float _desiredDistance;
    private float _correctedDistance;
    private Rigidbody _rb;
    private Rigidbody _targetRigidbody;

    //Fixed Build Cam
    private float b_dist_y = 25f;
    private float b_rotation_x = 64.6f;
      
    //Auto Cam
    // How fast the rig will move to keep up with target's position
    private float m_MoveSpeed = 3;
    // How fast the rig will turn to keep up with target's rotation
    private float m_TurnSpeed = 1;
    // How fast the rig will roll (around Z axis) to match target's roll.
    private float m_RollSpeed = 0.2f;
    // Whether the rig will rotate in the direction of the target's velocity.
    private bool m_FollowVelocity = false;
    // Whether the rig will tilt (around X axis) with the target.
    private bool m_FollowTilt = true;
    // The threshold beyond which the camera stops following the target's rotation. (used in situations where a car spins out, for example)
    private float m_SpinTurnLimit = 90;
    // the minimum velocity above which the camera turns towards the object's velocity. Below this we use the object's forward direction.
    private float m_TargetVelocityLowerLimit = 4f;
    // the smoothing for the camera's rotation
    private float m_SmoothTurnTime = 0.2f;
    // The relative angle of the target and the rig from the previous frame.
    private float m_LastFlatAngle;
    // How much to turn the camera
    private float m_CurrentTurnAmount;
    // The change in the turn speed velocity
    private float m_TurnSpeedVelocityChange;
    // The roll of the camera around the z axis ( generally this will always just be up )
    private Vector3 m_RollUp = Vector3.up;
    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();        
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _xDeg = angles.x;
        _yDeg = angles.y;

        _currentDistance = distance;
        _desiredDistance = distance;
        _correctedDistance = distance;
        
        // Make the rigid body not change rotation
        _rb.freezeRotation = true;

        if (_targetRigidbody == null && target != null)
            _targetRigidbody = target.GetComponent<Rigidbody>();
    }

    public static bool BuildCamMode = false;
    
    /**
     * Camera logic on LateUpdate to only update after all character movement logic has been handled.
     */
    private void LateUpdate()
    {
        Vector3 vTargetOffset;

        // Don't do anything if target is not defined
        if (!target) return;

        FollowTarget(Time.deltaTime);

        // If either mouse buttons are down, let the mouse govern camera position
        if (GUIUtility.hotControl == 0)
        {
            //This will enable both middle and right buttons. Right will assist in camera changes, 
            //and middle will allow freelook
            if (Input.GetMouseButton(KeyBindings.MIDDLE_MOUSE_BUTTON) || Input.GetMouseButton(KeyBindings.RIGHT_MOUSE_BUTTON))
            {
                _xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                _yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
            //otherwise, ease behind the target if any of the directional keys are pressed
            else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                float targetRotationAngle = target.eulerAngles.y;
                float currentRotationAngle = transform.eulerAngles.y;
                _xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
            }           
        }


        // calculate the desired distance
        _desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(_desiredDistance) * speedDistance;
        _desiredDistance = Mathf.Clamp(_desiredDistance, minDistance, maxDistance);

        _yDeg = ClampAngle(_yDeg, yMinLimit, yMaxLimit);

        // set camera rotation
        Quaternion rotation = Quaternion.Euler(BuildCamMode ? b_rotation_x : _yDeg, _xDeg, 0);
        _correctedDistance = _desiredDistance;

        // calculate desired camera position
        vTargetOffset = new Vector3(0, -targetHeight, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * _desiredDistance + vTargetOffset);

        // check for collision using the true target's desired registration point as set by user using height
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y, target.position.z) - vTargetOffset;

        // if there was a collision, correct the camera position and calculate the corrected distance
        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers.value))
        {
            // calculate the distance from the original estimated position to the collision location,
            // subtracting out a safety "offset" distance from the object we hit.  The offset will help
            // keep the camera from being right on top of the surface we hit, which usually shows up as
            // the surface geometry getting partially clipped by the camera's front clipping plane.
            _correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetFromWall;
            isCorrected = true;
        }

        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
        _currentDistance = !isCorrected || _correctedDistance > _currentDistance ? Mathf.Lerp(_currentDistance, _correctedDistance, Time.deltaTime * zoomDampening) : _correctedDistance;

        // keep within legal limits
        _currentDistance = Mathf.Clamp(_currentDistance, minDistance, maxDistance);

        // recalculate position based on the new currentDistance
        position = target.position - (rotation * Vector3.forward * _currentDistance + vTargetOffset);

        transform.rotation = rotation;
        transform.position = new Vector3(position.x, BuildCamMode ? b_dist_y + position.y : position.y,position.z);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    private void FollowTarget(float deltaTime)
    {
        // if no target, or no time passed then we quit early, as there is nothing to do
        if (!(deltaTime > 0) || target == null) return;        

        // initialise some vars, we'll be modifying these in a moment
        var targetForward = target.forward;
        var targetUp = target.up;

        if (m_FollowVelocity && Application.isPlaying)
        {
            // in follow velocity mode, the camera's rotation is aligned towards the object's velocity direction
            // but only if the object is traveling faster than a given threshold.

            if (_targetRigidbody.velocity.magnitude > m_TargetVelocityLowerLimit)
            {
                // velocity is high enough, so we'll use the target's velocty
                targetForward = _targetRigidbody.velocity.normalized;
                targetUp = Vector3.up;
            }
            else
            {
                targetUp = Vector3.up;
            }
            m_CurrentTurnAmount = Mathf.SmoothDamp(m_CurrentTurnAmount, 1, ref m_TurnSpeedVelocityChange, m_SmoothTurnTime);
        }
        else
        {
            // we're in 'follow rotation' mode, where the camera rig's rotation follows the object's rotation.

            // This section allows the camera to stop following the target's rotation when the target is spinning too fast.
            // eg when a car has been knocked into a spin. The camera will resume following the rotation
            // of the target when the target's angular velocity slows below the threshold.
            var currentFlatAngle = Mathf.Atan2(targetForward.x, targetForward.z) * Mathf.Rad2Deg;
            if (m_SpinTurnLimit > 0)
            {
                var targetSpinSpeed = Mathf.Abs(Mathf.DeltaAngle(m_LastFlatAngle, currentFlatAngle)) / deltaTime;
                var desiredTurnAmount = Mathf.InverseLerp(m_SpinTurnLimit, m_SpinTurnLimit * 0.75f, targetSpinSpeed);
                var turnReactSpeed = (m_CurrentTurnAmount > desiredTurnAmount ? .1f : 1f);
                if (Application.isPlaying)
                {
                    m_CurrentTurnAmount = Mathf.SmoothDamp(m_CurrentTurnAmount, desiredTurnAmount,
                                                         ref m_TurnSpeedVelocityChange, turnReactSpeed);
                }
                else
                {
                    // for editor mode, smoothdamp won't work because it uses deltaTime internally
                    m_CurrentTurnAmount = desiredTurnAmount;
                }
            }
            else
            {
                m_CurrentTurnAmount = 1;
            }
            m_LastFlatAngle = currentFlatAngle;
        }

        // camera position moves towards target position:
        transform.position = Vector3.Lerp(transform.position, target.position, deltaTime * m_MoveSpeed);

        // camera's rotation is split into two parts, which can have independend speed settings:
        // rotating towards the target's forward direction (which encompasses its 'yaw' and 'pitch')
        if (!m_FollowTilt)
        {
            targetForward.y = 0;
            if (targetForward.sqrMagnitude < float.Epsilon)
            {
                targetForward = transform.forward;
            }
        }
        var rollRotation = Quaternion.LookRotation(targetForward, m_RollUp);

        // and aligning with the target object's up direction (i.e. its 'roll')
        m_RollUp = m_RollSpeed > 0 ? Vector3.Slerp(m_RollUp, targetUp, m_RollSpeed * deltaTime) : Vector3.up;
        transform.rotation = Quaternion.Lerp(transform.rotation, rollRotation, m_TurnSpeed * m_CurrentTurnAmount * deltaTime);
    }
}