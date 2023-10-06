using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HealthBarEnemy : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSlider;
        [SerializeField] private EnemyState _enemyState;
        [SerializeField] private Transform _enemyHead;

        private void Start()
        {
            _healthBarSlider.maxValue = _enemyState.GetHealth();
            _healthBarSlider.value = _enemyState.GetHealth();
            _enemyState.ChangeHealth += ChangeHealth;
        }
        
        private void FixedUpdate()
        {
            Vector3 headPosition = _enemyHead.position;
            headPosition.y += 1.0f;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(headPosition);
            _healthBarSlider.transform.position = screenPosition;
        }
        
        private void ChangeHealth(int currentHealth)
        {
            _healthBarSlider.value = currentHealth;
        }

        private void OnDestroy()
        {
            _enemyState.ChangeHealth -= ChangeHealth;
        }
    }
}