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
            _gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.name == _lostObjectName)
            {
                Debug.Log("Hemlet Found");

                GameObject t_hamlet = Instantiate(other.gameObject, _objectFound.transform.position, _objectFound.transform.rotation * Quaternion.Euler(-90.0f,0.0f,180.0f) ) as GameObject;
                t_hamlet.GetComponent<Rigidbody>().isKinematic = true;
                t_hamlet.gameObject.tag = "Untouchable";

                _gameManagerScript.ObjectSDone += 1;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}