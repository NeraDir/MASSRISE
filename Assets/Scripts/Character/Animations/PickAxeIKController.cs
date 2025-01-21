using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PickAxeIKController : MonoBehaviour, IIKAnimation
{
    [SerializeField] private Transform _pickAxe;
    [SerializeField] private Transform _handIKBone;
    [SerializeField] private Transform _handStartTransform;
    [SerializeField] private Transform _pickAxeGrabTransform;
    [SerializeField] private Transform _pickAxeShowTransform;
    [SerializeField] private Transform _pickAxeParent;

    [SerializeField] private TwoBoneIKConstraint _handBone;

    private float moveDuration = 1f;

    private Vector3 _startTorchPosition;
    private Quaternion _startTorchRotation;

    private Coroutine _torchHandingCoroutine;

    public void Init()
    {
        _startTorchPosition = _pickAxe.localPosition;
        _startTorchRotation = _pickAxe.localRotation;
        _handBone.weight = 0;
    }

    public void TakePickAxe()
    {
        Debug.Log("handed");
        if (_torchHandingCoroutine != null)
            StopCoroutine(_torchHandingCoroutine);

        _handBone.weight = 1;
        _torchHandingCoroutine = StartCoroutine(GrabSequence());
    }

    public void HidePickAxe()
    {
        Debug.Log("hided");
        if (_torchHandingCoroutine != null)
            StopCoroutine(_torchHandingCoroutine);

        _torchHandingCoroutine = StartCoroutine(HideSequence());
    }

    private IEnumerator GrabSequence()
    {
        yield return MoveAndRotate(_handIKBone, _pickAxeGrabTransform, _pickAxeGrabTransform.rotation, moveDuration);

        AttachTorchToHand();

        yield return MoveAndRotate(_handIKBone, _pickAxeShowTransform, _pickAxeShowTransform.rotation, moveDuration);
    }

    private IEnumerator HideSequence()
    {
        yield return MoveAndRotate(_handIKBone, _pickAxeGrabTransform, _pickAxeGrabTransform.rotation, moveDuration);

        DetachTorch();

        yield return MoveAndRotate(_handIKBone, _handStartTransform, _handStartTransform.rotation, moveDuration);

        _handBone.weight = 0;
    }

    private void AttachTorchToHand()
    {
        _pickAxe.SetParent(_handIKBone);
        _pickAxe.localPosition = Vector3.zero;
        _pickAxe.localRotation = Quaternion.identity;
    }

    private void DetachTorch()
    {
        _pickAxe.SetParent(_pickAxeParent);
        _pickAxe.localPosition = _startTorchPosition;
        _pickAxe.localRotation = _startTorchRotation;
    }

    private IEnumerator MoveAndRotate(Transform target, Transform position, Quaternion rotation, float duration)
    {
        Quaternion startRotation = target.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            target.position = Vector3.Lerp(target.position, position.position, t);
            target.rotation = Quaternion.Slerp(startRotation, rotation, t);

            yield return null;
        }

        target.position = position.position;
        target.rotation = rotation;
    }
}
