using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aref.Com.GameJam
{
    public class PortalTextureSetup : MonoBehaviour
    {
        [SerializeField] private Camera _camera2CBlue;
        [SerializeField] private Material _cameraBlueMaterial;

        private void Start()
        {
            if (_camera2CBlue.targetTexture != null)
            {
                _camera2CBlue.targetTexture.Release();
            }

            _camera2CBlue.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            _cameraBlueMaterial.mainTexture = _camera2CBlue.targetTexture;
        }
    }
}