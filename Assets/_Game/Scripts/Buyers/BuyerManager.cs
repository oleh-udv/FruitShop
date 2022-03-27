using DG.Tweening;
using Game.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class BuyerManager : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        [Space]
        [SerializeField] private Buyer _buyerPrefab;
        [SerializeField] private Transform _buyerContainer;

        [Space]
        [SerializeField] private List<ReceivingStand> _stands;

        [Space]
        [SerializeField] private ParticleSystem _smoke;

        private List<Buyer> _activeBuyers = new List<Buyer>();
        private Pool<Buyer> _pool;

        private bool _canSpawnBuyer;

        #region UnityMethods
        private void Start()
        {
            _pool = new Pool<Buyer>(_buyerPrefab, _buyerContainer, _gameSettings.CountBuyersPoolObjects);
            _canSpawnBuyer = true;

            DOVirtual.DelayedCall(_gameSettings.BuyerSpawnDelay, () =>
            {
                _canSpawnBuyer = true;
                AddBuyer();
            });
        }
        #endregion

        #region PrivateMethods
        private void AddBuyer()
        {
            if (_activeBuyers.Count >= _gameSettings.MaxBuyers || _canSpawnBuyer == false)
                return;

            _canSpawnBuyer = false;

            var buyer = _pool.GetFreeElement();
            buyer.gameObject.SetActive(true);
            _activeBuyers.Add(buyer);

            int countProducts = Random.Range(1, _gameSettings.MaxProductInHands + 1);
            List<ProductType> needProducts = new List<ProductType>(countProducts);

            var type = ProductType.Apple;

            for (int i = 0; i < countProducts; i++)
                needProducts.Add(type);

            var stand = _stands.Where(s => s.TypeProduct == type).ToList()[0];

            buyer.SetRoute(needProducts, stand);

            buyer.OnPuthComplete += ClearInactiveBuyer;

            _smoke.Play();

            DOVirtual.DelayedCall(_gameSettings.BuyerSpawnDelay, () => 
            {
                _canSpawnBuyer = true;
                AddBuyer();
            });
        }

        private void ClearInactiveBuyer(Buyer buyer)
        {
            _activeBuyers.Remove(buyer);
            AddBuyer();

            _smoke.Play();
            buyer.OnPuthComplete -= ClearInactiveBuyer;
        }
        #endregion
    }
}