using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _joystickBackground;
    [SerializeField] private RectTransform _joystickHandle;
    
    private Vector2 _inputDirection;
    private float _maxRadius;

    private void Start()
    {
        _maxRadius = _joystickBackground.rect.width / 2;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputDirection = Vector2.zero;
        _joystickHandle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground, eventData.position, eventData.pressEventCamera, out position))
        {
            position = Vector2.ClampMagnitude(position, _maxRadius);
            _inputDirection = position / _maxRadius;
            _joystickHandle.anchoredPosition = position;
        }
    }

    public Vector2 GetInputDirection()
    {
        return _inputDirection;
    }
}