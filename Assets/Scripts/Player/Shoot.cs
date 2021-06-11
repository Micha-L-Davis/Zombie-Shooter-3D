using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    float _fireCooldown;
    float _canfire = -1;
    [SerializeField]
    GameObject _bloodSplatFX;
    Transform _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.transform;
        if (_camera == null)
            throw new UnityException("Main Camera is NULL");   
    }

    public void Fire()
    {
        RaycastHit hit;
        if (Time.time > _canfire && Physics.Raycast(_camera.position, _camera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            _canfire = Time.time + _fireCooldown;
            IDamageable target = hit.transform.GetComponent<IDamageable>();
            if (target != null)
            {

                Instantiate(_bloodSplatFX, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                target.Damage(25);
            }

        }
    }
}
