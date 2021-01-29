using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aref.Com.GameJam {
    public class VictoryCheckPoints : MonoBehaviour
    {
        [SerializeField] private GameObject _objectFound;
        [SerializeField] private string _lostObjectName;

        private GameManager _gameManagerScript;

        private void Start()
        {
            _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.name == _lostObjectName)
            {
                Debug.Log("Hemlet Found");
                Instantiate(other.gameObject, _objectFound.transform.position, _objectFound.transform.rotation, _objectFound.transform);
                _gameManagerScript.ObjectSDone += 1;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}