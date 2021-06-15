using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Level : MonoBehaviour
{

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

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level_Design");        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void LoadingScreen()
    {
        SceneManager.LoadScene("Loading_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAudio()
    {
        _audioSource.PlayOneShot(_audioclip);
    }

}
