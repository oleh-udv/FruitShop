using Game.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        [Space]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private FloatingJoystick _joystick;

        [Space]
        [SerializeField] private Animator _animator;

        private bool _isMove;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = _joystick.Direction;
                direction.z = direction.y;
                direction.y = 0;
                Move(direction);
            }
            else if(_isMove)
            {
                _animator.SetInteger("State", 0);
                _agent.SetDestination(transform.localPosition);
                _isMove = false;
            }
        }

        private void Move(Vector3 direction)
        {
            _agent.speed = _gameSettings.PlayerSpeed;
            _agent.angularSpeed = _gameSettings.PlayerAngularSpeed;
            _agent.acceleration = _gameSettings.PlayerAcceleration;

            _agent.SetDestination(transform.localPosition + direction);

            if (!_isMove)
            {
                _animator.SetInteger("State", 1);
                _isMove = true;
            }
        }
    }
}
