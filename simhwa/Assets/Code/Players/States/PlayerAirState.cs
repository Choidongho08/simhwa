using Assets.Code.Animators;
using Assets.Code.Entities;
using Assets.Code.Entities.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Players.States
{
    public abstract class PlayerAirState : EntityState
    {
        public PlayerAirState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }
    }
}
