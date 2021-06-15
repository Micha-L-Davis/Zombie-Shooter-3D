using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDevHQ;

public class Player_Health : MonoBehaviour
{
    [SerializeField]
    private int _playerHealth;
    private int _maxHealth;
    private Text _playerHealthTextField;
    private int _damage = 10;
    private bool _cooldown;
    [SerializeField]
    private GameObject _blackOverlay;
    private Animator _bloodOverlay;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealthTextField = GameObject.Find("Player_Health").GetComponent<Text>();
        _playerHealth = 100;
        _maxHealth = 100;
        _bloodOverlay = GameObject.Find("Blood_Overlay").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerHealthTextField.text = _playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (_cooldown == false)
            {
                _playerHealth -= _damage;

                if (_playerHealth >= 40)
                {
                    _bloodOverlay.SetTrigger("Phase1");
                }

                if (_playerHealth < 40)
                {
                    _bloodOverlay.SetTrigger("Phase2");
                }

                if (_playerHealth <= 0)
                {
                    playerDeath();
                }
            }
        }
    }

    public void playerDeath()
    {
        
        Time.timeScale = 0.0f;
        _blackOverlay.SetActive(true);
        
    }

    IEnumerator DamageCooldown()
    {
        _cooldown = true;
        yield return new WaitForSeconds(2.0f);
        _cooldown = false;
    }
}
