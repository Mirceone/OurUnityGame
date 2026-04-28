using UnityEngine;

namespace MySoulsProject
{
    /// <summary>
    /// Add to the Locomotion state (or any state using a nested idle 1D blend tree).
    /// Sets <see cref="idleVariationParameter"/> so one of several idle clips is chosen at random.
    /// For Animation Events on those idles (sounds, FX) with a probability each time, use <see cref="IdleRandomEventRelay"/> on this Animator's GameObject.
    /// </summary>
    public class RandomIdleVarietyBehaviour : StateMachineBehaviour
    {
        [Tooltip("Float parameter on the Animator; should be the Parameter on a 1D idle blend tree.")]
        public string idleVariationParameter = "IdleVariation";

        [Tooltip("How many idle motions are in that 1D tree (thresholds 0, 1, 2, ...).")]
        public int idleClipCount = 3;

        [Tooltip("While standing still, pick a new random idle every N seconds. 0 = only when entering this state.")]
        [Min(0f)]
        public float reshuffleEverySeconds = 12f;

        static readonly int HorizontalHash = Animator.StringToHash("Horizontal");
        static readonly int VerticalHash = Animator.StringToHash("Vertical");

        int _paramHash;
        float _timer;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _paramHash = Animator.StringToHash(idleVariationParameter);
            _timer = 0f;
            PickRandomIdle(animator);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (reshuffleEverySeconds <= 0f || idleClipCount < 2)
                return;

            if (Mathf.Abs(animator.GetFloat(HorizontalHash)) > 0.01f ||
                Mathf.Abs(animator.GetFloat(VerticalHash)) > 0.01f)
            {
                _timer = 0f;
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= reshuffleEverySeconds)
            {
                _timer = 0f;
                PickRandomIdle(animator);
            }
        }

        void PickRandomIdle(Animator animator)
        {
            if (idleClipCount < 1)
                return;
            float index = Random.Range(0, idleClipCount);
            animator.SetFloat(_paramHash, index);
        }
    }
}
