using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timer {

    // 
    internal class SingleTimer {
        public float currentValue;
        public float resetValue;

        public SingleTimer(float val,float resetVal) {
            currentValue = val;
            resetValue = resetVal;
        }

        public void Reset() {
            currentValue = resetValue;
        }
        
        public void DecrementCurrentValue(float decrement) {
            currentValue -= decrement;
        }
    }
    public class Timers : MonoBehaviour {

        private Dictionary<string, SingleTimer> _timers;

        // initialise an empty dictionary
        private void Awake() {
            _timers = new Dictionary<string, SingleTimer>();
        }

        // will take away scale * deltaTime from the timer and return true if finished
        public bool UpdateTimer(string timerName, float scale = 1) {
            if (_timers[timerName].currentValue > 0) {
                _timers[timerName].DecrementCurrentValue(scale *Time.deltaTime);
                return false;
            }

            return true;
        }

        // reset the timer to the reset value
        public void ResetTimer(string timerName) {
            _timers[timerName].Reset();
        }
        
        // add a timer in with a custom value and reset value
        public void AddTimer(string timerName, float value, float resetValue) {
            _timers.Add(timerName, new SingleTimer(value, resetValue));
        }
         
        // add a timer in with the same value and reset value
        public void AddTimer(string timerName, float value) {
            _timers.Add(timerName, new SingleTimer(value, value));
        }
        
        // get timer with given key
        public float GetTimerValue(string timerName) {
            return _timers[timerName].currentValue;
        }

        public void SetTimersFromSave(Timers timers) {
            _timers = timers._timers;
        }
        

    }
}