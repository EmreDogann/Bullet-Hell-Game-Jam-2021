using UnityEngine;

namespace AI.Prowler {
    public class AIProwlerTeleport : StateMachineBehaviour {
        private static readonly int Teleport = Animator.StringToHash("teleport");
        private static readonly int Open = Animator.StringToHash("open");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

            // if (Input.GetKeyDown(KeyCode.Space))
            //     animator.SetBool(Teleport, false);
            var _prowlerController = animator.GetComponent<ProwlerController>();
            _prowlerController.Teleport();
            animator.SetBool(Open, true);
            animator.SetBool(Teleport, false);
        }

    }
}