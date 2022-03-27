using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enums;
using UnityEngine.AI;
using DG.Tweening;
using System;

namespace Game
{
    public class Buyer : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        [Space]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        [Space]
        [SerializeField] private Transform _productsPoint;
        [SerializeField] private Transform _productsContainer;

        [Space]
        [SerializeField] private IKManager _ik;

        private List<Product> _productsInHands = new List<Product>();
        private List<ProductType> _needProducts;

        private ReceivingStand _stand;

        private Transform _movePoint;

        private Vector3 _startPoint;

        private bool _isReturns;
        private bool _isKeepProducts;

        private bool _isCameToPoint => _agent.path != null && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

        public event Action<Buyer> OnPuthComplete;

        #region UnityMethods
        private void Update()
        {
            if (gameObject.activeSelf && _isCameToPoint)
            {
                if (_isReturns)
                    PuthComplete();
                else if(_isKeepProducts == false)
                    ReceiptProducts();
            }
        }
        #endregion

        #region PublicMethods
        public void SetRoute(List<ProductType> needProducts, ReceivingStand stand)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _isReturns = false;
            _isKeepProducts = false;

            _ik.SetIK(false);

            _needProducts = needProducts;
            _stand = stand;

            _startPoint = transform.position;

            _agent.speed = _gameSettings.BuyerSpeed;
            _agent.angularSpeed = _gameSettings.BuyerAngularSpeed;
            _agent.acceleration = _gameSettings.BuyerAcceleration;

            _movePoint = _stand.GetProductsPoint();
            _agent.SetDestination(_movePoint.position);

            _animator.SetInteger("State", 1);
        }
        #endregion

        #region PrivateMethods
        private void PuthComplete()
        {
            gameObject.SetActive(false);

            foreach (var product in _productsInHands)
            {
                product.gameObject.SetActive(false);
                _productsPoint.localPosition -= Vector3.up * product.UpOffset;
            }
            _productsInHands.Clear();

            OnPuthComplete?.Invoke(this);
        }

        private void ReceiptProducts()
        {
            _animator.SetInteger("State", 0);
            _isKeepProducts = true;
            StartCoroutine(TakeProducts());
        }
        #endregion

        #region Coroutines
        private IEnumerator TakeProducts()
        {
            _ik.SetIK(true);

            for (int i = 0; i < _needProducts.Count; i++)
            {
                yield return new WaitUntil(() => _stand.HaveProduct);

                var product = _stand.GiveProduct();
                _productsInHands.Add(product);
                product.transform.parent = _productsContainer;

                product.transform.DORotate(Vector3.zero, _gameSettings.ProductMovementTime);
                product.transform.DOLocalMove(_productsPoint.localPosition, _gameSettings.ProductMovementTime);

                _productsPoint.localPosition += Vector3.up * product.UpOffset;

                yield return new WaitForSeconds(_gameSettings.GiveProductsDelay);
            }

            _agent.SetDestination(_startPoint);
            _isReturns = true;
            _animator.SetInteger("State", 1);
            _stand.ReleasePoint(_movePoint);

            MoneyCounter.Instance.UpdateMoney(_productsInHands.Count);
        }
        #endregion
    }
}
