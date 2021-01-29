using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aref.Com.GameJam
{
    public class PlayerFlashLIght : MonoBehaviour
    {
        [SerializeField] private KeyCode _flashLightInputKey;

        [SerializeField] private GameObject _flashLight;
        [SerializeField] private bool _flashLightIsActive;

        private void Start()
        {
            _flashLight.SetActive(false);
        }

        private void Update()
        {
            FlashLightActivition();
        }

        private void FlashLightActivition()
        {
            if (Input.GetKeyDown(_flashLightInputKey))
            {
                _flashLightIsActive = !_flashLightIsActive;
                FlashLightActiveState(_flashLightIsActive);
            }
        }

        private void FlashLightActiveState(bool status)
        {
            if (status) _flashLight.SetActive(true);
            else _flashLight.SetActive(false);
        }
    }
}