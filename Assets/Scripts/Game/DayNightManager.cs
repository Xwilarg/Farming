using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [SerializeField]
    private Light _sun;

    [SerializeField]
    private DayNightInfo _dayNightInfo;

    private float _timer, _refTimer;
    private DayNightState _state;
    private float _maxIntensity;

    private void Start()
    {
        _state = DayNightState.AfterNoon; // We start the day at noon
        _refTimer = (_dayNightInfo.dayLength * 60f) / 2f; // Remaining timer is only half a day
        _timer = 0f;
        _maxIntensity = Options.S.GetInfo().sunIntensity;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _refTimer)
        {
            _state++;
            switch (_state)
            {
                case DayNightState.AfterNoon:
                case DayNightState.BeforeNoon:
                    _refTimer = _dayNightInfo.dayLength * 60f / 2f;
                    break;

                case DayNightState.BeforeNight:
                    _refTimer = _dayNightInfo.nightLength * 60f;
                    break;
            }
            _timer = 0f;
        }
        else
        {
            switch (_state)
            {
                case DayNightState.BeforeNoon:
                    _sun.intensity = _timer * (_maxIntensity / 2f) / _refTimer;
                    break;

                case DayNightState.AfterNoon:
                    _sun.intensity = (_maxIntensity / 2f) + (_timer * (_maxIntensity / 2f) / _refTimer);
                    break;

                case DayNightState.BeforeNight:
                    _sun.intensity = _maxIntensity - (_timer * _maxIntensity / _refTimer);
                    break;
            }
        }
    }

    public enum DayNightState
    {
        BeforeNoon,
        AfterNoon,
        BeforeNight
    }
}
