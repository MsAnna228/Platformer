using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _callButton;

    [SerializeField]
    private int _requiredCoins = 8;

    [SerializeField]
    private Elevator _elevator;

    private bool _elevatorCalled = false;


    private void Start()
    {
        _elevator = _elevator.GetComponent<Elevator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && other.GetComponent<Player>()._playerCoins >= _requiredCoins)
            {
                if (_elevatorCalled)
                {
                    _callButton.material.color = Color.red;
                    _elevatorCalled = false;
                }
                else if (_elevatorCalled == false)
                {
                    _callButton.material.color = Color.blue;
                    _elevatorCalled = true;
                }
                _elevator.CallElevator();
            }
        }
    }
}