using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucktoryGameManager : MonoBehaviour
{

    [SerializeField] private VictoryScreenUI victoryScreenUI;

    public static DucktoryGameManager Instance { get; private set; }

    public event EventHandler OnGameWon;

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;

    public event EventHandler OnGameUnpaused;

    public bool IsVictory()
    {
        return state == State.Victory;
    }


    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
        Victory 
    }


    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 120f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        ResetGameState(); 
    }


    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
       
        if (OptionsUI.Instance != null && OptionsUI.Instance.gameObject.activeSelf)
        {
            return;
        }

        TogglePauseGame();
    }


    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)

                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;


            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    gamePlayingTimer = 0f;

                    if (DeliveryManager.Instance.GetSucessfulRecipesAmount() >= DeliveryManager.Instance.GetminimumRecipesPassLevel())
                    {
                        victoryScreenUI.ShowVictoryScreen(); 
                        state = State.Victory; 
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }

                break;



            case State.GameOver:

                break;
        }

    }
    public void ResetGameState()
    {
        state = State.WaitingToStart;
        waitingToStartTimer = 1f;
        countdownToStartTimer = 3f;
        gamePlayingTimer = gamePlayingTimerMax;
        isGamePaused = false;

        Time.timeScale = 1f; 

        OnStateChanged?.Invoke(this, EventArgs.Empty);

        Debug.Log("ResetGameState() called");
    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
        {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
        }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

}
