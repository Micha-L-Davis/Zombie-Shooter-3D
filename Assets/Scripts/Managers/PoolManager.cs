using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;

    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
                throw new UnityException("The Object Pool Manager is NULL");
            return _instance;
        }
    }

    [SerializeField]
    private GameObject _bloodSpatterPrefab;
    [SerializeField]
    private List<GameObject> _bloodSpatterPool;
    [SerializeField]
    private int _amountOfBlood = 10;

    private void Start()
    {
        _bloodSpatterPool = GenerateBloodSpatter();
    }

    List<GameObject> GenerateBloodSpatter()
    {
        for (int i = 0; i < _amountOfBlood; i++)
        {
            GameObject blood = Instantiate(_bloodSpatterPrefab, this.transform);
            blood.SetActive(false);
            _bloodSpatterPool.Add(blood);
        }

        return _bloodSpatterPool;
    }    

    public GameObject RequestBloodSpatter()
    {
        foreach (var item in _bloodSpatterPool)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                return item;
            }
        }
        
        GameObject blood = Instantiate(_bloodSpatterPrefab, this.transform);
        _bloodSpatterPool.Add(blood);
        return blood;
    }

    private void Awake()
    {
        _instance = this;
    }
}
