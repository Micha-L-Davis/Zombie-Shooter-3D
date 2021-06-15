using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Fire : MonoBehaviour
{
    [SerializeField]
    [Header("Place GO with Animator Here", order = 0)]
    private GameObject _player;
    [SerializeField]
    [Header("Place GO with Smoke Here", order = 1)]
    private ParticleSystem _smoke;
    [SerializeField]
    [Header("Place Fire Audio Clip Here", order = 2)]
    private AudioClip _audioClip;
    [SerializeField]
    [Header("Place Reload Audio Clip Here", order = 3)]
    private AudioClip _audioClipReload;
    [SerializeField]
    [Header("Bullet Clip Size", order = 4)]
    private int _clipSize;
    [SerializeField]
    [Header("Total Bullets Left", order = 5)]
    public int _bullets;
    [Header("Damage", order = 6)]
    public int damage;
    [SerializeField]
    [Header("Place Camera Here", order = 7)]
    private Camera _cam;
    [SerializeField]
    [Header("Place Bullet Hole Prefabs Here", order = 8)]
    private GameObject[] _bulletHoles;
    
    private Animator _anim;
    private AudioSource _audioSource;
    private bool _reload;
    private bool _reloadCooldown;
    private int layerMask = 1 << 8;// used to hide raycast from hitting the player and children

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _smoke = transform.Find("Smoke").GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //pull trigger
        {
            if (_player.GetComponent<Movement>()._areAiming == true)
            {
                if (_bullets >= 1 && _reloadCooldown == false)// checks if the player has bullets in the gun
                {
                    _anim.SetTrigger("Fire");
                    _smoke.Play();
                    RandomizeAudioClip();
                    Shoot();
                    _bullets -= 1;
                }

                else if (_bullets < 1)// if fired and no bullets, it reloads the gun
                {
                    StartCoroutine(WeaponCoolDown());
                    _anim.SetTrigger("Reload");
                    _bullets = 8;
                    PlayReloadClip();
                }
            }
        }
    }


    void RandomizeAudioClip() // randomizes the firing sound slightly
    {
        _audioSource.clip = _audioClip;
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.Play();
    }

    void PlayReloadClip()
    {
        _audioSource.clip = _audioClipReload;
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.Play();
    }

    void Shoot()
    {
        Vector3 origin = _cam.transform.position;
        Vector3 direction = _cam.transform.forward;

        Ray rayOrigin = new Ray(origin, direction);
        RaycastHit hitInfo;

        Debug.DrawRay(origin, direction * 10, Color.red);

        if (Physics.Raycast(rayOrigin, out hitInfo, layerMask))
        {
            if (hitInfo.collider != null)
            {
                Target target = hitInfo.collider.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                    Debug.Log(hitInfo.collider.name);
                }

                Instantiate(_bulletHoles[Random.Range(0, 3)], hitInfo.point, Quaternion.LookRotation(hitInfo.normal));                
            }
        }
    }

    IEnumerator WeaponCoolDown()// reload cooldown 
    {
        _reloadCooldown = true;
        yield return new WaitForSeconds(1.25f);
        _reloadCooldown = false;
    }
}
