using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{

    #region Varibales

    public Text timerText;

    private float _timer = 0;

    #endregion

    #region Inbuilt Functions

    private void Start()
    {
        LoseCondition.OnTimerUpdate += TimerCatcher;
    }

    void Update()
    {
        int sec = Mathf.FloorToInt(_timer % 60);
        int min = Mathf.FloorToInt(_timer / 60);

        timerText.text = min.ToString() + ":" + sec.ToString(); 
    }

    #endregion

    #region Functions 

    private void TimerCatcher(float gameTime)
    {
        _timer = gameTime;
    } 

    #endregion

}
