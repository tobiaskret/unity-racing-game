using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public GameObject leftWheelObject;
    public GameObject rightWheelObject;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}


public class CarSimController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public Rigidbody rb;  // cars rigitbody


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Move Car
    public void MoveUpdate(float[] action)
    {
        // processing inputs
        float motor = maxMotorTorque * action[0];
        float steering = maxSteeringAngle * action[1];

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
                // break if veocity positive and motor negative
                if (rb.velocity.magnitude > 1 && motor < 0)
                {
                    axleInfo.leftWheel.brakeTorque = 500;
                    axleInfo.rightWheel.brakeTorque = 500;
                }
                else
                {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.rightWheel.brakeTorque = 0;
                }

            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftWheelObject);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightWheelObject);
        }
    }

    // correctly applies the transform to visual wheel
    public void ApplyLocalPositionToVisuals(WheelCollider collider, GameObject game_object)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        //Debug.Log(position);
        game_object.transform.position = position;
        game_object.transform.rotation = rotation; // * Quaternion.Euler(Vector3.right * 90);

    }

    // destroy this cars gameobject
    public void destroyGO()
    {
        Destroy(gameObject);
    }
}