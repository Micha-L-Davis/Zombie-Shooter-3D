using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_Fire_Pistol : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _smoke;

    [SerializeField]
    private ParticleSystem _bulletCasing;

    [SerializeField]
    private ParticleSystem _muzzleFlashSide;

    [SerializeField]
    private ParticleSystem _Muzzle_Flash_Front;

    private Animator _anim;

    [SerializeField]
    private AudioClip _gunShotAudioClip;

    [SerializeField]
    private AudioClip _reloadAudioClip;

    [SerializeField]
    private AudioSource _audioSource;

    private Raycast _raycast;

    [SerializeField]
    private int _clipSize;//total amount of bullets inside each clip
    public int _remainingBulletsInClip;//total amount of bullets inside my clip
    public int totalBullets;//total amount of bullets carried
    public int maxBulletsAvailable; //cap of how many bullets can be carried
    private int _bullet_Refill_Amount;// amount of bullets added when picking up ammo
    [SerializeField]

    private bool _reload;
    private bool _reloadCooldown;

    [SerializeField]
    private Text _totalBulletInInventory;

    [SerializeField]
    private Text _ammoTextHolder;
    public bool FullAuto;
    [SerializeField]
    float _fireCooldown;
    [SerializeField]
    float _autoFireCooldown;
    float _canfire = -1;
    [SerializeField]
    PoolManager _poolManager;
    Transform _camera;
    [SerializeField]
    int _damageAmount;


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _raycast = GetComponent<Raycast>();
        _ammoTextHolder = GameObject.Find("Ammo_Count").GetComponent<Text>();
        _totalBulletInInventory = GameObject.Find("Total_Bullets_Inventory").GetComponent<Text>();
    }

    public void Fire()
    {
        if (_remainingBulletsInClip >= 1 && _reloadCooldown == false)
        {
            if (FullAuto == false)
            {
                _anim.SetTrigger("Fire");
                RayCast();
                _canfire = Time.time + _fireCooldown;
            }

            if (FullAuto == true)
            {
                _anim.SetBool("Automatic_Fire", true);
                RayCast();
                _canfire = Time.time + _autoFireCooldown;
            }
        }
        if (_remainingBulletsInClip < 1)
        {
            _anim.SetBool("Automatic_Fire", false);
            ReloadWeapon();
        }
    }

    private void RayCast()
    {
        RaycastHit hit;
        if (Time.time > _canfire && Physics.Raycast(_camera.position, _camera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            IDamageable target = hit.transform.GetComponent<IDamageable>();
            if (target != null)
            {
                //reuse spatter
                //Instantiate(_bloodSplatFX, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                GameObject blood = _poolManager.RequestBloodSpatter();
                blood.transform.position = hit.point;
                blood.transform.rotation = Quaternion.LookRotation(hit.normal);
                target.Damage(_damageAmount);
            }

        }
    }

    public void ReleaseAuto()
    {
        if (FullAuto == true)
        {
            _anim.SetBool("Automatic_Fire", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BulletCount();
        _ammoTextHolder.text = _remainingBulletsInClip.ToString() + " / " + _clipSize.ToString();
    }


    public void FireGunParticles()
    {
        Debug.Log("Fired gun particles");
        _smoke.Play();
        _bulletCasing.Play();
        _muzzleFlashSide.Play();
        _Muzzle_Flash_Front.Play();
        GunFireAudio();
        _raycast.Shoot();
        _remainingBulletsInClip -= 1;
        
    }

    public void GunFireAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }

    public void ReloadAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_reloadAudioClip);
    }

    IEnumerator WeaponCoolDown()// reload cooldown 
    {
        _reloadCooldown = true;
        yield return new WaitForSeconds(1.25f);
        _reloadCooldown = false;
    }

    public void ReloadWeapon()
    {
        if (totalBullets <= 0)// you have no more bullets
        {
            //play empty bullet firing animation - need to create
            //no reload allowed

        }
        if (totalBullets > 0)//If you can reload
        {
            int bulletCount;
            bulletCount = _clipSize - _remainingBulletsInClip;                           
            _remainingBulletsInClip += bulletCount;
            totalBullets -= bulletCount;//remove bullets from inventory and place into clip
            if (totalBullets < 0)//If you reload with a negative total bullet count
            {
                int negative_bullet_count;
                negative_bullet_count = totalBullets;
                _remainingBulletsInClip += negative_bullet_count;
                totalBullets = 0;//Fixes count so remainder of bullets go into clip and sets total to 0
            }
            _anim.SetTrigger("Reload");
            StartCoroutine(WeaponCoolDown());
        }        
    }

    public void AddBullets()
    {
        int temp_bullet_amt;
        temp_bullet_amt = totalBullets + _bullet_Refill_Amount;

        if (temp_bullet_amt > maxBulletsAvailable)// if you are collecting more bullets than your max
        {
            totalBullets = maxBulletsAvailable;//Just set your bullet count to max
        }

        else
        {
            totalBullets = totalBullets + _bullet_Refill_Amount;//if you are collecting less than your max, just add it
        }
    }

    public void BulletCount()
    {
        int tempTotal;
        tempTotal = totalBullets + _remainingBulletsInClip;
        _totalBulletInInventory.text = "Ammo:" + tempTotal.ToString();
    }
}
