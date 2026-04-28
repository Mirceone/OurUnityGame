using UnityEngine;
using UnityEngine.Events;

namespace MySoulsProject
{
    /// <summary>
    /// Put on the same GameObject as the Animator. Add Animation Events on idle clips that call
    /// <see cref="TryTriggerIdleEvent"/> (or the overloads). Each call rolls <see cref="triggerChance"/>; only then are listeners invoked.
    /// </summary>
    public class IdleRandomEventRelay : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)]
        [Tooltip("Per event call: probability that OnTriggered runs (0 = never, 1 = always).")]
        float triggerChance = 0.3f;

        [SerializeField]
        UnityEvent onTriggered;

        /// <summary>Animation event: no parameters. Uses <see cref="triggerChance"/>.</summary>
        public void TryTriggerIdleEvent()
        {
            if (Random.value > triggerChance)
                return;
            onTriggered?.Invoke();
        }

        /// <summary>Animation event: pass a Float (0–1) to override chance for this call only.</summary>
        public void TryTriggerIdleEventWithChance(float chance01)
        {
            chance01 = Mathf.Clamp01(chance01);
            if (Random.value > chance01)
                return;
            onTriggered?.Invoke();
        }

        /// <summary>Animation event: Int interpreted as percent chance (e.g. 25 = 25%).</summary>
        public void TryTriggerIdleEventPercent(int percentChance)
        {
            float chance01 = Mathf.Clamp(percentChance, 0, 100) / 100f;
            TryTriggerIdleEventWithChance(chance01);
        }
    }
}
