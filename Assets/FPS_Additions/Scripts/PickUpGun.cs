using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    private Gun_Swap _gunSwap;
    [SerializeField]
    private AudioClip _pick_Up_Gun_Audio_Clip;
    [SerializeField]
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _gunSwap = GameObject.Find("Player").GetComponent<Gun_Swap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            _gunSwap._enableMachineGun = true;
            _gunSwap.EnableMachineGun();
            audioSource.PlayOneShot(_pick_Up_Gun_Audio_Clip);
            Destroy(this.gameObject, 1.0f);
        }
    }
}
