using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.CellManagement;
using Source.CellManagement.Mono;
using Source.Game;
using Source.Level.Mono;
using UnityEngine;
using VContainer;

namespace Source.Level
{
    public class Level
    {
        [Inject] private readonly CellsFactory m_CellsFactory;
        [Inject] private readonly ItemFactory _itemFactory;
        [Inject] private readonly GameSettingsScriptable _gameSettingsScriptable;
        [Inject] private readonly LevelMono m_LevelMono;
        private readonly GameStats _gameStats;

        private Vector2Int _levelSize = new Vector2Int(5, 7);
        private List<CellMono> _cellViews = new List<CellMono>();

        private List<CellMono> _mergedCellViews = new List<CellMono>();

        [Inject]
        private Level(GameStats gameStats)
        {
            _gameStats = gameStats;
            CellMono.OnSwapItem += CheckMergedCell;
            _gameStats.OnStartLevel += Generate;
            _gameStats.OnStopGame += RemoveLevel;
        }

        private void Generate()
        {
            if (_cellViews.Count > 0)
            {
                foreach (var cellView in _cellViews)
                {
                    cellView.DeleteItem();
                }
            }
            else
            {
                m_LevelMono.Transform.position = new Vector2(
                                                    -_gameSettingsScriptable.LevelSize.x / 2f * _gameSettingsScriptable.CellSize.x + _gameSettingsScriptable.CellSize.x/2f,
                                                    -_gameSettingsScriptable.LevelSize.y / 2f * _gameSettingsScriptable.CellSize.y + _gameSettingsScriptable.CellSize.y/2f) + 
                                                _gameSettingsScriptable.LevelPosition;
                for (int i = 0; i < _gameSettingsScriptable.LevelSize.x; i++)
                {
                    for (int j = _gameSettingsScriptable.LevelSize.y; j > 0 ; j--)
                    {
                        if ((Mathf.Abs(i) >= _gameSettingsScriptable.LevelSize.x - _gameSettingsScriptable.BorderCut || 
                            Mathf.Abs(i) <= _gameSettingsScriptable.BorderCut - 1) && 
                            (Mathf.Abs(j) >= _gameSettingsScriptable.LevelSize.y - _gameSettingsScriptable.BorderCut + 1 ||
                            Mathf.Abs(j) <= _gameSettingsScriptable.BorderCut)) continue;
                        Vector2Int position = new Vector2Int(i, j);
                        CellMono cellMono = m_CellsFactory.Create(position);
                        ItemMono itemMono = _itemFactory.Get();
                        cellMono.Init(itemMono);
                    
                        _cellViews.Add(cellMono);
                    }
                }
            }
        }

        private void RemoveLevel()
        {
            for (int i = 0; i < _cellViews.Count; i++)
            {
                _cellViews[i].DeleteItem();
                GameObject.Destroy(_cellViews[i].gameObject);
            }
            _cellViews.Clear();
        }

        public void UpdateCells()
        {
            MoveItem();
            foreach (var cellView in _cellViews)
            {
                cellView.MoveItem();
            }
        }
        
        private void CheckMergedCell(CellMono cellMono)
        {
            _mergedCellViews.Clear();
            Vector2Int vector2Int = cellMono.Position;

            for (int i = vector2Int.x; i < _gameSettingsScriptable.LevelSize.x+1; i++)
            {
                Vector2Int checkPosition = new Vector2Int(i, vector2Int.y);
                var findCell = _cellViews.FirstOrDefault(_ => checkPosition == _.Position);
                if (findCell && !findCell.IsEmpty && !cellMono.IsEmpty && findCell.CurrentItem.index == cellMono.CurrentItem.index)
                {
                    if (!_mergedCellViews.Contains(findCell))
                    {
                        _mergedCellViews.Add(findCell);
                    }
                }
                else
                {
                    break;
                }
            }
            for (int i = vector2Int.x; i >= 0; i--)
            {
                Vector2Int checkPosition = new Vector2Int(i, vector2Int.y);
                var findCell = _cellViews.FirstOrDefault(_ => checkPosition == _.Position);
                if (findCell && !findCell.IsEmpty && !cellMono.IsEmpty && findCell.CurrentItem.index == cellMono.CurrentItem.index)
                {
                    if (!_mergedCellViews.Contains(findCell))
                    {
                        _mergedCellViews.Add(findCell);
                    }
                }
                else
                {
                    break;
                }
            }
            for (int i = vector2Int.y; i < _gameSettingsScriptable.LevelSize.y+1; i++)
            {
                Vector2Int checkPosition = new Vector2Int(vector2Int.x, i);
                var findCell = _cellViews.FirstOrDefault(_ => checkPosition == _.Position);
                if (findCell && !findCell.IsEmpty && !cellMono.IsEmpty && findCell.CurrentItem.index == cellMono.CurrentItem.index)
                {
                    if (!_mergedCellViews.Contains(findCell))
                    {
                        _mergedCellViews.Add(findCell);
                    }
                }
                else
                {
                    break;
                }
            }
            for (int i = vector2Int.y; i >= 0; i--)
            {
                Vector2Int checkPosition = new Vector2Int(vector2Int.x, i);
                var findCell = _cellViews.FirstOrDefault(_ => checkPosition == _.Position);
                if (findCell && !findCell.IsEmpty && !cellMono.IsEmpty && findCell.CurrentItem.index == cellMono.CurrentItem.index)
                {
                    if (!_mergedCellViews.Contains(findCell))
                    {
                        _mergedCellViews.Add(findCell);
                    }
                }
                else
                {
                    break;
                }
            }

            if (_mergedCellViews.Count > 2)
            {
                if (_gameSettingsScriptable.ByItemScore)
                {
                    _gameStats.AddScore(_mergedCellViews.Count, _mergedCellViews[0].CurrentItem.index);
                }
                else
                {
                    _gameStats.AddScore(_mergedCellViews.Count);
                }
                m_LevelMono.PlayMerge();
                foreach (var mergedCellView in _mergedCellViews)
                {
                    mergedCellView.DeleteItem();
                }
            }
            else
            {
                cellMono.ReturnCell();
            }

        }
        
        private void CheckMergedCells()
        {
            foreach (var cellView in _cellViews)
            {
                CheckMergedCell(cellView);
            }
        }

        private void MoveItem()
        {
            foreach (var cellView in _cellViews)
            {
                Vector2Int movedPosition = cellView.Position;
                movedPosition.y--;
                CellMono nextCellMono = _cellViews.FirstOrDefault(_ => movedPosition == _.Position);
                if (nextCellMono && nextCellMono.IsEmpty)
                {
                    nextCellMono.Init(cellView.TakeItem());
                    SpawnNewItem();
                }
            }
        }

        private void SpawnNewItem()
        {
            foreach (var cellView in _cellViews)
            {
                if ((cellView.Position.y >= _gameSettingsScriptable.LevelSize.y - _gameSettingsScriptable.BorderCut - 1 && 
                     (cellView.Position.x >= _gameSettingsScriptable.LevelSize.x - _gameSettingsScriptable.BorderCut ||
                      cellView.Position.x <= _gameSettingsScriptable.BorderCut) ||
                     cellView.Position.y == _gameSettingsScriptable.LevelSize.y) && cellView.IsEmpty)
                {
                    ItemMono itemMono = _itemFactory.Get();
                    itemMono.SetPosition(cellView.transform.position+Vector3.up);
                    cellView.Init(itemMono);
                }
            }
        }
    }
}