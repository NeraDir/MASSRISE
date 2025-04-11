using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartup : MonoBehaviour
{
    [SerializeField] private UISettings _uiSettings;
    [SerializeField] private Canvas _canvas;

    public void Init()
    {
        Canvas newCanvas = Instantiate(_canvas);
    }
}
