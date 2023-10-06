using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool _isTimeStopped = false;

    public void StopTime()
    {
        if (!_isTimeStopped)
        {
            Time.timeScale = 0f;
            _isTimeStopped = true;
        }
    }

    public void ResumeTime()
    {
        if (_isTimeStopped)
        {
            Time.timeScale = 1f;
            _isTimeStopped = false;
        }
    }
}