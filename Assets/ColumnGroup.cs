using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ColumnGroup : MonoBehaviour
    {
        private bool _isGameStarted = false;
        
        private Vector3 _defaultPosition;

        private void OnEnable()
        {
            GameManager.GameStarted += OnGameStarted;
            GameManager.GamePaused  += OnGamePaused;
            GameManager.GameEnded   += OnGameEnded;
            GameManager.GameResumed += OnGameResumed;
            _defaultPosition        =  transform.position;
        }

        private void OnGameResumed()
        {
            _isGameStarted = true;
        }

        private void OnGameEnded()
        {  
            _isGameStarted = false;
        }

        private void OnGamePaused()
        {
            _isGameStarted = false;
        }

        private void OnGameStarted()
        {
            transform.position = _defaultPosition;
            _isGameStarted     = true;
        }

        private void Update()
        {
            if (!_isGameStarted) return;
            transform.position += Vector3.left * (Time.deltaTime * 0.9f);
        }
    }
}