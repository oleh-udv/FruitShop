using DG.Tweening;
using Game.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ReceivingStand : MonoBehaviour, ITriggerable
    {
        [SerializeField] private GameSettings _gameSettings;

        [SerializeField] private ProductType _typeProduct; 
        [SerializeField] private List<ProductPoint> _productPoints;

        private Tween _waitTween;

        private int _activePointIndex;

        private bool _playerInTrigger;

        private bool IsFull => _activePointIndex == _productPoints.Count;

        #region PublicMethods
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
