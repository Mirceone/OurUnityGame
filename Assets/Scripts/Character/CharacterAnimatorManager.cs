using MySoulsProject;
using UnityEngine;

namespace MyNamespace
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager characterManager;

        float vertical;
        float horizontal;
        
        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }
        
        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            characterManager.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
            characterManager.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction,
            bool applyRootMotion = true)
        {
            characterManager.animator.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(targetAnimation, 0.2f);
            characterManager.isPerformingAction = isPerformingAction;
        }
    }
}

