using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Players.States;

namespace Code.Players.States
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }
    }
}