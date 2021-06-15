using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    Player _player;
    Animator _anim;
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _playerDistance = 25;

    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }
    [SerializeField]
    EnemyState _currentState = EnemyState.Chase;

    public int Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            int flip = Random.Range(0, 2);
            if (_currentHealth < 50 && flip == 1 && !_anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Crawl"))
                _anim.SetTrigger("Crawl");
            if (_currentHealth < 1 && _anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Crawl"))
                _anim.SetTrigger("Death_Prone");
            else if (_currentHealth < 1 && _anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Walk"))
                _anim.SetTrigger("Death");
        }
    }

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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (_player == null)
            throw new UnityException("Player is NULL");
        _anim = transform.Find("Man_01_Dismembered").GetComponent<Animator>();
        if (_anim == null)
            throw new UnityException("Animator is NULL");
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
            throw new UnityException("NaveMesh Agent is NULL");
    }

    void LateUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;
            case EnemyState.Attack:
                _anim.SetBool("Attack", true);
                _agent.speed = 0;
                if (_agent.remainingDistance >= 1.5f)
                {
                    EndCombat();
                }
                break;
            default:
                break;
        }

        Animate();
    }

    private void Animate()
    {
        if (_agent.velocity.magnitude > 0.1f)
        {
            _anim.SetBool("Walk", true);
        }

        if (_agent.velocity.magnitude < 0.1f)
        {
            _anim.SetBool("Walk", false);
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Death"))
        {
            _speed = 0;
            StartCoroutine(DestroyZombie());
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Death_Prone"))
        {
            _speed = 0;
            StartCoroutine(DestroyZombie());
        }
    }

    private void CalculateMovement()
    {
        if (Vector3.Distance(_player.transform.position, this.transform.position) < _playerDistance)
        {
            _agent.SetDestination(_target.position);
        }

        if (_agent.remainingDistance < 1.5f)
        {
            _currentState = EnemyState.Attack;
        }
        //if (_controller.isGrounded)
        //{
        //    _direction = Vector3.Normalize(_player.position - transform.position);
        //    _velocity = _direction * _speed;
        //}
        //else
        //{
        //    _yVelocity -= _gravity;
        //}

        //_velocity.y = _yVelocity;
        //_controller.Move(_velocity * Time.deltaTime);

        //if (_controller.velocity != Vector3.zero)
        //{
        //    Quaternion toRotate = Quaternion.LookRotation(_direction, Vector3.up);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, _rotationSpeed * Time.deltaTime);
        //}


    }

    //public IEnumerator AttackRoutine()
    //{
    //    _currentState = EnemyState.Attack;
    //    while (_currentState == EnemyState.Attack)
    //    {
    //        _player.Damage(10);
    //        yield return new WaitForSeconds(_attackSpeed);
    //    }
    //}

    public void EndCombat()
    {
        _anim.SetBool("Attack", false);
        _agent.speed = _speed;
        _currentState = EnemyState.Chase;
    }

    IEnumerator DestroyZombie()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }
}
