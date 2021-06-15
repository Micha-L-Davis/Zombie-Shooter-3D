using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_menu : MonoBehaviour
{

    private bool isPaused = false;
    [SerializeField]
    private GameObject _pauseMenuOverlay;
    [SerializeField]
    private AudioClip _audioClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenuOverlay.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            _audioSource.PlayOneShot(_audioClip);
        }
    }

    public void PauseGame()
    {
        if (isPaused == true)
        {
            Time.timeScale = 0.0f;
            _pauseMenuOverlay.SetActive(true);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 1.0f;
            _pauseMenuOverlay.SetActive(false);
            isPaused = true;
        }
    }
}
