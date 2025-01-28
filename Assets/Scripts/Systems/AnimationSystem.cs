using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    private Animator _animator;

    private IKAnimation[] _animations;

    public void Init()
    {
        _animations = GetComponents<IKAnimation>();
        foreach (var item in _animations)
        {
            item.Init();
        }

        _animator = SetupAnimator();
    }

    public void SetAnimationState(string anima = "", int value = 0)
    {
        if (_animator == null)
            return;
        _animator.SetInteger(anima, value);
    }

    private Animator SetupAnimator()
    {
        Animator animator = GetComponent<Animator>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        return animator;
    }
}
