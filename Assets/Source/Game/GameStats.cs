using System;
using Source.Level.Mono;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Source.Game
{
    public class GameStats
    {
        [Inject] private readonly GameSettingsScriptable _gameSettingsScriptable;
        private LevelScriptable _levelScriptable;
        private int _currentIndex;
        public int CurrentIndex => _currentIndex;

        private float _time;
        public float LevelTime => _levelScriptable.Duration;
        private int _score;
        private int[] _scoreArray;
        private int[] _currentScoreArray;

        private bool _isGameStarted = false;
        public bool IsGameStarted => _isGameStarted;

        public Action OnStartLevel, OnLoseLevel, OnWinLevel, OnStopGame;

        public Action<int, int> OnScoreUpdate;
        public Action<int, int, int> OnItemScoreUpdate;
        public Action<float> OnTimerUpdate;
        public Action<int> OnLevelUpdate;


        public void UpdateTimer()
        {
            if (_isGameStarted && _gameSettingsScriptable.TimerEnable)
            {
                _time = Mathf.Clamp(_time - Time.deltaTime, 0, _levelScriptable.Duration);
                OnTimerUpdate?.Invoke(_time);
                if (_time == 0)
                {
                    OnLoseLevel?.Invoke();
                    _isGameStarted = false;
                    UpdateAllData();
                }
            }
        }

        public void AddScore(int value)
        {
            _score = Mathf.Clamp(_score + value, 0, _levelScriptable.MaxScore);
            OnScoreUpdate?.Invoke(_score, _levelScriptable.MaxScore);
            if (_score == _levelScriptable.MaxScore)
            {
                PlayerPrefs.SetInt("Level_"+(_currentIndex+1), 1);
                OnWinLevel?.Invoke();
                _isGameStarted = false;
                UpdateAllData();
            }
        }
        
        public void AddScore(int value, int id)
        {
            if (_scoreArray.Length < id) return;
            
            _currentScoreArray[id] = Mathf.Clamp(_currentScoreArray[id] + value, 0, _scoreArray[id]);
            Debug.Log(_currentScoreArray[id] + "/" + _scoreArray[id]);
            OnItemScoreUpdate?.Invoke(_currentScoreArray[id], _scoreArray[id], id);
            bool isComplete = true;
            for (int i = 0; i < _gameSettingsScriptable.EggSprites.Length; i++)
            {
                if (_currentScoreArray[i] != _scoreArray[i])
                {
                    isComplete = false;
                }
            }
            if (isComplete)
            {
                PlayerPrefs.SetInt("Level_"+(_currentIndex+1), 1);
                OnWinLevel?.Invoke();
                _isGameStarted = false;
                UpdateAllData();
            }
        }
        
        public void StartLevel(int index)
        {
            _currentIndex = index;
            _levelScriptable = _gameSettingsScriptable.LevelScriptables[_currentIndex];
            Restart();
        }

        public void NextLevel()
        {
            StartLevel(Mathf.Clamp(_currentIndex+1, 0, _gameSettingsScriptable.LevelScriptables.Length));
        }

        public void Pause()
        {
            _isGameStarted = false;
        }

        public void Resume()
        {
            _isGameStarted = true;
        }

        public void Restart()
        {
            _time = _levelScriptable.Duration;
            if (_gameSettingsScriptable.ByItemScore)
            {
                _scoreArray = new int[_gameSettingsScriptable.EggSprites.Length];
                _currentScoreArray = new int[_gameSettingsScriptable.EggSprites.Length];
                for (int i = 0; i < _gameSettingsScriptable.EggSprites.Length; i++)
                {
                    _scoreArray[i] = Random.Range(_levelScriptable.MinScore, _levelScriptable.MaxScore);
                }
            }
            else
            {
                _score = 0;
            }
            
            _isGameStarted = true;
            OnStartLevel?.Invoke();
            UpdateAllData();
        }

        private void UpdateAllData()
        {
            OnTimerUpdate?.Invoke(_time);
            if (_gameSettingsScriptable.ByItemScore)
            {
                for (int i = 0; i < _gameSettingsScriptable.EggSprites.Length; i++)
                {
                    OnItemScoreUpdate?.Invoke(_currentScoreArray[i], _scoreArray[i], i);
                }
            }
            else
            {
                OnScoreUpdate?.Invoke(_score, _levelScriptable.MaxScore);
            }

            OnLevelUpdate?.Invoke(_currentIndex+1);
        }

        public void Menu()
        {
            _isGameStarted = false;
            OnStopGame?.Invoke();
        }
    }
}