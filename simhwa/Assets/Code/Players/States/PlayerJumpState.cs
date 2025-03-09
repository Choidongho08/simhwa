using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Players.States;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerJumpState : PlayerAirState
    {
        public PlayerJumpState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.DecreaseJumpCount();
            _mover.StopImmediately(true); // 공중에서 떨어지는 힘을 리셋하고 점프 가능
            _mover.Jump();
            _mover.OnMove.AddListener(HandleVelocityChange);
        }

        public override void Exit()
        {
            _mover.OnMove.RemoveListener(HandleVelocityChange);
            base.Exit();
        }

        private void HandleVelocityChange(Vector2 velocity)
        {
            if(velocity.y < 0)
                _player.ChangeState("FALL");
        }
    }
}