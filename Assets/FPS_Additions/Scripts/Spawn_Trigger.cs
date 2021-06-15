using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Trigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnPoint_01;

    [SerializeField]
    private GameObject _spawnPoint_02;

    [SerializeField]
    private GameObject _spawnPoint_03;

    [SerializeField]
    private GameObject _spawnPoint_04;

    [SerializeField]
    private GameObject _Zombie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(_Zombie, _spawnPoint_01.transform.position, Quaternion.identity);
            Instantiate(_Zombie, _spawnPoint_02.transform.position, Quaternion.identity);
            Instantiate(_Zombie, _spawnPoint_03.transform.position, Quaternion.identity);
            Instantiate(_Zombie, _spawnPoint_04.transform.position, Quaternion.identity);
            Destroy(this.gameObject); 
        }
    }
}
