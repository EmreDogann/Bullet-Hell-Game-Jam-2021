using Timer;
using UnityEngine;

namespace DefaultNamespace.AI.Turret
{
    public class BossAITurretFire : StateMachineBehaviour
    {
        private global::BossTurret _turret;
        private Timers _timers;
        private static readonly int IsTargeting = Animator.StringToHash("isTargeting");
        private static readonly int IsFiring = Animator.StringToHash("isFiring");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _timers.ResetTimer(global::BossTurret.FireTimerName);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_timers == null)
            {
                _timers = animator.gameObject.GetComponent<Timers>();
            }

            if (_turret == null)
            {
                _turret = animator.gameObject.GetComponent<global::BossTurret>();
            }


            // laserFiringTime += (1f * Time.deltaTime);
            if (!_timers.UpdateTimer(global::BossTurret.FireTimerName))
            {
                _turret.Shoot(true);
            }
            else
            {
                // _turret.isShooting = false;
                animator.SetBool(IsTargeting, true);
                animator.SetBool(IsFiring, false);
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