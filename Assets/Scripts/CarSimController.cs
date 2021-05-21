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
    void ApplyLocalPositionToVisuals(WheelCollider collider, GameObject game_object)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        //Debug.Log(position);
        game_object.transform.position = position;
        game_object.transform.rotation = rotation; // * Quaternion.Euler(Vector3.right * 90);

    }
    

    public List<float> EnvironmentPerception()
    {
        bool show_rays = true;
        int ray_length = 100;
        List<float> perception_list = new List<float> { 0, 0, 0 };

        RaycastHit hitL;
        bool left_ray = Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-0.5f, 0f, 1f)), out hitL, ray_length);
        perception_list[0] = (ray_length - hitL.distance) / ray_length;

        RaycastHit hitC;
        bool center_ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitC, ray_length);
        perception_list[1] = (ray_length - hitC.distance) / ray_length;

        RaycastHit hitR;
        bool right_ray = Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0.5f, 0f, 1f)), out hitR, ray_length);
        perception_list[2] = (ray_length - hitR.distance) / ray_length;
        //Debug.Log(perception_list[1]);

        if (show_rays)
        {
            if (left_ray)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-0.5f, 0f, 1f)) * hitL.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-0.5f, 0f, 1f)) * ray_length, Color.green);
            }

            if (center_ray)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitC.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * ray_length, Color.green);
            }

            if (right_ray)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0.5f, 0f, 1f)) * hitR.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0.5f, 0f, 1f)) * ray_length, Color.green);
            }
        }

        return perception_list;
    }

    // destroy this cars gameobject
    public void destroyGO()
    {
        Destroy(gameObject);
    }
}