using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/GameSettings")]
    public class GameSettings : ScriptableObject
    {
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

        [Header("Animations")]
        [Range(0f, 1f)]
        [SerializeField] private float _productScaleTime;
        [Range(0f, 1f)]
        [SerializeField] private float _productMovementTime;
        [Range(0f, 0.5f)]
        [SerializeField] private float _productPunchTime;
        [Range(0f, 0.5f)]
        [SerializeField] private float _productScaleImpulse;

        //PlayerSettings
        public float PlayerSpeed => _playerSpeed;
        public float PlayerAngularSpeed => _playerAngularSpeed;
        public float PlayerAcceleration => _playerAcceleration;
        public float TimeWaitInTrigger => _timeWaitInTrigger;
        public float MaxProductInHands => _maxProductInHands;

        //CameraSettings
        public float CameraSpeed => _cameraSpeed;

        //Stands
        public float IssuingStandSpawnDelay => _issuingStandSpawnDelay;
        public float GiveProductsDelay => _giveProductsDelay;
        public float TakeProductsDelay => _takeProductsDelay;

        //Pool
        public int CountProductPoolObjects => _countProductPoolObjects;

        //Animations
        public float ProductScaleTime => _productScaleTime;
        public float ProductMovementTime => _productMovementTime;
        public float ProductPunchTime => _productPunchTime;
        public float ProductScaleImpulse => _productScaleImpulse;
    }
}