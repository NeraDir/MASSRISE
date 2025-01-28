using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
            if (other.TryGetComponent(out EnemieHealth health))
                health.changeHealth?.Invoke(1);
    }
}
