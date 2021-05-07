/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class SimAxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public GameObject leftWheelObject;
    public GameObject rightWheelObject;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
[System.Serializable]
public class CarSimController : GameBehavior
{
    public List<SimAxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public Rigidbody rb;  // cars rigitbody
    public GameObject car_object;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveUpdate(float[] input_array)
    {
        // check if car is falling
        if (car_object.transform.position.y < 0)
        {
            Destroy(gameObject);
        }
        input_array[0] = 100;
        // processing inputs
        float motor = maxMotorTorque * input_array[0];
        float steering = maxSteeringAngle * input_array[1];  // Input.GetAxis("Horizontal");
        foreach (SimAxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                Debug.Log("MOTOR" + motor);
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

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, GameObject game_object)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        Debug.Log(game_object.transform.position);
        game_object.transform.position = position;
        game_object.transform.rotation = rotation; // * Quaternion.Euler(Vector3.right * 90);
    }




}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public GameObject leftWheelObject;
    public GameObject rightWheelObject;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}*/

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
    // Update is called once per frame
    public void MoveUpdate()
    {
        // processing inputs
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

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

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, GameObject game_object)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        //Debug.Log(position);
        game_object.transform.position = position;
        game_object.transform.rotation = rotation; // * Quaternion.Euler(Vector3.right * 90);

    }

    public float getPos()
    {
        return rb.velocity.x;// transform.position.y;
    }

    public void destroyGO()
    {
        Destroy(gameObject);
    }


}