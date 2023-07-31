using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Shacker : MonoBehaviour
    {
        public  bool    IsGameStarted;
        private bool    _currentSide;
        private Vector3 _angle0 = new Vector3(0f, 0f, 0f);
        private Vector3 _angle1 = new Vector3(0f, 0f, 180f);

        private Vector3     _defaultPosition;
        private Rigidbody2D _rb2D;

        private Vector2 _cachedVelocity;

        public float Force = 100f;

        private void OnEnable()
        {
            _defaultPosition = transform.position;
            _rb2D            = GetComponent<Rigidbody2D>();
        }

        public void StartShacking()
        {
            _currentSide       = false;
            IsGameStarted      = true;
            transform.position = _defaultPosition;
            transform.rotation = Quaternion.identity;
            _rb2D.gravityScale = 0.6f;
            _rb2D.simulated    = true;
        }


        public void ResumeShacking()
        {
            IsGameStarted   = true;
            _rb2D.simulated = true;
        }

        public void StopShacking()
        {
            IsGameStarted   = false;
            _rb2D.simulated = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!IsGameStarted) return;
            Column column = col.GetComponent<Column>();
            if ((column.ID == 0 && !_currentSide) || (column.ID == 1 && _currentSide))
            {
                _rb2D.velocity = Vector2.zero;
                _rb2D.AddForce(new Vector2(0f, 1f) * Force);
                GameManager.IncrementScore?.Invoke(1);
            }
            else
            {
                _rb2D.simulated = false;
                IsGameStarted   = false;

                GameManager.GameEnded?.Invoke();
            }
        }


        private void Update()
        {
            if (!IsGameStarted) return;
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(0))
            {
                _currentSide = !_currentSide;
            }
        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                !_currentSide ? Quaternion.Euler(_angle0) : Quaternion.Euler(_angle1), Time.deltaTime * 5f);
        }
    }
}