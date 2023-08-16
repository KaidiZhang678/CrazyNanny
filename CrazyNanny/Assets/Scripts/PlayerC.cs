//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerC: MonoBehaviour
//{
//    public GameObject player;
//    private Vector3 offset;

//    // Start is called before the first frame update
//    void Start()
//    {
//        offset=transform.position - player.transform.position; /*camera position minus player position*/
//    }

//    // LateUpdate is called after all the other updates are done once per frame
//    void LateUpdate() 
//    {
//        transform.position=player.transform.position + offset; /*before the frame displayed, the camera and player move to a new position*/
//    }
//}

using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
    public float positionSmoothTime = 1f;		// a public variable to adjust smoothing of camera motion
    public float rotationSmoothTime = 1f;
    public float positionMaxSpeed = 50f;        //max speed camera can move
    public float rotationMaxSpeed = 50f;
    public Transform desiredPose;			// the desired pose for the camera, specified by a transform in the game
    public Transform target;
    public GameObject player;

    protected Vector3 currentPositionCorrectionVelocity;
    //protected Vector3 currentFacingCorrectionVelocity;
    //protected float currentFacingAngleCorrVel;
    protected Quaternion quaternionDeriv;

    protected float angle;

    void Start()
    {
        // offset = transform.position - player.transform.position; /*camera position minus player position*/
        desiredPose = player.transform.Find("CamPos");
        target = player.transform;
    }

    void LateUpdate()
    {

        if (desiredPose != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPose.position, ref currentPositionCorrectionVelocity, positionSmoothTime, positionMaxSpeed, Time.deltaTime);

            var targForward = desiredPose.forward;
            //var targForward = (target.position - this.transform.position).normalized;

            transform.rotation = QuaternionUtil.SmoothDamp(transform.rotation,
                Quaternion.LookRotation(targForward, Vector3.up), ref quaternionDeriv, rotationSmoothTime);

        }
    }
}