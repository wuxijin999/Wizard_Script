using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameTimer : SingletonMonobehavior<GameTimer> {

    public class Timer {
        public float interval;
        public float triggerTimer;
        public Action callBack;

        public Timer(float interval, Action callBack) {
            this.interval = interval;
            this.callBack = callBack;
            this.triggerTimer = Time.time + interval;
        }

        public void Excute() {
            triggerTimer = Time.time + interval;
            if (callBack != null) {
                callBack();
            }
        }
    }

    public class Clock {
        public DateTime triggerTimer;
        public int repeatInterval;
        public int repeatTimes;
        public Action callBack;
        public Clock(DateTime _dateTime, int _repeatInterval, int _repeatTimes, Action _callBack) {
            triggerTimer = _dateTime;
            repeatInterval = _repeatInterval;
            repeatTimes = _repeatTimes;
            callBack = _callBack;
        }

        public void Excute() {
            if (callBack != null) {
                callBack();
            }
            triggerTimer = System.DateTime.Now + new TimeSpan(TimeSpan.TicksPerSecond * repeatInterval);
            repeatTimes--;
        }
    }

    List<Timer> timerList = new List<Timer>();
    List<Clock> clockList = new List<Clock>();

    public void AddTimer(float interval, Action callBack) {//自添加时起，固定时间触发

        if (interval < 0f || callBack == null) {
            Debug.Log("Invaild timer!");
            return;
        }

        timerList.Add(new Timer(interval, callBack));
    }

    public void DelTimer(Action _callBack) {
        for (int i = 0; i < timerList.Count; i++) {
            if (timerList[i].callBack == _callBack) {
                timerList.Remove(timerList[i]);
                break;
            }
        }
    }


    public void AddClock(DateTime _dateTime, int _repeatInterval, int _repeatTimes, Action _callBack) {//固定时刻触发
        if (_dateTime < System.DateTime.Now || _repeatInterval < 1 || _repeatTimes < 1 || _callBack == null) {
            WDebug.Log("Invaild clock!");
            return;
        }
        Clock clock = new Clock(_dateTime, _repeatInterval, _repeatTimes, _callBack);
        clockList.Add(clock);
    }

    public void DelClock(Action _callBack) {
        for (int i = 0; i < clockList.Count; i++) {
            if (clockList[i].callBack == _callBack) {
                clockList.Remove(clockList[i]);
                break;
            }
        }
    }

    protected override void Awake() {
        base.Awake();
    }

    void Start() {

    }


    void Update() {
        ProcessTimer();
        ProcessClock();
    }

    void OnDestroy() {
        timerList.Clear();
    }

    private void ProcessTimer() {
        for (int i = 0; i < timerList.Count; i++) {
            if (Time.time > timerList[i].triggerTimer) {
                timerList[i].Excute();
            }
        }
    }

    private void ProcessClock() {
        for (int i = 0; i < clockList.Count; i++) {
            if (System.DateTime.Now > clockList[i].triggerTimer) {
                clockList[i].Excute();
                if (clockList[i].repeatTimes < 1) {
                    clockList.RemoveAt(i);
                }
            }
        }
    }


}
