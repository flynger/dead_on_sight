using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject activeSprite;
    public GameObject cooldownSprite;
    public float cooldownSec;
    public string effect;
    public bool isOneTimeUse;
    public bool touchActivated;
    
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
        StartCoroutine(offOnCooldown());
        if (isOneTimeUse) {
            GetComponent<Activate>().enabled = false;
        }
    }

    IEnumerator offOnCooldown() {
        cooldownSprite.SetActive(true);
        activeSprite.SetActive(false);
        yield return new WaitForSeconds(cooldownSec);
        cooldownSprite.SetActive(false);
        activeSprite.SetActive(true);
    }

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("enemy") && touchActivated) {
            activateEffect();
        }
    }
}
