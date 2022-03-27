using System.Collections;
using System.Collections.Generic;
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

        private bool _isFill;

        #region UnityMethods
        private void Start()
        {
            Init();
        }
        #endregion

        #region PublicMethods
        public void TriggerEnterAction(Player player)
        {
        }

        public void TriggerExitAction(Player player)
        {
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
            CheckFillStand();

            if (_isFill == false)
                StartCoroutine(FillStand(fillInstantly ? 0f : _gameSettings.IssuingStandSpawnDelay));
        }

        private void CheckFillStand()
        {
            foreach (var point in _productPoints)
            {
                if (!point.HasProduct)
                {
                    _isFill = false;
                    return;
                }
            }

            _isFill = true;
        }
        #endregion

        #region Coroutines
        private IEnumerator FillStand(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            foreach (var point in _productPoints)
            {
                if (point.HasProduct)
                    continue;

                point.AddProduct(_pool.GetFreeElement(), _gameSettings.ProductScaleTime);
                yield return new WaitForSeconds(delayTime);
            }
        }
        #endregion
    }
}