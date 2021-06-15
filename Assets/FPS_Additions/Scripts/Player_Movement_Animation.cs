using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Animation : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //horizontal inputs (a, d, leftarrow, rightarrow)
        float v = Input.GetAxis("Vertical"); //veritical inputs (w, s, uparrow, downarrow)

        Move(h, v);

    }

    private void Move(float h, float v)
    {
        _animator.SetFloat("VelX", h, 1f, Time.smoothDeltaTime * 10f);
        _animator.SetFloat("VelY", v, 1f, Time.smoothDeltaTime * 10f);
    }
}
