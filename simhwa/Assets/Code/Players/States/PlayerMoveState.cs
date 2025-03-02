using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Players.States;
using Code.Entities;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }
        public override void Update()
        {
            base.Enter();
            float xInput = _player.PlayerInput.InputDirection.x;

            _mover.SetMovementX(xInput);

           if(Mathf.Approximately(xInput, 0)) // 유사 근접 체크, 
            {
                _player.ChangeState("IDLE");
            }
        }
    }
}
