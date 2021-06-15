using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Laptop : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _itemsToDestroy;
    [SerializeField]
    private AudioClip _audioclip;
    [SerializeField]
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
            for(int i = 0; i < _itemsToDestroy.Length; i++)
            {
                _itemsToDestroy[i].SetActive(false);
            }
        }
    }
}
