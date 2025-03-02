using System;
using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Players.States;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Update()
        {
            base.Update();
            if (_mover.IsGroundDetected())
            {
                _player.ChangeState("IDLE");
            }
        }
        
    }
}