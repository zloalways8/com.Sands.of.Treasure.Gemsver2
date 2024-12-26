using Source.CellManagement.Mono;
using Source.Level.Mono;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.CellManagement
{
    public class CellsFactory
    {
        [Inject] private readonly CellMono m_CellMonoPrefab;
        [Inject] private readonly LevelMono m_LevelMono;
        [Inject] private readonly IObjectResolver _objectResolver;

        public CellMono Create(Vector2Int position)
        {
            CellMono cellMono = _objectResolver.Instantiate(m_CellMonoPrefab, m_LevelMono.Transform);
            cellMono.SetPosition(position);
            return cellMono;
        }
    }
}