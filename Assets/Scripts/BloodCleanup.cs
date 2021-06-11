using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCleanup : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Hide", 2);
    }

    void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
