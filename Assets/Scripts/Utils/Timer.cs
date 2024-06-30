#nullable enable
using System;
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        private class TimerLogic
        {
            private float _timeRemaining;
            private readonly Action _onCompleteTimer;

            public TimerLogic(float timeRemaining, Action onCompleteTimer)
            {
                _timeRemaining = timeRemaining;
                _onCompleteTimer = onCompleteTimer;
            }

            public bool TickAndTryComplete()
            {
                _timeRemaining -= Time.deltaTime;
                if (_timeRemaining > 0)
                {
                    return false;
                }
                
                _onCompleteTimer.Invoke();
                return true;
            }
        }

        private TimerLogic? _runningTimer;
        
        public void Start(float delay, Action onComplete)
        {
            if (_runningTimer != null)
            {
                Debug.LogError("Timer.Start: the timer is not finished yet");
                return;
            }

            _runningTimer = new TimerLogic(delay, onComplete);
        }

        public void TryTick()
        {
            if (_runningTimer == null)
            {
                return;
            }

            if (_runningTimer.TickAndTryComplete())
            {
                _runningTimer = null;
            }
        }
    }
}