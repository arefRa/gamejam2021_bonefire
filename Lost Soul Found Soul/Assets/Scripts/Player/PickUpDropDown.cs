using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aref.Com.GameJam
{
    public class PickUpDropDown : MonoBehaviour
    {
        //--------------------------------variables & references-----------------------------------------
        #region variables and references:

        [Header("VARIABLES AND REFERENCES")]

        [SerializeField] private KeyCode _pickUpKey;
        [SerializeField] private KeyCode _dropDownKey;

        [SerializeField] private Transform _lightEquiptSlot, _heavyEquiptSlot;
        [SerializeField] private float _pickUpDistance;
        [SerializeField] private float _slowMoveSpeed;

        private float _normalMoveSpeed;

        private PlayerMovingAround _moveScript;

        private GameObject _currentEquiptable;
        private GameObject _wp;

        [Header("BOOLS")]

        [SerializeField] private bool _canGrab;

        #endregion
        //--------------------------------main functions-------------------------------------------------
        #region main functions:

        private void Start()
        {
            _moveScript = transform.GetComponent<PlayerMovingAround>();
            _normalMoveSpeed = _moveScript.MovementSpeed;
        }

        private void Update()
        {
            CheckEquiptable();

            if (_canGrab)
            {
                if (Input.GetKeyDown(_pickUpKey))
                {
                    if (_currentEquiptable != null) DropDown();
                    PickUp();
                }
            }

            if(_currentEquiptable != null)
            {
                if (Input.GetKeyDown(_dropDownKey))
                    DropDown();
            }
        }

        #endregion
        //--------------------------------private functions-------------------------------------------------
        #region private functions:

        private void CheckEquiptable()
        {
            RaycastHit t_hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out t_hit, _pickUpDistance))
            {
                if (t_hit.transform.tag == "Grabable_H" || t_hit.transform.tag == "Grabable_L")
                {
                    _canGrab = true;
                    _wp = t_hit.transform.gameObject;
                }
            }
            else _canGrab = false;
        }

        private void PickUp()
        {
            _currentEquiptable = _wp;

            if(_currentEquiptable.tag == "Grabable_H")
            {
                _currentEquiptable.transform.position = _heavyEquiptSlot.position;
                _currentEquiptable.transform.parent = _heavyEquiptSlot;

                _moveScript.CarryingHeavy = true;
                _moveScript.MovementSpeed = _slowMoveSpeed;
            }
            else if(_currentEquiptable.tag == "Grabable_L")
            {
                _currentEquiptable.transform.position = _lightEquiptSlot.position;
                _currentEquiptable.transform.parent = _lightEquiptSlot;
            }

            _currentEquiptable.transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            _currentEquiptable.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void DropDown()
        {
            if (_currentEquiptable.tag == "Grabable_H")
            {
                _moveScript.CarryingHeavy = false;
                _moveScript.MovementSpeed = _normalMoveSpeed;
            }

            _currentEquiptable.transform.parent = null;
            _currentEquiptable.GetComponent<Rigidbody>().isKinematic = false;
            _currentEquiptable = null;
        }

        #endregion
    }
}