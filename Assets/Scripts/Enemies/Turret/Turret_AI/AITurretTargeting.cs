using Timer;
using UnityEngine;

namespace DefaultNamespace.AI.Turret
{
    public class AITurretTargeting : StateMachineBehaviour
    {
        private global::Turret _turret;
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
                _timers.ResetTimer(global::Turret.TargetingTimerName);

            }
            if (_turret == null)
            {
                _turret = animator.gameObject.GetComponent<global::Turret>();
            }

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
            // if (_turret == null) {
            //     _turret = animator.gameObject.GetComponent<global::Turret>();
            // }


            if (!_timers.UpdateTimer(global::Turret.TargetingTimerName))
            {
                Vector2 direction = _turret.target.position - _turret.transform.position;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Rotate Turret in the correct angle
                angle -= 90;

                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                _turret.transform.rotation = Quaternion.Slerp(_turret.transform.rotation, rotation, _turret.speed * Time.deltaTime);

                _turret.speed -= (_turret.speedReduction * Time.deltaTime);

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