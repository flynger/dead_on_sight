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

        public void ApplyDamage(int damage)
        {
            hitPoints -= damage;
        }

        public void ToggleInput(bool state)
        {
            controller._rawInput.enabled = state;
        }
    }
}
