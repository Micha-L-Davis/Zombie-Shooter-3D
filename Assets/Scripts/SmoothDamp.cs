using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homesource
{
    public class SmoothDamp : MonoBehaviour
    {
        [SerializeField]
        Transform _target;
        [SerializeField]
        float _speed = 10f;
        void LateUpdate()
        {
            Vector3 targetPosition = _target.transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(_target.transform.rotation.eulerAngles);
        }
    }
}

