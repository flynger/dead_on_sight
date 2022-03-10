using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Entity 
{
    public int hitPoints { get; set; }
    public int baseDamage { get; }
    public bool isHackable { get; }

    void ApplyDamage(int damage);

    void ToggleInput(bool state);
}
