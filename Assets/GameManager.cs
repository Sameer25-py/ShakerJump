using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public GameObject MainMenu, Settings, Statistics, IngameUI, PauseMenu, GameOverMenu;

        private int _score, _bestScore, _timesPlayed;

        public TMP_Text ScoreText, ScoreText2, BestScoreText, TimesPlayedText;

        public        Shacker      Shacker;
        public static Action       GameStarted;
        public static Action       GamePaused;
        public static Action       GameEnded;
        public static Action       GameResumed;

        public static bool EnableVibration = true;

        public static Action<int> IncrementScore;

        private void OnEnable()
        {
            IncrementScore += i =>
            {
                if (EnableVibration)
                {
                    Handheld.Vibrate();
                }
                _score          += 1;
                ScoreText.text  =  _score.ToString();
                ScoreText2.text =  _score.ToString();
            };

            GameEnded += () =>
            {
                GameOverMenu.SetActive(true);
                IngameUI.SetActive(false);
                if (_score > _bestScore)
                {
                    _bestScore         = _score;
                    BestScoreText.text = _bestScore.ToString();
                }
            };
        }

        public void ShowHome()
        {
            IngameUI.SetActive(false);
            Statistics.SetActive(false);
            Settings.SetActive(false);
            MainMenu.SetActive(true);
            PauseMenu.SetActive(false);
            GameOverMenu.SetActive(false);
            GamePaused?.Invoke();
            Shacker.StopShacking();
        }

        public void ShowSettings()
        {
            MainMenu.SetActive(false);
            Settings.SetActive(true);
        }

        public void ShowStatistics()
        {
            MainMenu.SetActive(false);
            Statistics.SetActive(true);
        }

        public void StartGame()
        {
            _timesPlayed         += 1;
            _score               =  0;
            ScoreText.text       =  _score.ToString();
            ScoreText2.text      =  _score.ToString();
            TimesPlayedText.text =  _timesPlayed.ToString();

            MainMenu.SetActive(false);
            IngameUI.SetActive(true);
            GameOverMenu.SetActive(false);
            Shacker.StartShacking();
            GameStarted?.Invoke();
        }

        public void ShowPauseMenu()
        {
            IngameUI.SetActive(false);
            PauseMenu.SetActive(true);
            Shacker.StopShacking();
            GamePaused?.Invoke();
        }

        public void ResumeGame()
        {
            PauseMenu.SetActive(false);
            IngameUI.SetActive(true);
            Shacker.ResumeShacking();
            GameResumed?.Invoke();
        }
    }
}