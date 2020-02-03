using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3 pointA, pointB; //set in inspector
    private Vector3 _targetPos;

    [SerializeField]
    private float _speed = 1.0f;

    private GameObject _player;


    private void Start()
    {
        _targetPos = pointA; //start at point A
        _player = GameObject.Find("Player");
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);

        if (transform.position == pointA)
        {
            _targetPos = pointB;
        }
        if (transform.position == pointB)
        {
            _targetPos = pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.SetParent(this.gameObject.transform);
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
