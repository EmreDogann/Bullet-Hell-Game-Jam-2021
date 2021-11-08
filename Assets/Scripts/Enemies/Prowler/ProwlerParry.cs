using UnityEngine;

namespace DefaultNamespace {
    public class ProwlerParry : Parryable {
        protected override void OnParry() {
            // get destroyed maybe
            GetComponent<HealthStat>().SetHealth(0);
            GetComponentInChildren<Animator>().Play("Death");
        }
    }
}