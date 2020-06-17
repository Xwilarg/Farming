using System.Threading;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [SerializeField]
    private Light _sun;

    [SerializeField]
    private DayNightInfo _dayNightInfo;

    private float _timer;
    private DayNightState _state;
    private float _baseIntensity, _nextIntensity;

    private void Start()
    {
        _state = DayNightState.AfterNoon; // We start the day at noon
        _timer = (_dayNightInfo.dayLength * 60f) / 2f; // Remaining timer is only half a day
        _baseIntensity = 1;
        _nextIntensity = 0;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            switch (_state)
            {
                case DayNightState.AfterNoon:
                case DayNightState.BeforeNoon:
                    _timer = (_dayNightInfo.dayLength * 60f) / 2f;
                    break;

                case DayNightState.BeforeNight:
                    _timer = _dayNightInfo.nightLength * 60f;
                    break;
            }
        }
        else
        {

        }
    }

    public enum DayNightState
    {
        BeforeNoon,
        AfterNoon,
        BeforeNight
    }
}
