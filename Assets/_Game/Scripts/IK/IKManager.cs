using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class IKManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] public Transform _rightHandObj;
        [SerializeField] public Transform _leftHandObj;
        [SerializeField] public Transform _lookPoint;

        private bool _ikActive;

        #region PublicMethods
        public void SetIK(bool active)
        {
            _ikActive = active;
        }
        #endregion

        #region PrivateMethods
        private void OnAnimatorIK()
        {
            if (_animator)
            {
                if (_ikActive)
                {
                    if (_rightHandObj != null)
                        SetBoneToPoint(AvatarIKGoal.RightHand, _rightHandObj);

                    if (_leftHandObj != null)
                        SetBoneToPoint(AvatarIKGoal.LeftHand, _leftHandObj);

                    if (_lookPoint != null)
                    {
                        _animator.SetLookAtWeight(1);
                        _animator.SetLookAtPosition(_lookPoint.position);
                    }
                }
                else
                {
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    _animator.SetLookAtWeight(0);
                }
            }
        }

        private void SetBoneToPoint(AvatarIKGoal bone, Transform point)
        {
            _animator.SetIKPositionWeight(bone, 1f);
            _animator.SetIKRotationWeight(bone, 1f);
            _animator.SetIKPosition(bone, point.position);
            _animator.SetIKRotation(bone, point.rotation);
        }
        #endregion
    }
}
