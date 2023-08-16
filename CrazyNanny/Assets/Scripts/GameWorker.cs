using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameWorker : MonoBehaviour
{
    public static GameWorker Instance { get; private set;}

    public event EventHandler StateChange;

    private enum State {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private TimerScript timeInstance;


    private void StartTimer() {
        timeInstance = FindObjectOfType<TimerScript>();
        if (timeInstance != null) {
            timeInstance.TimerOn = true;
        }
    }

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
            waitingToStartTimer -= Time.deltaTime;
            if (waitingToStartTimer < 0f) {
                state=State.CountDownToStart;
                StateChange?.Invoke(this, EventArgs.Empty);
            }
            break;

            case State.CountDownToStart:
            countdownToStartTimer -= Time.deltaTime;
            if (countdownToStartTimer < 0f) {
                state=State.GamePlaying;
                StateChange?.Invoke(this, EventArgs.Empty);
                StartTimer(); //start the timer after 3seconds
            }
            break;

            case State.GamePlaying:
            timeInstance = FindObjectOfType<TimerScript>();
            float timeRemain = timeInstance.TimeLeft;
            if (timeRemain < 0f) {
                state=State.GameOver;
                StateChange?.Invoke(this, EventArgs.Empty);
            }
            break;

            case State.GameOver:
            break;

        }
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool CountdownActive() {
        return state==State.CountDownToStart;
    }

    public float GetCountdownTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return state==State.GameOver;
    }

}
