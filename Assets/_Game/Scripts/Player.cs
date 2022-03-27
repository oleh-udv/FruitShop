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

        #region UnityMethods
        private void Update()
        {
            if (Input.GetMouseButton(0))
                Move(GetCurrentJoystickDirection());
            else if(_isMove)
                Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.gameObject.GetComponent<ITriggerable>();
            if (obj != null)
                obj.TriggerEnterAction(this);
        }

        private void OnTriggerExit(Collider other)
        {
            var obj = other.gameObject.GetComponent<ITriggerable>();
            if (obj != null)
                obj.TriggerExitAction(this);
        }
        #endregion

        #region PrivateMethods
        private Vector3 GetCurrentJoystickDirection()
        {
            Vector3 direction = _joystick.Direction;
            direction.z = direction.y;
            direction.y = 0;
            return direction;
        }

        private void Move(Vector3 direction)
        {
            _agent.speed = _gameSettings.PlayerSpeed;
            _agent.angularSpeed = _gameSettings.PlayerAngularSpeed;
            _agent.acceleration = _gameSettings.PlayerAcceleration;

            _agent.SetDestination(transform.localPosition + direction);

            if (_isMove == false)
            {
                _animator.SetInteger("State", 1);
                _isMove = true;
            }
        }

        private void Stop()
        {
            _animator.SetInteger("State", 0);
            _agent.SetDestination(transform.localPosition);
            _isMove = false;
        }
        #endregion
    }
}
