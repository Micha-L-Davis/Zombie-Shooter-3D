using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour
{
    // Start is called before the first frame update
    private Text _percentageText;
    void Start()
    {
        _percentageText = GameObject.Find("Loading_Percentage").GetComponent<Text>();
        StartCoroutine(LoadLevelwithAsync());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadLevelwithAsync()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level_Design");

        while (operation.isDone == false)
        {
            _percentageText.text = (operation.progress * 100) + "%";
            yield return new WaitForEndOfFrame(); 
        }
    }
}
