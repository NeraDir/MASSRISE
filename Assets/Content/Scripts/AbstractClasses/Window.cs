using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public virtual void Show()
    {

    }

    public virtual void Hide() 
    {
        _animator.SetBool("MASS_WINDOW_STATE", true);
        Invoke(nameof(DisActivate), 0.5f);
    }

    private void DisActivate()
    {
        gameObject.SetActive(false);
    }
}
