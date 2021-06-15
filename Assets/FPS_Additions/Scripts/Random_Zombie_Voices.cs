using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Zombie_Voices : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audiosource;
    [SerializeField]
    private AudioClip[] _audioclips;
    private AudioClip _audioClipSFX;
    private bool _playNow = false;
    private Animator _anim;
    private bool _zombieIsDead;

    // Start is called before the first frame update
    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        _playNow = true;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Death") || _anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            _zombieIsDead = true;
        }

        if (_zombieIsDead == false)
        {
            if (_playNow == true)
            {
                StartCoroutine(VoiceTimer());
                _playNow = false;
            }
        }
    }

    public void PlayVoice()
    {
        _audiosource.clip = _audioclips[Random.Range(0, _audioclips.Length)];
        _audiosource.Play();
    }

    IEnumerator VoiceTimer()
    {
        PlayVoice();
        yield return new WaitForSeconds(_audiosource.clip.length);
        _playNow = true;
    }
}
