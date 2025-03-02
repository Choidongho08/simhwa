using UnityEngine;

namespace Assets.Code.Animators
{
    [CreateAssetMenu(fileName = "ParamSO", menuName = "SO/Animator/Param")]
    public class AnimParamSO : ScriptableObject
    {
        public string parameterName;
        public int hashValue;
        [TextArea]
        public string description;

        private void OnValidate()
        {
            hashValue = Animator.StringToHash(parameterName);
        }
    }
}
