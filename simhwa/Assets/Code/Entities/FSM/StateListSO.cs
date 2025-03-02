using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Entities.FSM
{
    [CreateAssetMenu(fileName = "StateList", menuName = "SO/FSM/StateList")]
    public class StateListSO : ScriptableObject
    {
        public List<StateSO> states;
    }
}
