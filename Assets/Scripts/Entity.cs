using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class Entity : MonoBehaviour
    {
        public GameManager gameManager;
        public ThirdPersonController controller;
        public int hitPoints = 7;
        public int baseDamage = 2;
        public bool isHackable = true;
        public bool hasAI = true;

        public void ApplyDamage(int damage)
        {
            hitPoints -= damage;
        }

        public void ToggleInput(bool state)
        {
            controller.GetRawInput().enabled = state;
            if (!state)
            {
                controller.HaltAnimations();
            }
            controller.enabled = state;
        }

        public void ToggleAI(bool state)
        {
            if (hasAI)
            {
                GetComponent<AIScript>().enabled = state;
                GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = state;
            }
        }

        public GameObject CheckVision(float distance, out GameObject obj)
        {
            // account for starting point based on entity
            Debug.DrawRay(transform.position + new Vector3(0.06f, 1.6f, 0f), transform.TransformDirection(Vector3.forward) * distance, Color.green);
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0.06f, 1.6f, 0f), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
            if (hit) obj = hit.collider.gameObject;
            else obj = null;
            return obj;
        }
    }
}
