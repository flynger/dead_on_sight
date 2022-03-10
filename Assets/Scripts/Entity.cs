using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
<<<<<<< HEAD
    public int hitPoints { get; set; }
    public int baseDamage { get; }
    public bool isHackable { get; }

    void ApplyDamage(int damage);

    void ToggleInput(bool state);
=======
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
>>>>>>> 84c7dc6204f864f695c3913deb2aa5fd1a804c96
}
