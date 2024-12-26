using System;
using Source.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using Random = UnityEngine.Random;

namespace Source.CellManagement.Mono
{
    public class ItemMono : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        private bool _isSwapping = false;
        public bool IsSwapping => _isSwapping;

        private Transform _transform;
        public Transform Transform => _transform;
        private SpriteRenderer _spriteRenderer;
        public int index { get; private set; }

        public Action<ItemMono> OnDeath;

        [Inject] private GameSettingsScriptable _gameSettingsScriptable;

        private void Awake()
        {
            _transform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetRandomImage()
        {
            index = Random.Range(0, _gameSettingsScriptable.EggSprites.Length);
            _spriteRenderer.sprite = _gameSettingsScriptable.EggSprites[index];
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }
        
        public bool Move(Vector3 position)
        {
            _isSwapping = Vector3.Distance(_transform.position, position) > 0.05f;
            _transform.position = Vector3.MoveTowards(_transform.position, position, Time.deltaTime * _moveSpeed);
            return _isSwapping;
        }

        public void Death()
        {
            OnDeath?.Invoke(this);
        }
    }
}