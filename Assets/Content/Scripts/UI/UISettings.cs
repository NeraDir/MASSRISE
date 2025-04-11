using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new UI Data", fileName = "New UI Data")]
public class UISettings : ScriptableObject
{
    public Window[] windows;
}