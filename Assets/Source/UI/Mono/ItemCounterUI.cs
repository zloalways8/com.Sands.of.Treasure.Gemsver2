using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Mono
{
    public class ItemCounterUI : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text scoreText;


        public void SetSprite(Sprite sprite)
        {
            itemImage.sprite = sprite;
        }
        
        public void UpdateCounter(int value, int maxValue)
        {
            scoreText.text = $"{value}/{maxValue}";
        }
    }
}