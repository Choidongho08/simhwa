using Assets.Code.Core.StatSystem;
using Assets.Code.Entities;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInit
    {
        public UnityEvent<Vector2> OnMove;

        [SerializeField] private StatSO moveSpeedStat;

        private Rigidbody2D _rbCompo;

        private float _movementX;
        private float _moveSpeed = 6f;
        private float _moveSpeedMultiplier;

        private EntityStat _statCompo;
        
        public bool CanManualMove { get; set; } = true; // 넉백, 기절 시 이동 불가

        public void Initialize(Entity entity)
        {
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _statCompo = entity.GetComponentInChildren<EntityStat>();
            _moveSpeedMultiplier = 1f;
        }

        public void AfterInit()
        {
            _statCompo.GetStat(moveSpeedStat).OnValueChange += HandleMoveSpeedChange;
        }

        private void OnDestroy()
        {
            _statCompo.GetStat(moveSpeedStat).OnValueChange -= HandleMoveSpeedChange;
        }

        public void AddForceToEntity(Vector2 force) => _rbCompo.AddForce(force, ForceMode2D.Impulse);
        
        public void SetMoveSpeedMultiplier(float value) => _moveSpeedMultiplier = value;
        
        private void HandleMoveSpeedChange(StatSO stat, float current, float previous)
            => _moveSpeed = current;

        private void FixedUpdate()
        {
            if (CanManualMove)
                _rbCompo.linearVelocityX = _movementX * _moveSpeed * _moveSpeedMultiplier;
            
            _rbCompo.linearVelocityX = _movementX * _moveSpeed;
            OnMove?.Invoke(_rbCompo.linearVelocity);

        }

        public void SetMovementX(float xMovemenet)
        {
            _movementX = xMovemenet;
        }

        public void StopImmediately(bool isYAxisToo)
        {
            if (isYAxisToo)
                _rbCompo.linearVelocity = Vector2.zero;
            else
                _rbCompo.linearVelocityX = 0;

            _movementX = 0;
        }

       
    }
}

