using System;
using UnityEngine;

namespace Source.UI.Mono
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private PoliceConfirmUI _policeConfirmUI;
        [SerializeField] private MainMenuUI _mainMenu;
        [SerializeField] private UIView[] _UIViews;

        private void Start()
        {
            foreach (var variUIView in _UIViews)
            {
                variUIView.Hide();
            }
            var policy = PlayerPrefs.GetInt("Policy", 0);
            if (policy == 0)
            {
                _policeConfirmUI.Show();
            }
            else
            {
                _mainMenu.Show();
            }
        }
    }
}