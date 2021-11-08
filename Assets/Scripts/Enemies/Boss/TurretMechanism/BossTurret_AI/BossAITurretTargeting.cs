using Timer;
using UnityEngine;

namespace DefaultNamespace.AI.Turret
{
    public class BossAITurretTargeting : StateMachineBehaviour
    {
        private global::BossTurret _turret;
        private Timers _timers;
        private static readonly int IsTargeting = Animator.StringToHash("isTargeting");
        private static readonly int IsFiring = Animator.StringToHash("isFiring");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            if (_timers == null)
            {
                _timers = animator.gameObject.GetComponent<Timers>();
            }
            else
            {
                _timers.ResetTimer(global::BossTurret.TargetingTimerName);

            }
            if (_turret == null)
            {
                _turret = animator.gameObject.GetComponent<global::BossTurret>();
            }

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_timers.UpdateTimer(global::BossTurret.TargetingTimerName))
            {
                _turret.lineRenderer.enabled = false;
                _turret.endOfRay.SetActive(false);
            }
            else
            {
                animator.SetBool(IsTargeting, false);
                animator.SetBool(IsFiring, true);
            }
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}