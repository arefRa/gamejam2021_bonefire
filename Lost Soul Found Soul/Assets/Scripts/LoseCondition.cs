using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoseCondition : MonoBehaviour
{

    #region Veriables 

    private static LoseCondition _instance;

    public static event Action<float> OnTimerUpdate;

    //game time in seconds.
    [SerializeField] private float gameTime = 600;

    [SerializeField] private List<GameObject> canvasesToDeactivate;

    #endregion

    #region Getter/Setter 

    public float GameTime { get => gameTime; set => gameTime = value; }

    #endregion

    #region Inbuilt Functions 

    private void Awake()
    {
        Time.timeScale = 1;

        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start() => Debug.Log("Game Timer Started");

    void Update() => UpdateGameTime();

    #endregion

    #region Functions 

    public static LoseCondition Instance { get { return _instance; } }

    private void UpdateGameTime()
    {
        GameTime = GameTime - Time.deltaTime;

        if (OnTimerUpdate != null)
            OnTimerUpdate(GameTime);

        if (GameTime <= 0)
            Lose();         
    }

    private void Lose()
    {
        foreach (var obj in canvasesToDeactivate)
            obj.SetActive(false);


        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;

        GameObject loseUI = GameObject.Find("LoseUI");

        loseUI.transform.GetChild(0).gameObject.SetActive(true);
    }

    #endregion

}
