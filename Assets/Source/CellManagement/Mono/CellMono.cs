using System;
using System.Collections;
using Source.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Source.CellManagement.Mono
{
    public class CellMono : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
    {
        private Vector2Int _position;
        public Vector2Int Position => _position;

        private Transform _transform;
        public Transform Transform => _transform;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;
        
        private ItemMono _currentItem;
        public ItemMono CurrentItem => _currentItem;
        private static bool _canSwap = true;
        public bool IsEmpty => !_currentItem;
        private CellMono _oldSwapCell;

        public static Action<CellMono> OnSwapItem;

        [Inject] private GameSettingsScriptable _gameSettingsScriptable;

        private void Awake()
        {
            _transform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _spriteRenderer.sprite = _gameSettingsScriptable.CellSprite;
        }
        
        public void Init(ItemMono itemMono)
        {
            _currentItem = itemMono;
        }
        
        public void SetPosition(Vector2Int position)
        {
            _position = position;
            _transform.localPosition = position * GetSpriteSize();
        }

        private Vector2 GetSpriteSize()
        {
            return _spriteRenderer.bounds.size;
        }
        
        public void MoveItem()
        {
            if (_currentItem)
            {
                _currentItem.Move(_transform.position);
            }
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin");
            _collider.enabled = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _collider.enabled = true;
        }

        public ItemMono TakeItem()
        {
            var currentItem = _currentItem;
            _currentItem = null;
            return currentItem;
        }

        private IEnumerator SwapCell(CellMono swapCellMono)
        {
            _oldSwapCell = swapCellMono;
            (_currentItem, swapCellMono._currentItem) = (swapCellMono._currentItem, _currentItem);
            _canSwap = false;
            yield return new WaitWhile(() => _currentItem.IsSwapping);
            yield return new WaitForSeconds(0.2f);
            _canSwap = true;
            OnSwapItem?.Invoke(this);
            //OnSwapItem?.Invoke(swapCellView);
        }
        
        private IEnumerator SwapCell()
        {
            (_currentItem, _oldSwapCell._currentItem) = (_oldSwapCell._currentItem, _currentItem);
            _canSwap = false;
            yield return new WaitWhile(() => _currentItem.IsSwapping);
            yield return new WaitForSeconds(0.2f);
            _canSwap = true;
            _oldSwapCell = null;
        }

        public void ReturnCell()
        {
            StartCoroutine(SwapCell());
        }

        public void DeleteItem()
        {
            _currentItem.Death();
            _currentItem = null;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_canSwap) return;
            if (eventData.pointerDrag && eventData.pointerDrag.TryGetComponent(out CellMono dragCellView))
            {
                
                int x = Mathf.Abs(_position.x - dragCellView._position.x);
                int y = Mathf.Abs(_position.y - dragCellView._position.y);
                if ((x == 1 || y == 1) && x + y == 1)
                {
                    StartCoroutine(SwapCell(dragCellView));
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
    
}