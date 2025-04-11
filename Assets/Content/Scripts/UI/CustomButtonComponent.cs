using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButtonComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _closePage;
    [SerializeField] private Animator _openPage;

    [SerializeField] private UnityEvent _completeEvent;

    public static bool isClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
            return;
        isClicked = true;
        StartCoroutine(OnClick());
    }

    private IEnumerator OnClick()
    {
        if (_closePage != null)
            _closePage.SetBool("ui_panel_state", true);
        yield return new WaitForSecondsRealtime(0.5f);
        if (_openPage != null)
            _openPage.gameObject.SetActive(true);
        if(_closePage != null)
            _closePage.gameObject.SetActive(false);
        _completeEvent?.Invoke();
        isClicked = false;
    }
}
