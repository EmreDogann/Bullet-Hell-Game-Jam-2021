using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIProwlerIdle : StateMachineBehaviour {
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Teleport = Animator.StringToHash("teleport");

    public float teleportIntervals;
    public float teleportIntervalsDeviation = 1f;
    private float teleportTimer = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        teleportTimer = teleportIntervals + Random.Range(-teleportIntervalsDeviation, teleportIntervalsDeviation + 1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // animator.SetBool(Idle, false);
        if (teleportTimer > 0) {
            teleportTimer -= Time.deltaTime;
        }
        else {
            animator.SetBool(Teleport, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    }
}