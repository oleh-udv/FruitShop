using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class IssuingStand : MonoBehaviour, ITriggerable
    {
        [SerializeField] private GameSettings _gameSettings;
        
        [Space]
        [SerializeField] private Product _productPrefab;

        [Space]
        [SerializeField] private List<ProductPoint> _productPoints;
        [SerializeField] private Transform _productContainer;

        private Pool<Product> _pool;

        private List<Transform> _freePoints;

        private Tween _waitTween;

        private bool _isFull;
        private bool _isFilling;
        private bool _playerInTrigger;

        #region UnityMethods
        private void Start()
        {
            Init();
        }
        #endregion

        #region PublicMethods
        public void TriggerEnterAction(Player player)
        {
            if (_waitTween == null)
            {
                _playerInTrigger = true;
                _waitTween = DOVirtual.DelayedCall(_gameSettings.TimeWaitInTrigger, () => GiveProductToPlayer(player));
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
        private void Init()
        {
            _pool = new Pool<Product>(_productPrefab, _productContainer, _gameSettings.CountProductPoolObjects);
            FillingStand(true);
        }

        private void FillingStand(bool fillInstantly = false)
        {
            if (_isFilling)
                return;
                
            CheckFillStand();

            if (_isFull == false)
                StartCoroutine(FillStand(fillInstantly ? 0f : _gameSettings.IssuingStandSpawnDelay));
        }

        private void CheckFillStand()
        {
            _isFull = _productPoints.Where((p) => p.HasProduct == true).ToList().Count == _productPoints.Count;
        }

        private void GiveProductToPlayer(Player player)
        {
            if (_playerInTrigger)
            {
                foreach (var point in _productPoints)
                {
                    if (point.ProductReady && player.CanTakeProducts)
                    {
                        player.TakeProduct(point.Product);
                        point.RemoveProduct();
                        _isFull = false;
                        FillingStand();
                        break;
                    }
                }
            }

            DOVirtual.DelayedCall(_gameSettings.GiveProductsDelay, () => GiveProductToPlayer(player));
        }
        #endregion

        #region Coroutines
        private IEnumerator FillStand(float delayTime)
        {
            _isFilling = true;
            yield return new WaitForSeconds(delayTime);
            foreach (var point in _productPoints)
            {
                if (point.HasProduct)
                    continue;

                var element = _pool.GetFreeElement();
                element.transform.localScale = Vector3.zero;

                point.AddProduct(element, _gameSettings.ProductScaleTime);
                yield return new WaitForSeconds(delayTime);
            }

            _isFilling = false;
            FillingStand();
        }
        #endregion
    }
}