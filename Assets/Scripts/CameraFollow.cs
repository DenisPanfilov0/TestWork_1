using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    public int offset;

    private void LateUpdate()
    {
        if (_player != null)
        {
            var position = _player.position;
            transform.position = new Vector3(position.x, position.y, offset);
            transform.LookAt(position);
        }
    }
}