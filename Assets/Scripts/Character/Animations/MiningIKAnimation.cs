using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MiningIKAnimation : MonoBehaviour, IIKAnimation
{
    [SerializeField] private Transform _handIKBone;
    [SerializeField] private Transform[] positions;
    [SerializeField] private TwoBoneIKConstraint _handBone;
    [SerializeField] private PickAxeComponent _pickAxe;

    private Coroutine _miningCoroutine;
    private float moveDuration = 0.2f;

    public void Init()
    {
        
    }

    private void OnDisable()
    {
        if (_miningCoroutine != null)
            StopCoroutine(_miningCoroutine);
    }

    public void StartMiningSequence()
    {
        if (_miningCoroutine != null)
            StopCoroutine(_miningCoroutine);

        _miningCoroutine = StartCoroutine(MiningSequence());
    }

    public void StopMiningSequence()
    {
        if (_miningCoroutine != null)
            StopCoroutine(_miningCoroutine);
        _pickAxe.SetMiningState(false);
        _handBone.weight = 0;
    }

    private IEnumerator MiningSequence()
    {
        while (true)
        {
            yield return MoveToPosition(positions[0]);
            yield return MoveToPosition(positions[1]);
           
            yield return MoveToPosition(positions[2]);
            _pickAxe.SetMiningState(true);
            _pickAxe.Mine();

            yield return MoveToPosition(positions[1]);
            _pickAxe.SetMiningState(false);
        }
    }

    private IEnumerator MoveToPosition(Transform targetPosition)
    {
        Vector3 startPos = _handIKBone.position;
        Quaternion startRot = _handIKBone.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            _handIKBone.position = Vector3.Lerp(startPos, targetPosition.position, t);
            _handIKBone.rotation = Quaternion.Slerp(startRot, targetPosition.rotation, t);

            yield return null;
        }

        _handIKBone.position = targetPosition.position;
        _handIKBone.rotation = targetPosition.rotation;
    }
}
