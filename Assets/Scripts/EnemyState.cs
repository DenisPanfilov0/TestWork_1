using System;
using DefaultNamespace;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private float _visibilityRange = 10f;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private Transform _enemySkin;
    [SerializeField] private float _damageCooldown = 1.0f;
    
    private CharacterState _characterState;
    private float _lastDamageTime;
    
    public event Action<int> ChangeHealth;
    public event Action DropLoot;

    private void Start()
    {
        _targetPlayer = GameObject.FindWithTag("Player").transform;
        _characterState = _targetPlayer.GetComponent<CharacterState>();
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _targetPlayer.position);

        if (distanceToPlayer <= _visibilityRange)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 directionToPlayer = (_targetPlayer.position - transform.position).normalized;
        transform.Translate(directionToPlayer * (_moveSpeed * Time.deltaTime));

        if (directionToPlayer.x < 0)
        {
            _enemySkin.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            _enemySkin.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    
    public int GetHealth()
    {
        return _health;
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _lastDamageTime = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - _lastDamageTime >= _damageCooldown)
            {
                Attack();
                _lastDamageTime = Time.time;
            }
        }
    }

    private void Attack()
    {
        _characterState.TakeDamage(_damage);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (ChangeHealth != null) ChangeHealth(_health);
        if (_health <= 0)
        {
            if (DropLoot != null) DropLoot();
            Destroy(gameObject);
        }
    }
}