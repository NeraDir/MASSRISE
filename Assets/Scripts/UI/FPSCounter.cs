using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TMP_Text _text;

    private float _timer;
    private int _counter;

    private int _fps;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _counter++;

        if (_timer >= 1.0f) 
        {
            _fps = _counter;
            _counter = 0;
            _timer = 0.0f;

            _text.text = "FPS: " + _fps.ToString();
        }
    }
}
