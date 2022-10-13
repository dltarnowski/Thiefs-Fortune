using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeutronCat.LowpolyParticle
{
    public class LowpolyParticleSimpleMovement : MonoBehaviour
    {
        public Vector3 rotationVelocity;
        public Vector3 swingVector;
        public float swingFrequency;

        Transform _transform;
        Vector3 _startPosition;

        void Start()
        {
            _transform = transform;
            _startPosition = _transform.position;
        }

        void Update()
        {
            _transform.Rotate(rotationVelocity * Time.deltaTime);
            _transform.position = _startPosition + swingVector * Mathf.Sin(Time.time * swingFrequency);
        }
    }
}