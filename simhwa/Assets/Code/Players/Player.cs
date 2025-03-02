using Assets.Code.Entities;
using Assets.Code.Entities.FSM;
using Assets.Code.Players;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [SerializeField] private StateListSO playerFSM;

        private StateMachine _stateMachine;

        protected override void Awake()
        {
            base.Awake();

            _stateMachine = new StateMachine(this, playerFSM);
        }
        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }
        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStateName)
        {
            _stateMachine.ChangeState(newStateName);
        }
    }
}

