using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image _fillImage;

    private List<Image> _images = new List<Image>();

    private float _fillingSpeed = 0.25f;

    private FollowerComponent _followComponent;

    private Vector3 _barOffset = new Vector3(0, 2.48f, 0.58f);
    private float _barSpeed = 10;

    public void Init()
    {
        _images = GetComponentsInChildren<Image>().ToList();
        _fillImage = _images[_images.Count - 1];
        _followComponent = gameObject.AddComponent<FollowerComponent>();
    }

    public void Show()
    {
        foreach (var item in _images)
        {
            item.DOFade(1, 0.25f);
        }
    }

    public void Hide()
    {
        foreach (var item in _images)
        {
            item.DOFade(0, 0.25f);
        }
    }

    public void SetFollow(Transform target)
    {
        _followComponent.SetData(target, _barOffset, _barSpeed);
    }

    public void ChangeValue(float value, float maxValue)
    {
        UpdateVisual(value,maxValue);
    }

    public void UpdateVisual(float value,float maxValue)
    {
        _fillImage.DOFillAmount(value/maxValue, _fillingSpeed).OnComplete(() =>
        {

        });
    }
}
