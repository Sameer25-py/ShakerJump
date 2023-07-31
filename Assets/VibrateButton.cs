using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class VibrateButton : MonoBehaviour
    {
        public int  ID;
        public bool Vibrate;

        public static Action<int> VibrateButtonPressed;
        public        Sprite      EnableSprite, DisableSprite;

        private Button _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
            VibrateButtonPressed += OnVibrateRadioButtonPressed;
        }

        private void OnVibrateRadioButtonPressed(int id)
        {
            if (id == ID)
            {
                GameManager.EnableVibration = Vibrate;
            }
            else
            {
                _button.image.sprite = DisableSprite;
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (_button.image.sprite == DisableSprite)
            {
                _button.image.sprite = EnableSprite;
            }

            VibrateButtonPressed?.Invoke(ID);
        }
    }
}