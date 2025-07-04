using UnityEngine;
using TMPro;
using System; // for time span calss 

public class Timer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool _timerActive;
    private float _currentTime;
    [SerializeField] private float _startMinutes;
    [SerializeField] private TMP_Text _text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timerActive = true;
        _currentTime = _startMinutes*60;
        
        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _text.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_timerActive)
        {
            _currentTime = _currentTime - Time.deltaTime;
            if(_currentTime <= 0)
            {
                _timerActive = false;
                gameManager.GameOver();
            }
            
        }

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _text.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
        
    }
}
