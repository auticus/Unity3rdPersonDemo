using Unity3rdPersonDemo.StateMachine.States;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        private IGameState _currentState;
        public Transform EntityTransform => this.transform;

        private void Update()
        {
            _currentState?.Tick(Time.deltaTime);                
        }

        public void SwitchState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}
