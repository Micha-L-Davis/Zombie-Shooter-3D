using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable_Triggers : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _triggerToEnable;
    void Start()
    {
        _triggerToEnable.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _triggerToEnable.SetActive(true);
        }
    }
}
