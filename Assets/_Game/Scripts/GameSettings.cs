using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        #region Fields
        [Header("PlayerSettings")]
        [Range(5f, 30f)]
        [SerializeField] private float _playerSpeed;
        [Range(50f, 500f)]
        [SerializeField] private float _playerAngularSpeed;
        [Range(5f, 30f)]
        [SerializeField] private float _playerAcceleration;
        [Range(0f, 3f)]
        [SerializeField] private float _timeWaitInTrigger;
        [Range(1, 10)]
        [SerializeField] private int _maxProductInHands;

        [Header("BuyerSettings")]
        [Range(5f, 30f)]
        [SerializeField] private float _buyerSpeed;
        [Range(50f, 500f)]
        [SerializeField] private float _buyerAngularSpeed;
        [Range(5f, 30f)]
        [SerializeField] private float _buyerAcceleration;
        [Range(5f, 100f)]
        [SerializeField] private float _buyerSpawnDelay;
        [Range(1, 5)]
        [SerializeField] private int _maxBuyers;

        [Header("CameraSettings")]
        [Min(3f)]
        [SerializeField] private float _cameraSpeed;

        [Header("Stands")]
        [Range(0f, 10f)]
        [SerializeField] private float _issuingStandSpawnDelay;
        [Range(0.1f, 1f)]
        [SerializeField] private float _giveProductsDelay;
        [Range(0.1f, 1f)]
        [SerializeField] private float _takeProductsDelay;

        [Header("Pool")]
        [Range(1, 100)]
        [SerializeField] private int _countProductPoolObjects;
        [Range(1, 100)]
        [SerializeField] private int _countBuyersPoolObjects;

        [Header("Animations")]
        [Range(0f, 1f)]
        [SerializeField] private float _productScaleTime;
        [Range(0f, 1f)]
        [SerializeField] private float _productMovementTime;

        [Header("UI")]
        [Range(0f, 0.5f)]
        [SerializeField] private float _punchCounterTime;
        [Range(1f, 1.5f)]
        [SerializeField] private float _counterImpulse;
        [Range(0f, 0.5f)]
        [SerializeField] private float _counterValueChangeTime;
        #endregion

        #region Getters
        //PlayerSettings
        public float PlayerSpeed => _playerSpeed;
        public float PlayerAngularSpeed => _playerAngularSpeed;
        public float PlayerAcceleration => _playerAcceleration;
        public float TimeWaitInTrigger => _timeWaitInTrigger;
        public int MaxProductInHands => _maxProductInHands;

        //BuyerSettings
        public float BuyerSpeed => _buyerSpeed;
        public float BuyerAngularSpeed => _buyerAngularSpeed;
        public float BuyerAcceleration => _buyerAcceleration;
        public float BuyerSpawnDelay => _buyerSpawnDelay;
        public int MaxBuyers => _maxBuyers;

        //CameraSettings
        public float CameraSpeed => _cameraSpeed;

        //Stands
        public float IssuingStandSpawnDelay => _issuingStandSpawnDelay;
        public float GiveProductsDelay => _giveProductsDelay;
        public float TakeProductsDelay => _takeProductsDelay;

        //Pool
        public int CountProductPoolObjects => _countProductPoolObjects;
        public int CountBuyersPoolObjects => _countBuyersPoolObjects;

        //Animations
        public float ProductScaleTime => _productScaleTime;
        public float ProductMovementTime => _productMovementTime;

        //UI
        public float PunchCounterTime => _punchCounterTime;
        public float CounterImpulse => _counterImpulse;
        public float CounterValueChangeTime => _counterValueChangeTime;
        #endregion
    }
}