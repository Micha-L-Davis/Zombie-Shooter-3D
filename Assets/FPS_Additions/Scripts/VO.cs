using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VO : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _audioclip;
    private Collider _collider;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _audioSource.PlayOneShot(_audioclip);
            _collider.enabled = false;
        }
    }
}
