using System;
using Core.Locator;
using Core.Services.GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class UIController : MonoBehaviour
    {
        private IGamePlayService _gamePlayService;
        [SerializeField] private TextMeshProUGUI levelTxt;
        [SerializeField] private Button settingsBtn;
        
        
        [SerializeField] private LoseScreenController _loseScreenController;
        [SerializeField] private WinScreenController _winScreenController;
        
        
        [SerializeField] private Button nextLevelBtn;
        [SerializeField] private Button previousLevelBtn;
        private void Awake()
        {
            _gamePlayService = ServiceLocator.Instance.Resolve<IGamePlayService>();
            _gamePlayService.LevelFinishedEvent += OnLevelFinished;
            SetLevelText();
            SetButtonBehaviours();
        }

        private void OnLevelFinished(object sender, LevelFinishedType e)
        {
            switch (e)
            {
                case LevelFinishedType.Fail:
                    OpenFailScreen();
                    break;
                case LevelFinishedType.Complete:
                    OpenWinScreen();
                    break;
                case LevelFinishedType.Restart:
                    // NO ACTION FOR NOW 
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e), e, null);
            }
        }

        private void OpenFailScreen()
        {
            CloseSettingScreen();
            _loseScreenController.Activate();
        }

        private void OpenWinScreen()
        {
            CloseSettingScreen();
            _winScreenController.Activate();
        }

        private void OpenSettingsScreen()
        {
            Debug.Log("Setting Screen Open");
        }

        private void CloseSettingScreen()
        {
            Debug.Log("Close Setting Screen");
        }
        
        private void SetLevelText()
        {
            levelTxt.text ="LV " +_gamePlayService.GetCurrentLevel();
        }

        private void SetButtonBehaviours()
        {
            nextLevelBtn.onClick.AddListener(() =>
            {
                _gamePlayService.LoadNext();
            });
            
            previousLevelBtn.onClick.AddListener(() =>
            {
                _gamePlayService.LoadPrevious();
            });
            // settingsBtn.onClick.AddListener(() =>
            // {
            //     OpenSettingsScreen();
            // });
        }

        private void OnDestroy()
        {
            _gamePlayService.LevelFinishedEvent -= OnLevelFinished;
        }
    }
}