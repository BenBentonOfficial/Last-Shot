using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer 
{
    public float _timer { get; private set; } = 0;
    public float _timerLength { get; private set; } = 0;

    public bool Ready => _timer.Equals(_timerLength);
    private bool HasSlider => _cooldownSlider != null;

    private Slider _cooldownSlider;

    public Timer(float timerLength)
    {
        _timerLength = timerLength;
        _timer = _timerLength;
    }

    public Timer(float timerLength, Slider cooldownSlider)
    {
        _timerLength = timerLength;
        _timer = timerLength;
        _cooldownSlider = cooldownSlider;
        _cooldownSlider.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _timer = 0;
        _timerLength = 0;
    }

    public IEnumerator StartTimer()
    {
        _timer = 0;

        if (HasSlider)
        {
            _cooldownSlider.gameObject.SetActive(true);
        }

        while (_timer < _timerLength)
        {
            yield return new WaitForEndOfFrame();
            _timer += Time.deltaTime;
            _timer = Mathf.Clamp(_timer, 0, _timerLength);
            if(HasSlider)
                _cooldownSlider.value = _timer / _timerLength;
            
            //Debug.Log("Timer: " + _timer + "  ::  " + _timerLength);
        }

        if (HasSlider)
        {
            _cooldownSlider.gameObject.SetActive(false);
        }
    }
}
