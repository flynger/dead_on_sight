using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    public GameObject[] objects;
    public string effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateEffect() {
        if (effect == "toggle") {
            foreach (GameObject obj in objects) {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
