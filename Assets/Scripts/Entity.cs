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
            GetComponent<ThirdPersonController>().enabled = state;
            //GetComponent<StarterAssetsInputs>().enabled = state;
            // controller.Assign();
            controller.GetRawInput().enabled = state;
        }

        public void ToggleAI(bool state)
        {
            if (hasAI)
            {
                GetComponent<AIScript>().enabled = state;
                GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = state;
            }
        }
    }
}
