using System.Collections.Generic;
using Source.CellManagement.Mono;
using Source.Level.Mono;
using UnityEngine.Rendering;
using VContainer;
using VContainer.Unity;

namespace Source.CellManagement
{
    public class ItemFactory
    {
        [Inject] private readonly ItemMono m_ItemMonoPrefab;
        [Inject] private readonly IObjectResolver _objectResolver;
        [Inject] private readonly LevelMono m_LevelMono;

        private List<ItemMono> _poolItemList = new List<ItemMono>();

        private ItemMono Create()
        {
            ItemMono itemMono = _objectResolver.Instantiate(m_ItemMonoPrefab, m_LevelMono.Transform);
            return itemMono;
        }

        public ItemMono Get()
        {
            ItemMono findItem = null;
            if (_poolItemList.Count > 0)
            {
                foreach (var poolItem in _poolItemList)
                {
                    if (!poolItem.gameObject.activeSelf)
                    {
                        findItem = poolItem;
                    }
                }
            }

            if (!findItem)
            {
                findItem = Create();
                findItem.OnDeath += Return;
                _poolItemList.Add(findItem);
            }
            
            findItem.gameObject.SetActive(true);
            findItem.SetRandomImage();
            return findItem;
        }

        private void Return(ItemMono itemMono)
        {
            itemMono.gameObject.SetActive(false);
        }
    }
}