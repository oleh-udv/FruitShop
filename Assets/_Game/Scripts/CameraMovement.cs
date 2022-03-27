using UnityEngine;

namespace Game
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        [Space]
        [SerializeField] private Transform _player;

        Vector3 _offset;

        #region UnityMethods
        private void Start()
        {
            _offset = transform.position - _player.position;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _player.position + _offset, Time.deltaTime * _gameSettings.CameraSpeed);
        }
        #endregion
    }
}