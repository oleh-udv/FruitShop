using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        [Space]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private FloatingJoystick _joystick;

        [Space]
        [SerializeField] private Animator _animator;

        [Space]
        [SerializeField] private Transform _productsPoint;
        [SerializeField] private Transform _productsContainer;

        [Space]
        [SerializeField] private IKManager _ik;

        private List<Product> _productsInHands = new List<Product>();
        private bool _isMove;

        public bool CanTakeProducts => _productsInHands.Count < _gameSettings.MaxProductInHands;
        public bool HaveProduct => _productsInHands.Count > 0;

        #region UnityMethods
        private void Update()
        {
            if (Input.GetMouseButton(0))
                Move(GetCurrentJoystickDirection());
            else if(_isMove)
                Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.gameObject.GetComponent<ITriggerable>();
            if (obj != null)
                obj.TriggerEnterAction(this);
        }

        private void OnTriggerExit(Collider other)
        {
            var obj = other.gameObject.GetComponent<ITriggerable>();
            if (obj != null)
                obj.TriggerExitAction(this);
        }
        #endregion

        #region PublicMethods
        public void TakeProduct(Product product)
        {
            if (CanTakeProducts == false)
                return;

            _ik.SetIK(true);

            _productsInHands.Add(product);
            product.transform.parent = _productsContainer;

            product.transform.DORotate(Vector3.zero, _gameSettings.ProductMovementTime);
            product.transform.DOLocalMove(_productsPoint.localPosition, _gameSettings.ProductMovementTime);

            _productsPoint.localPosition += Vector3.up * product.UpOffset;
        }

        public Product GetLastProduct()
        {
            if (HaveProduct)
            {
                var product = _productsInHands[_productsInHands.Count - 1];
                _productsInHands.RemoveAt(_productsInHands.Count - 1);

                _productsPoint.localPosition -= Vector3.up * product.UpOffset;

                if (_productsInHands.Count == 0)
                    _ik.SetIK(false);

                return product;
            }
            throw new System.Exception("Index out of range");
        }
        #endregion

        #region PrivateMethods
        private Vector3 GetCurrentJoystickDirection()
        {
            Vector3 direction = _joystick.Direction;
            direction.z = direction.y;
            direction.y = 0;
            return direction;
        }

        private void Move(Vector3 direction)
        {
            _agent.speed = _gameSettings.PlayerSpeed;
            _agent.angularSpeed = _gameSettings.PlayerAngularSpeed;
            _agent.acceleration = _gameSettings.PlayerAcceleration;

            _agent.SetDestination(transform.localPosition + direction);

            if (_isMove == false)
            {
                _animator.SetInteger("State", 1);
                _isMove = true;
            }
        }

        private void Stop()
        {
            _animator.SetInteger("State", 0);
            _agent.SetDestination(transform.localPosition);
            _isMove = false;
        }
        #endregion
    }
}
