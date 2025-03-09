using System;
using Assets.Code.Animators;
using Assets.Code.Core.StatSystem;
using Assets.Code.Entities;
using Code.Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Players
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private StatSO attackSpeedStat;
        [SerializeField] private AnimParamSO attackSpeedParam;
        
        private Player _player;
        private EntityStat _statCompo;
        private EntityRenderer _renderer;
        private EntityMover _mover;

        private bool _canJumpAttack;

        #region Init Section

        public void Initialize(Entity entity)   
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
            _statCompo = entity.GetCompo<EntityStat>();
            _renderer = entity.GetCompo<EntityRenderer>();
        }

        public void AfterInit()
        {
            _statCompo.GetStat(attackSpeedStat).OnValueChange += HandleAttackSpeedChange;
            _renderer.SetParam(attackSpeedParam, _statCompo.GetStat(attackSpeedStat).Value);
        }

        private void OnDestroy()
        {
            _statCompo.GetStat(attackSpeedStat).OnValueChange -= HandleAttackSpeedChange;
        }

        #endregion
       
        private void HandleAttackSpeedChange(StatSO stat, float current, float previous)
        {
            _renderer.SetParam(attackSpeedParam, current);
        }

        public bool CanJumpAttack()
        {
            bool returnValue = _canJumpAttack;
            if (_canJumpAttack)
                _canJumpAttack = false;
            return returnValue;
        }

        private void FixedUpdate()
        {
            if (_canJumpAttack == false && _mover.IsGroundDetected())
                _canJumpAttack = true;
        }
    }
}