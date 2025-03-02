using Assets.Code.Animators;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Code.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float facingRight = 1f;

        private Entity _entity;
        private Animator _animator;

        public void Initialize(Entity entity)
        { 
            _entity = entity;
            _animator = GetComponent<Animator>();      

        }

        public void SetParam(AnimParamSO param, bool value) => _animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => _animator.SetFloat(param.hashValue, value);
        public void SetParam(AnimParamSO param, int value) => _animator.SetInteger(param.hashValue, value);
        public void SetParam(AnimParamSO param) => _animator.SetTrigger(param.hashValue);

        #region Flip Controller

        public void HandleVelocityChange(Vector2 movement) => FilpController(movement.x);

        public void Filp()
        {
            facingRight *= -1f;
            _entity.transform.Rotate(0, 180f, 0);
        }

        public void FilpController(float xVelocity)
        {
            float xMove = Mathf.Approximately(xVelocity, 0f) ? 0 : Mathf.Sign(xVelocity);
            if (Mathf.Abs(xMove + facingRight) < 0.5f) //바라보는 방향과 진행방향 다르다면 플립
                Filp();
        }

        #endregion
    }
}
