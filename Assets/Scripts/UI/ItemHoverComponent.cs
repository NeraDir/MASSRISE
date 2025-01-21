using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemHoverComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator _buttonAnimator;

    private void Start()
    {
        _buttonAnimator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonAnimator.SetInteger("button_state", 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonAnimator.SetInteger("button_state", 0);
    }
}
