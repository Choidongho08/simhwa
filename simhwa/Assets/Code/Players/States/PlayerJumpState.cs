using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Players.States;

namespace Code.Players.States
{
    public class PlayerJumpState : PlayerAirState
    {
        public PlayerJumpState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }
    }
}