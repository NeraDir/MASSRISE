using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sun : MonoBehaviour
{
    public static UnityEvent NightSet = new UnityEvent();
    public static UnityEvent DaySet = new UnityEvent();

    private Light _light;

    private Transform _transform;

    private const float _rotationSpeed = 30f;

    private const float _nightIntensity = 180;
    private const float _nightEndValue = 0;

    public static bool _isDay;

    public void Init()
    {
        _isDay = true;
        _light = GetComponent<Light>();
        _transform = transform;
        _transform.rotation = Quaternion.Euler(0, 84, 0);
        StartCoroutine(PeriodCycles());
    }

    private IEnumerator PeriodCycles()
    {
        while (true)
        {
            _transform.Rotate(new Vector3(1, 0, 0), _rotationSpeed * Time.deltaTime);
            float currentAngle = _transform.rotation.eulerAngles.x;
            if (currentAngle >= _nightIntensity && currentAngle < 360 && _isDay)
            {
                _light.intensity = 0;
                NightSet?.Invoke();
                _isDay = false;
            }
            else if (currentAngle >= _nightEndValue && currentAngle < _nightIntensity && !_isDay)
            {
                _light.intensity = 1;
                DaySet?.Invoke();
                _isDay = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
