using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInput : MonoBehaviour
{
    public static UnityEvent<KeyCode> onSendKeyCode = new UnityEvent<KeyCode>();

    private void Update()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                onSendKeyCode?.Invoke(key);
            }
        }
    }
}
