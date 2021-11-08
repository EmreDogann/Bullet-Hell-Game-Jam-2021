# AI Instructions
> Aum Patel

Basic Tutorial on using Animator as FSM for AI: https://youtu.be/dYi-i83sq5g

For an example look at Prowler's `Animator Controller` and the `states` in it along with the `behaviours` attached to them.

For Timers:
- either use a `float timer` and count down using delta time
  - (example in `AIProwlerIdle.cs`).
- or you can use the transition time in `animationTransitionArrow-> Settings>fixedDuration and Transition duration in seconds`
    - example in the transition between open and idle
    - when using this, the `OnStateExit()` method inside a behaviour gets executed at the end of the transition
      - so in the example you can see that `isParryable` gets set to false after that state is over.
    - `has exit time` determines how long the `OnStateUpdate()` runs for. I have turned it off for the prowler, but can be useful for other use cases.