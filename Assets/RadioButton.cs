using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RadioButton : MonoBehaviour
    {
        public static Action<int> RadioButtonPressed;
        public        int         ID;
        public        bool        Mute;

        public Sprite      EnableSprite, DisableSprite;
        public AudioSource AudioSource;

        private Button _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
            RadioButtonPressed += OnRadioButtonPressed;
        }

        private void OnRadioButtonPressed(int id)
        {
            if (id == ID)
            {
                AudioSource.mute = Mute;
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

            RadioButtonPressed?.Invoke(ID);
        }
    }
}