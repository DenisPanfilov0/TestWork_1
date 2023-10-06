using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HealthBarPlayer : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSlider;
        [SerializeField] private CharacterState _characterState;
        [SerializeField] private Transform _heroHead;

        private void Start()
        {
            _healthBarSlider.maxValue = _characterState.GetMaxHealth();
            _healthBarSlider.value = _characterState.GetCurrentHealth();
            _characterState.ChangeHealth += ChangeHealth;
        }
        
        private void FixedUpdate()
        {
            Vector3 headPosition = _heroHead.position;
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
            _characterState.ChangeHealth -= ChangeHealth;
        }
    }
}