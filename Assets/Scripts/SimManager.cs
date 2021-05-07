using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    public int car_n = 3;  // max amount of cars in the scene
    public GameObject car_prefab;
    public GameObject platform_prefab;
    List<GameObject> carPrefabList;
    int platform_dist = 30;
    // Start is called before the first frame update
    void Start()
    {
        carPrefabList = new List<GameObject>();
        for (int i = 0; i < car_n; i++)
        {
            Instantiate(platform_prefab, new Vector3(platform_dist * i, 0f, 0f), new Quaternion());
            carPrefabList.Add(Instantiate(car_prefab, new Vector3(platform_dist * i, 2f, 0f), new Quaternion()));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = carPrefabList.Count - 1; i >= 0; i--)
        {
            float[] movement = new float[2];
            movement[0] = Input.GetAxis("Vertical");
            movement[1] = Input.GetAxis("Horizontal");


            carPrefabList[i].GetComponent<CarSimController>().MoveUpdate();
            if (carPrefabList[i].GetComponent<Transform>().position.y < 0)
            {
                carPrefabList[i].GetComponent<CarSimController>().destroyGO();
                carPrefabList[i] = Instantiate(car_prefab, new Vector3(platform_dist * i, 2f, 0f), new Quaternion());
            }
        }
    }

}

