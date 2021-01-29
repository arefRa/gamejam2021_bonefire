using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aref.Com.GameJam
{
    public class PortalCamera : MonoBehaviour
    {
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private Transform _portal;             //the portal the camera is installed on
        [SerializeField] private Transform _targetPortal;       //the portal that we will see through

        private void Update()
        {
            PortalCameraMotion();
        }

        private void PortalCameraMotion()
        {
            Vector3 t_playerOffsetFromPortal = _playerCamera.position - _targetPortal.position;
            transform.position = _portal.position + t_playerOffsetFromPortal;

            float t_angularDifferenceBetweenPortalRotations = Quaternion.Angle(_portal.rotation, _targetPortal.rotation);

            Quaternion t_portalRotationDifference = Quaternion.AngleAxis(t_angularDifferenceBetweenPortalRotations, Vector3.up);
            Vector3 t_newCameraDirection = t_portalRotationDifference * _playerCamera.forward;
            transform.rotation = Quaternion.LookRotation(t_newCameraDirection, Vector3.up);
        }
    }
}