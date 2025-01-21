using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.TryGetComponent(out EnemieHealth enemie))
            {
               enemie.changeHealth?.Invoke(1);
            }
        }
    }
}
