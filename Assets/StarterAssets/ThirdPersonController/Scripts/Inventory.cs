using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject camera;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = new Vector3(camera.transform.position.x - 0.5f , camera.transform.position.y - 0.125f, camera.transform.position.z + 1);
        Quaternion camRotation = camera.transform.rotation;
        inventory.transform.position = camPos;
        inventory.transform.rotation = camRotation;
    }
}
