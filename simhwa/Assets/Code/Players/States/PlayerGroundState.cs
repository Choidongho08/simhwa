using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Entities.FSM;
using Code.Entities;
using Code.Players;

namespace Assets.Code.Players.States
{
    public abstract class PlayerGroundState : EntityState
    {
        protected Player _player;
        protected EntityMover _mover;
        protected PlayerGroundState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnJumpKeyPressed += HandleJumpKeyPressed;
        }

        public override void Update()
        {
            base.Update();
            if (_mover.IsGroundDetected() == false)
            {
                _player.ChangeState("FALL");
            }
        }

        public override void Exit()
        {
            _player.PlayerInput.OnJumpKeyPressed -= HandleJumpKeyPressed;
            base.Exit();
        }

        private void HandleJumpKeyPressed()
        {
            if(_mover.IsGroundDetected())
                _player.ChangeState("JUMP");
        }
    }
}
