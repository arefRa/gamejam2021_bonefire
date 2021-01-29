using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aref.Com.GameJam
{
    public class PortalTeleporterScript : MonoBehaviour
    {
        [SerializeField] private Transform _teleportable;                 //subject of teleportation
        [SerializeField] private Transform _receiverPortal;               //teleportation destination

        private bool _playerIsOverlappingWithPortal = false;

        private void Update()
        {
            TeleportThatBitch();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") _playerIsOverlappingWithPortal = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player") _playerIsOverlappingWithPortal = false;
        }

        private void TeleportThatBitch()
        {
            if (_playerIsOverlappingWithPortal)
            {
                Vector3 t_portal2Player = _teleportable.position - transform.position;
                float t_dotProduct = Vector3.Dot(transform.up, t_portal2Player);

                //if this is true so the player has moved over the portal
                if(t_dotProduct < 0f)
                {
                    //teleport that bastard!
                    float t_rotationDifference = -Quaternion.Angle(transform.rotation, _receiverPortal.rotation);
                    t_rotationDifference += 180;
                    _teleportable.Rotate(Vector3.up, t_rotationDifference);

                    Vector3 t_positionOffset = Quaternion.Euler(0.0f, t_rotationDifference, 0.0f) * t_portal2Player;
                    _teleportable.position = _receiverPortal.position + t_positionOffset;

                    _playerIsOverlappingWithPortal = false;
                }
            }
        }
    }
}