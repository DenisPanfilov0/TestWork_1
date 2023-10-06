using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _playerSkin;
    
    private VirtualJoystick _virtualJoystick;

    private void Start()
    {
        _virtualJoystick = FindObjectOfType<VirtualJoystick>();
    }

    private void FixedUpdate()
    {
        Vector2 inputDirection = _virtualJoystick.GetInputDirection();
        Vector3 movement = new Vector3(inputDirection.x, inputDirection.y,0f);
        transform.Translate(movement * (_moveSpeed * Time.deltaTime));
        
        if (inputDirection.x < 0)
        {
            _playerSkin.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (inputDirection.x > 0)
        {
            _playerSkin.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}