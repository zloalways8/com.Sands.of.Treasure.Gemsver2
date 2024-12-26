using UnityEngine;
using VContainer;

namespace Source.UI.Mono
{
    public class UIView : MonoBehaviour
    {
        private UIView _prevUIView;

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Show(UIView prevUIView)
        {
            _prevUIView = prevUIView;
            _prevUIView.Hide();
            Show();
        }

        public void Return()
        {
            if (_prevUIView)
            {
                _prevUIView.Show();
                Hide();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}