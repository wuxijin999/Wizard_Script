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
        public Clock() {

        }

        public void Excute() {

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


    public void AddClock() {//固定时刻触发

    }

    public void DelClock() {

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

    }

}
