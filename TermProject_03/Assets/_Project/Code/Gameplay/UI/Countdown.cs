using _Project.Code.Core.General;
using TMPro;
using UnityEngine;


[DefaultExecutionOrder(-10)]
public class Countdown : Singleton<Countdown>
{
    [SerializeField] private TextMeshProUGUI text;
    private float _timeTo;
    private float _countUp;
    private int _timeAt;

    private bool _isCounting = false;


    private void Start()
    {
        text.enabled = false;
    }

    public void StartCountdown(int time)
    {
        _timeTo = time;
        _timeAt = 0;
        _countUp = 0.0f;

        text.text = _timeTo.ToString();
        text.enabled = true;

        _isCounting = true;
    }

    private void Update()
    {
        if (!_isCounting) return;

        _countUp += Time.deltaTime;

        if (_countUp >= 1.0f)
        {
            _timeAt++;
            _countUp = 0.0f;
        }

        text.text = (_timeTo - _timeAt).ToString();

        if (_timeTo - _timeAt == 0)
        {
            _isCounting = false;
            text.enabled = false;
        }
    }
}
