using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace View
{
    public class AmmoCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammoCount;
        [SerializeField] private CharacterState _characterState;

        private void Start()
        {
            _characterState.ChangeAmmo += ChangeAmmo;
            _ammoCount.text = _characterState.GetAmmoCount().ToString();
        }

        private void ChangeAmmo(int ammoCount)
        {
            _ammoCount.text = _characterState.GetAmmoCount().ToString();
        }

    }
}