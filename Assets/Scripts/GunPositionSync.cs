using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunPositionSync : NetworkBehaviour {

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _handMount;
    [SerializeField] private Transform _gunPivot;
    [SerializeField] private Transform _rightHandHold;
    [SerializeField] private Transform _leftHandHold;
    [SerializeField] private float _threshHold = 10f;
    [SerializeField] private float _smoothing = 5f;

    [SyncVar] private float _pitch;
    private Vector3 _lastOffset;
    private float _lastSyncedPitch;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (isLocalPlayer)
            _gunPivot.parent = _cameraTransform;
        else
            _lastOffset = _handMount.position - transform.position;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            _pitch = _cameraTransform.localRotation.eulerAngles.x;
            if (Mathf.Abs(_lastSyncedPitch - _pitch) >= _threshHold)
            {
                CmdUpdatePitch(_pitch);
                _lastSyncedPitch = _pitch;
            }
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(_pitch, 0f, 0f);
            Vector3 currentOffset = _handMount.position - transform.position;
            _gunPivot.localPosition += currentOffset - _lastOffset;
            _lastOffset = currentOffset;

            _gunPivot.localRotation = Quaternion.Lerp(_gunPivot.localRotation, newRotation, Time.deltaTime * _smoothing);
        }
    }

    [Command]
    void CmdUpdatePitch(float newPitch)
    {
        _pitch = newPitch;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!_animator)
            return;

        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandHold.position);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandHold.rotation);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandHold.position);
        _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandHold.rotation);

    }

}
