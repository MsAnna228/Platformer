using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _pressurePad;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MovingBox")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance < 0.2)
            {
                Rigidbody _rigidbody = other.gameObject.GetComponent<Rigidbody>();
                if (_rigidbody != null)
                {
                    _rigidbody.isKinematic = true;
                }
                if (_pressurePad != null)
                {
                    _pressurePad.material.color = Color.blue;
                }
                Destroy(this);
            }
        }
    }
}
