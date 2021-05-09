using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    public int car_n = 5;  // max amount of cars in the scene
    public GameObject car_prefab;
    public GameObject platform_prefab;
    public int platform_dist = 30;  // distance to neighbouring platforms
    List<GameObject> carObjectList;


    void Start()
    {
        carObjectList = new List<GameObject>();
        for (int i = 0; i < car_n; i++)
        {
            Instantiate(platform_prefab, new Vector3(platform_dist * i, 0f, 0f), new Quaternion());
            carObjectList.Add(Instantiate(car_prefab, new Vector3(platform_dist * i, 2f, 0f), new Quaternion()));
        }
    }

    // FixedUpdate called frame rate independent
    void FixedUpdate()
    {
        //for (int i = carObjectList.Count - 1; i >= 0; i--)
        for (int i=0; i < carObjectList.Count; i++)
        {
            float[] movement = new float[2];
            movement[0] = Input.GetAxis("Vertical");
            movement[1] = Input.GetAxis("Horizontal");


            carObjectList[i].GetComponent<CarSimController>().MoveUpdate(movement);
            if (carObjectList[i].GetComponent<Transform>().position.y < 0)
            {
                carObjectList[i].GetComponent<CarSimController>().destroyGO();
                carObjectList[i] = Instantiate(car_prefab, new Vector3(platform_dist * i, 2f, 0f), new Quaternion());
            }
        }
    }
}

