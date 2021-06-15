using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private Gun_Fire_Pistol _gunFire;
    [SerializeField]
    private Camera _cam;
    private int layerMask = 1 << 8;// used to hide raycast from hitting the player and children
    [SerializeField]
    private GameObject []_bloodSplatter;


    [Header("Damage", order = 6)]
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        _gunFire = GetComponent<Gun_Fire_Pistol>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        Vector3 origin = _cam.transform.position;
        Vector3 direction = _cam.transform.forward;

        Ray rayOrigin = new Ray(origin, direction);
        RaycastHit hitInfo;

        Debug.DrawRay(origin, direction * 10, Color.red);

        if (Physics.Raycast(rayOrigin, out hitInfo, layerMask))
        {
            if (hitInfo.collider != null)
            {
                Target target = hitInfo.collider.GetComponent<Target>();
                if (target != null)
                {
                    if (hitInfo.collider.tag == "Enemy")
                    {
                        target.TakeDamage(damage);
                        Debug.Log(hitInfo.collider.name);
                        Instantiate(_bloodSplatter[Random.Range(0, 3)], hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    }
                }
            }
        }
    }
}
