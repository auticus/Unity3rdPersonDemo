namespace Unity3rdPersonDemo.StateMachine.States
{
    public interface IGameState
    {
        void Enter();
        void Tick(float deltaTime);
        void Exit();
    }

}