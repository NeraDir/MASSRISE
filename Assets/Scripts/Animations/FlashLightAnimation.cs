using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FlashLightAnimation : MonoBehaviour, IKAnimation
{
    [SerializeField] private Transform _torch;
    [SerializeField] private Transform _handIKBone;
    [SerializeField] private Transform _handStartTransform;
    [SerializeField] private Transform _torchGrabTransform;
    [SerializeField] private Transform _torchShowTransform;
    [SerializeField] private Transform _torchParent;
    [SerializeField] private ParticleSystem _torchEffect;

    [SerializeField] private TwoBoneIKConstraint _handBone;

    private float moveDuration = 1f;

    private Vector3 _startTorchPosition;
    private Quaternion _startTorchRotation;

    private Coroutine _torchHandingCoroutine;

    public void Init()
    {
        _handBone.weight = 0;

        _startTorchPosition = _torch.localPosition;
        _startTorchRotation = _torch.localRotation;
    }

    private void OnEnable()
    {
        Sun.NightSet.AddListener(OnNightStart);
        Sun.DaySet.AddListener(OnDayStart);
        if (Sun._isDay)
        {
            OnDayStart();
        }
        else
        {
            OnNightStart();
        }
    }

    private void OnDisable()
    {
        Sun.NightSet.RemoveListener(OnNightStart);
        Sun.DaySet.RemoveListener(OnDayStart);
    }

    private void OnNightStart()
    {
        if (_torchHandingCoroutine != null)
            StopCoroutine(_torchHandingCoroutine);

        _handBone.weight = 1;
        _torchHandingCoroutine = StartCoroutine(NightSequence());
    }

    private void OnDayStart()
    {
        if (_torchHandingCoroutine != null)
            StopCoroutine(_torchHandingCoroutine);

        _torchHandingCoroutine = StartCoroutine(DaySequence());
    }

    private IEnumerator NightSequence()
    {
        yield return MoveAndRotate(_handIKBone, _torchGrabTransform, _torchGrabTransform.rotation, moveDuration);

        AttachTorchToHand();

        yield return MoveAndRotate(_handIKBone, _torchShowTransform, _torchShowTransform.rotation, moveDuration);

        _torchEffect.gameObject.SetActive(true);
    }

    private IEnumerator DaySequence()
    {
        yield return MoveAndRotate(_handIKBone, _torchGrabTransform, _torchGrabTransform.rotation, moveDuration);

        _torchEffect.gameObject.SetActive(false);

        DetachTorch();

        yield return MoveAndRotate(_handIKBone, _handStartTransform, _handStartTransform.rotation, moveDuration);

        _handBone.weight = 0;
    }

    private void AttachTorchToHand()
    {
        _torch.SetParent(_handIKBone);
        _torch.localPosition = Vector3.zero;
        _torch.localRotation = Quaternion.identity;
    }

    private void DetachTorch()
    {
        _torch.SetParent(_torchParent);
        _torch.localPosition = _startTorchPosition;
        _torch.localRotation = _startTorchRotation;
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
