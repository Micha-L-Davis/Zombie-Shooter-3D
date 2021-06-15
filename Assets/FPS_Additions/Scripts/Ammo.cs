using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int bullets;

    [SerializeField]
    private Gun_Fire_Pistol _gun1;
    [SerializeField]
    private AudioClip _pickupAudio;
    [SerializeField]
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _gun1 = GameObject.Find("USP").GetComponent<Gun_Fire_Pistol>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (_gun1.totalBullets != _gun1.maxBulletsAvailable)
            {
                _gun1 = GameObject.Find("USP").GetComponent<Gun_Fire_Pistol>();
                
                _gun1.totalBullets += bullets;
                _gun1.AddBullets();

                
                Destroy(this.gameObject, 1.0f);
                audioSource.PlayOneShot(_pickupAudio);
            }
        }
    }
}
