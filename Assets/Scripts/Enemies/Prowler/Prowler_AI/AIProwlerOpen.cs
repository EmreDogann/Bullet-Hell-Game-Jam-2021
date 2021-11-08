using UnityEngine;

namespace AI.Prowler {
    public class AIProwlerOpen : StateMachineBehaviour {
        private Parryable _parryable;
        private bool _isParryableNull = true;
        private static readonly int Open = Animator.StringToHash("open");
        private static readonly int Attack = Animator.StringToHash("attack");

        public float parryInterval;
        private float _timer = 0;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _timer = parryInterval;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _parryable.SetIsParryable(false);
            // Debug.Log(_parryable.IsParryable());
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (_isParryableNull) {
                _parryable = animator.GetComponent<Parryable>();
                _isParryableNull = _parryable;
            }

            if (_timer > 0) {
                _timer -= Time.deltaTime;
                _parryable.SetIsParryable(true);
            }
            else {
                animator.SetBool(Open, false);
                // attack next
                animator.SetBool(Attack, true);
            }
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        }
    }
}