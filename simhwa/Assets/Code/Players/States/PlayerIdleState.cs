using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Entities.FSM;
using Code.Entities;
using System;
using Assets.Code.Players.States;

namespace Code.Players.States
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(false);
        }
        public override void Update()
        {
            base.Update();

            float xInput = _player.PlayerInput.InputDirection.x;

            if(MathF.Abs(xInput)> 0)
            {
                _player.ChangeState("MOVE");
            }
        }
    }
}
