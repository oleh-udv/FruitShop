using System.Collections;
using System.Collections.Generic;
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

        private bool _canMove;

        private void Start()
        {
            _canMove = true;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && _canMove)
            {
                Vector3 direction = _joystick.Direction;
                direction.z = direction.y;
                direction.y = 0;
                Move(direction);
            }
        }

        private void Move(Vector3 direction)
        {
            _agent.speed = _gameSettings.PlayerSpeed;
            _agent.angularSpeed = _gameSettings.PlayerAngularSpeed;
            _agent.acceleration = _gameSettings.PlayerAcceleration;

            _agent.SetDestination(transform.localPosition + direction);
        }
    }
}
