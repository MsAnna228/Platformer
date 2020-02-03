using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _pointA, _pointB;
    [SerializeField]
    private float _speed = 2.0f;


    private Vector3 _targetPos;



    private void Start()
    {
        _targetPos = _pointA; 
    }
    public void CallElevator()
    {
        //know the current position of the elevator && set new target accordingly
        if (transform.position == _pointA)
        {
            _targetPos = _pointB;
            //go to point b
        }
        if (transform.position == _pointB)
        {
            _targetPos = _pointA;
            //go to point a
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.SetParent(this.gameObject.transform);
                CallElevator();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
