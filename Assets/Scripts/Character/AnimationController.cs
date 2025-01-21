using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private IIKAnimation[] _animations;

    private PickAxeIKController _pickAxeIKController;
    private MiningIKAnimation _miningIKAnimation;

    [Inject]
    public void Init() 
    {
        _animations = GetComponents<IIKAnimation>();
        foreach (var item in _animations)
        {
            item.Init();
        }
       
        _animator = GetComponent<Animator>();
        _pickAxeIKController = GetComponent<PickAxeIKController>();
        _miningIKAnimation = GetComponent<MiningIKAnimation>();
    }

    public void SetAnimationState(string anima = "",int value = 0)
    {
        if (_animator == null)
            return;
        _animator.SetInteger(anima, value);
    }

    public void Mining()
    {
        _miningIKAnimation.StartMiningSequence();
    }

    public void StopMining()
    {
        _miningIKAnimation.StopMiningSequence();
    }

    public void TakePickAxe()
    {
        _pickAxeIKController.TakePickAxe();
    }

    public void HidePickAxe()
    {
        _pickAxeIKController.HidePickAxe();
    }
}
