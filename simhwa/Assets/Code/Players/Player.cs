
using Assets.Code.Entities.FSM;
using Assets.Code.Players;
using System;
using Assets.Code.Animators;
using Assets.Code.Core.StatSystem;
using Assets.Code.Entities;
using Code.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using StateMachine = Assets.Code.Entities.FSM.StateMachine;

namespace Code.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [SerializeField] private StateListSO playerFSM;

        private StateMachine _stateMachine;

        [SerializeField] private StatSO jumpCountStat, attackSpeedStat;
        [field:SerializeField] public AnimParamSO ComboCounterParam {get; private set;}
        
        private int _maxJumpCount;
        private int _currentJumpCount;
        public bool CanJump => _currentJumpCount > 0;

        [Header("Temp settings")]
        public Vector2[] atkMovement;
        public Vector2 dashAttackMovement;

        protected override void Awake()
        {
            base.Awake();

            _stateMachine = new StateMachine(this, playerFSM);
        }

        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            EntityStat statCompo = GetCompo<EntityStat>();
            statCompo.GetStat(jumpCountStat).OnValueChange += HandleJumpCountChange;
            
            _maxJumpCount = Mathf.RoundToInt(statCompo.GetStat(jumpCountStat).Value);
            _currentJumpCount = _maxJumpCount;
            
            PlayerInput.OnDashKeyPressed += HandleDashKeyPressed;

            GetCompo<EntityAnimationTrigger>().OnAnimationEnd += HandleAnimationEnd;
        }
        
        private void OnDestroy()
        {
            GetCompo<EntityStat>().GetStat(jumpCountStat).OnValueChange -= HandleJumpCountChange;
            PlayerInput.OnDashKeyPressed -= HandleDashKeyPressed;
        }

        private void HandleAnimationEnd() => _stateMachine.CurrentState.AnimationEndTrigger();
        

        private void HandleDashKeyPressed()
        {
            ChangeState("DASH");
        }

        private void HandleJumpCountChange(StatSO stat, float current, float previous)
            => _maxJumpCount = Mathf.RoundToInt(current);
        
        public void DecreaseJumpCount() => _currentJumpCount--;
        public void ResetJumpCount() => _currentJumpCount = _maxJumpCount;

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

