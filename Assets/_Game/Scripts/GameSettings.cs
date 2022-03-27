using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("PlayerSettings")]
        [Range(5, 30)]
        [SerializeField] private float _playerSpeed;
        [Range(50, 500)]
        [SerializeField] private float _playerAngularSpeed;
        [Range(5, 30)]
        [SerializeField] private float _playerAcceleration;

        [Header("CameraSettings")]
        [Min(3)]
        [SerializeField] private float _cameraSpeed;

        [Header("IssuingStand")]
        [Range(0, 10)]
        [SerializeField] private float _issuingStandSpawnDelay;
        [Range(0, 1)]
        [SerializeField] private float _productScaleTime;

        [Header("Pool")]
        [Range(1, 100)]
        [SerializeField] private int _countProductPoolObjects;

        public float PlayerSpeed => _playerSpeed;
        public float PlayerAngularSpeed => _playerAngularSpeed;
        public float PlayerAcceleration => _playerAcceleration;

        public float CameraSpeed => _cameraSpeed;

        public float IssuingStandSpawnDelay => _issuingStandSpawnDelay;
        public float ProductScaleTime => _productScaleTime;

        public int CountProductPoolObjects => _countProductPoolObjects;
    }
}