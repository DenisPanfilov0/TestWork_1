using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CharacterState : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private int _armor;
    [SerializeField] private int _ammoCount;
    [SerializeField] private int _radiusEnemyTarget;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private Button _attackButton;
    [SerializeField] private float _verticalOffset;
    public event Action<int> ChangeHealth;
    public event Action<int> ChangeAmmo;

    private void Start()
    {
        _attackButton.onClick.AddListener(AttackOnClick);
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position + Vector3.up * _verticalOffset;
        if (Physics2D.OverlapCircleAll(currentPosition, _radiusEnemyTarget, enemyLayerMask).Length >= 1f)
        {
            _attackButton.interactable = true;
        }
        else
        {
            _attackButton.interactable = false;
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (ChangeHealth != null) ChangeHealth(_currentHealth);
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AttackOnClick()
    {
        Vector3 currentPosition = transform.position + Vector3.up * _verticalOffset;
        Collider2D[] enemyColliders =
            Physics2D.OverlapCircleAll(currentPosition, _radiusEnemyTarget, enemyLayerMask);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemyCollider in enemyColliders)
        {
            EnemyState enemy = enemyCollider.GetComponent<EnemyState>();
            if (enemy != null)
            {
                Vector2 enemyCenter = enemyCollider.bounds.center;
                Vector2 direction = (enemyCenter - (Vector2)currentPosition).normalized;
                RaycastHit2D hit =
                    Physics2D.Raycast(currentPosition, direction, _radiusEnemyTarget, enemyLayerMask);
                if (hit.collider != null && hit.collider.CompareTag("Enemy") && hit.distance < closestDistance)
                {
                    closestEnemy = hit.transform;
                    closestDistance = hit.distance;
                }
            }
        }

        if (closestEnemy != null)
        {
            EnemyState closestEnemyState = closestEnemy.GetComponent<EnemyState>();
            if (closestEnemyState != null && _ammoCount > 0)
            {
                closestEnemyState.TakeDamage(_damage);
                _ammoCount--;
                if (ChangeAmmo != null) ChangeAmmo(_ammoCount);
            }
        }
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetAmmoCount()
    {
        return _ammoCount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 drawGizmos = transform.position + Vector3.up * _verticalOffset;
        Gizmos.DrawWireSphere(drawGizmos, _radiusEnemyTarget);
    }

    public CharacterStateData GetCharacterData()
    {
        return new CharacterStateData
        {
            CurrentHealth = _currentHealth,
            MaxHealth = _maxHealth,
            Damage = _damage,
            Armor = _armor,
            AmmoCount = _ammoCount,
        };
    }

    public void SetCharacterData(CharacterStateData data)
    {
        _currentHealth = data.CurrentHealth;
        _maxHealth = data.MaxHealth;
        _damage = data.Damage;
        _armor = data.Armor;
        _ammoCount = data.AmmoCount;
    }
}