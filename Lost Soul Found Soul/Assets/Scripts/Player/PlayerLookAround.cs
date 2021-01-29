using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aref.Com.GameJam
{
    public class PlayerLookAround : MonoBehaviour
    {
        //--------------------------------variables & references-----------------------------------------
        #region variables and references:

        [Header("VARIABLES AND REFERENCES")]

        [SerializeField] private string _mouseXInputName, _mouseYInputName;
        [SerializeField] private float _mouseSensitivity;
        [SerializeField] private Transform _playerBody;

        private float _xAxisClamp;
        private float _xAxisClampLimitaion;

        #endregion
        //--------------------------------main functions-------------------------------------------------
        #region main functions:

        private void Awake()
        {
            LockCursor();
            _xAxisClamp = 0.0f;
            _xAxisClampLimitaion = 90.0f;
        }

        private void Update()
        {
            CameraRotation();
        }

        #endregion
        //--------------------------------private functions-------------------------------------------------
        #region private functions:

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void CameraRotation()
        {
            float t_mouseX = Input.GetAxis(_mouseXInputName) * _mouseSensitivity * Time.deltaTime;
            float t_mouseY = Input.GetAxis(_mouseYInputName) * _mouseSensitivity * Time.deltaTime;

            _xAxisClamp += t_mouseY;

            if (_xAxisClamp > _xAxisClampLimitaion)
            {
                _xAxisClamp = 90.0f;
                t_mouseY = 0.0f;
                ClampXAxisRotation2Value(270.0f);
            }
            else if (_xAxisClamp < -_xAxisClampLimitaion)
            {
                _xAxisClamp = -90.0f;
                t_mouseY = 0.0f;
                ClampXAxisRotation2Value(90.0f);
            }

            transform.Rotate(Vector3.left * t_mouseY);
            _playerBody.Rotate(Vector3.up * t_mouseX);
        }

        private void ClampXAxisRotation2Value(float value)
        {
            Vector3 eularRotation = transform.eulerAngles;
            eularRotation.x = value;
            transform.eulerAngles = eularRotation;
        }
        #endregion
    }
    // line fill
    // line fill
}