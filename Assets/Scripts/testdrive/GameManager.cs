using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CamInfo
{
    public Camera camera;
    public bool show;
}

public class GameManager : MonoBehaviour
{
    public int car_amount = 10;
    public GameObject prefab;
    public List<CamInfo> CamList;
    List<GameObject> carPrefabList;
    // Start is called before the first frame update
    void Start()
    {
        carPrefabList = new List<GameObject>();
        prefabCreation();
        bool found = false;
        foreach (CamInfo ci in CamList)
        {
            if (ci.show && !found)
            {
                ci.camera.enabled = true;
            } else
            {
                ci.camera.enabled = false;
            }
        }
    }

    public void prefabCreation()
    {
        for (int i = 0; i < car_amount; i++)
        {
            carPrefabList.Add(prefab);
            Instantiate(carPrefabList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
