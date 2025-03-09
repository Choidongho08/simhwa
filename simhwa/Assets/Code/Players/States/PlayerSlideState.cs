using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Entities.FSM;
using Code.Entities;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.Players.States
{
    public class PlayerSlideState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        private readonly Vector2 _slideOffset = new Vector2(0.039f, -0.41f);
        private readonly Vector2 _slideSize = new Vector2(0.65f, 0.82f);
        
        public PlayerSlideState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>(); 
        }

        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = false;
            _mover.AddForceToEntity(new Vector2(5 * _renderer.FacingDirection, 0));
            _mover.SetColliderSize(_slideSize,  _slideOffset);
        }

        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
                _player.ChangeState("IDLE");
        }

        public override void Exit()
        {
            _mover.CanManualMove = true;
            _mover.ResetColliderSize();
            base.Exit();
        }
    }
}