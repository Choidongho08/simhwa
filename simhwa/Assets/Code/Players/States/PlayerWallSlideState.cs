using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Entities.FSM;
using Code.Entities;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerWallSlideState : EntityState
    {
        private Player _player;
        private EntityMover _mover;

        private const float WallSlideGravityScale = 0.3f;
        public PlayerWallSlideState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(true);
            _mover.SetGarvityScale(WallSlideGravityScale);
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            if (Mathf.Abs(xInput + _renderer.FacingDirection) < 0.5f)
            {
                _player.ChangeState("FALL");
                return;
            }
            // 쭉 내려가다가 땅에 닿으면 idle로 변경
            if (_mover.IsGroundDetected() || _mover.IsWallDetected(_renderer.FacingDirection) == false)
            {
                _player.ChangeState("IDLE");
                _player.ResetJumpCount();
            }
        }

        public override void Exit()
        {
            _mover.SetGarvityScale(1f);
            base.Exit();
        }
    }
}