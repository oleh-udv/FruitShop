using DG.Tweening;
using Game.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ReceivingStand : MonoBehaviour, ITriggerable
    {
        [SerializeField] private GameSettings _gameSettings;

        [SerializeField] private ProductType _typeProduct; 
        [SerializeField] private List<ProductPoint> _productPoints;

        [Space]
        [SerializeField] private List<Transform> _getProductsPoints;

        private List<Transform> _freeGetProductsPoints = new List<Transform>();
        private Tween _waitTween;

        private int _activePointIndex;

        private bool _playerInTrigger;

        private bool IsFull => _activePointIndex == _productPoints.Count;

        public ProductType TypeProduct => _typeProduct;

        public bool HaveProduct => _activePointIndex > 0;

        #region UnityMethods
        private void Start()
        {
            _freeGetProductsPoints.AddRange(_getProductsPoints);
        }
        #endregion

        #region PublicMethods
        public Product GiveProduct()
        {
            if (HaveProduct == false)
                throw new System.Exception("Index out of range");

            var point = _productPoints[_activePointIndex - 1];
            var product = point.Product;

            point.RemoveProduct();
            _activePointIndex--;

            return product;
        }

        public Transform GetProductsPoint()
        {
            var point = _freeGetProductsPoints[0];
            _freeGetProductsPoints.Remove(point);
            return point;
        }

        public void ReleasePoint(Transform point)
        {
            _freeGetProductsPoints.Add(point);
        }

        public void TriggerEnterAction(Player player)
        {
            if (_waitTween == null)
            {
                _playerInTrigger = true;
                _waitTween = DOVirtual.DelayedCall(_gameSettings.TimeWaitInTrigger, () => TakeProduct(player));
            }
        }

        public void TriggerExitAction(Player player)
        {
            _playerInTrigger = false;

            _waitTween?.Kill(false);
            _waitTween = null;
        }
        #endregion

        #region PrivateMethods
        private void TakeProduct(Player player)
        {
            if (IsFull || player.HaveProduct == false || _playerInTrigger == false)
                return;

            Product product = player.GetLastProduct();
            if (product.Type != _typeProduct)
                return;

            product.transform.parent = _productPoints[_activePointIndex].transform;
            product.transform.DOLocalMove(Vector3.zero, _gameSettings.ProductMovementTime).OnComplete(() => 
            {
                _productPoints[_activePointIndex].AddProduct(product);
                _activePointIndex++;
                DOVirtual.DelayedCall(_gameSettings.TakeProductsDelay, () => TakeProduct(player));
            });
        }
        #endregion
    }
}
