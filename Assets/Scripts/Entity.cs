using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Entity 
{
    public int hitPoints { get; set; }
    public int baseDamage { get; }

    void ApplyDamage(int damage);
}
