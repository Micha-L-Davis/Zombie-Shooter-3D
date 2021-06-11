using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    CharacterController _controller;
    [SerializeField]
    float _speed = 3;
    [SerializeField]
    float _rotationSpeed = 500;
    [SerializeField]
    float _attackSpeed = 1f;
    [SerializeField]
    int _maxHealth = 100;
    [SerializeField]
    int _currentHealth;
    float _yVelocity;
    Vector3 _velocity, _direction;
    float _gravity = 1;
    Transform _player;

    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }
    [SerializeField]
    EnemyState _currentState = EnemyState.Chase;

    public int Health { get { return _currentHealth; } set { _currentHealth = value; } }

    public void Damage(int amount)
    {
        Health -= amount;

        if (Health < 1)
            Destroy(gameObject);
    }

    void Start()
    {
        Health = _maxHealth;
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
            throw new UnityException("Character Controller is NULL");
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_player == null)
            throw new UnityException("Player Transform is NULL");
    }

    void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;
            case EnemyState.Attack:
                break;
            default:
                break;
        }
    }

    private void CalculateMovement()
    {
        if (_controller.isGrounded)
        {
            _direction = Vector3.Normalize(_player.position - transform.position);
            _velocity = _direction * _speed;
        }
        else
        {
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);

        if (_controller.velocity != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(_direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, _rotationSpeed * Time.deltaTime);
        }
    }



    public IEnumerator AttackRoutine(Player player)
    {
        _currentState = EnemyState.Attack;
        while (_currentState == EnemyState.Attack)
        {
            player.Damage(10);
            yield return new WaitForSeconds(_attackSpeed);
        }
    }

    public void EndCombat()
    {
        _currentState = EnemyState.Chase;
    }
}
