using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Init(float maxHealth = 0, Material damage = null)
    {
        base.Init(maxHealth, damage);
    }

    public override void OnDeadComplete()
    {
        base.OnDeadComplete();  
    }
}
