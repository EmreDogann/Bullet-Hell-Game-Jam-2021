using UnityEngine;

namespace AI.MachineGunner {
    public class AIMachineGunnerAttack : StateMachineBehaviour {
        private static readonly int Attack = Animator.StringToHash("attack");
        private EnemyMeleeAttack _enemyMeleeAttack;
        private bool _isMeleeAttackNull = true;
        private bool _hasAttacked;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (_isMeleeAttackNull) {
                _enemyMeleeAttack = animator.GetComponent<EnemyMeleeAttack>();
                if (_enemyMeleeAttack != null)
                    _isMeleeAttackNull = false;
            }

            _hasAttacked = false;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!_isMeleeAttackNull) {
                if (!_hasAttacked) {
                    _enemyMeleeAttack.Attack();
                    _hasAttacked = true;
                }
            }

            animator.SetBool(Attack, false);
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    }
}