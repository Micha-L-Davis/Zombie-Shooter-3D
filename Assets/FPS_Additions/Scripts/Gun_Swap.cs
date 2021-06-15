using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Swap : MonoBehaviour
{
    [SerializeField]
    private GameObject _gun1;
    [SerializeField]
    private GameObject _gun2;
    [SerializeField]
    private SmoothDamp _LHandIK;
    [SerializeField]
    private SmoothDamp _RHandIK;
    [SerializeField]
    private Transform _LHandPOS_Gun1;
    [SerializeField]
    private Transform _RHandPOS_Gun1;
    [SerializeField]
    private Transform _LHandPOS_Gun2;
    [SerializeField]
    private Transform _RHandPOS_Gun2;

    public bool _enableMachineGun = false;

    // Start is called before the first frame update
    void Start()
    {
        _gun1.SetActive(true);
        _gun2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _gun1.SetActive(true);
            _gun2.SetActive(false);
            _LHandIK.target = _LHandPOS_Gun1;
            _RHandIK.target = _RHandPOS_Gun1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_enableMachineGun == true)
            {
                EnableMachineGun();
            }
        }       
    }

    public void EnableMachineGun()
    {
        _gun1.SetActive(false);
        _gun2.SetActive(true);
        _LHandIK.target = _LHandPOS_Gun2;
        _RHandIK.target = _RHandPOS_Gun2;
    }
}
