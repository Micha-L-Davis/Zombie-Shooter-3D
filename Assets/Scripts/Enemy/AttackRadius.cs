using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour
{
    EnemyAI _enemy;
    private void Start()
    {
        _enemy = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                StartCoroutine(_enemy.AttackRoutine(player));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemy.EndCombat();
        }
    }
}
