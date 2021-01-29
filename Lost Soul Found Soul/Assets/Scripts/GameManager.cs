using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aref.Com.GameJam
{
    public class GameManager : MonoBehaviour
    {
        //--------------------------------variables & references-----------------------------------------
        #region variables and references:

        [Header("VARIABLES AND REFERENCES")]

        [SerializeField] private int _objectsDone = 0;

        #endregion
        //--------------------------------getter setter-------------------------------------------------
        #region getter and setters:

        public int ObjectSDone
        {
            get => _objectsDone;
            set => _objectsDone = value;
        }

        #endregion
        //--------------------------------main functions-------------------------------------------------
        #region main functions:

        private void Start()
        {
            _objectsDone = 0;
        }

        private void Update()
        {
            CheckObjectsDone();
        }

        #endregion
        //--------------------------------private functions-------------------------------------------------
        #region private functions:

        private void CheckObjectsDone()
        {
            if(_objectsDone == 7)
            {
                Debug.Log("victorious");
            }
        }

        public void OnRestartButtonClick()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Museum");
        }

        #endregion
    }
}