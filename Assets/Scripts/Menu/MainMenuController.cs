using System.Collections.Generic;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Panels/Containers")]
        [SerializeField] private GameObject mainMenuContainer;
        [SerializeField] private GameObject instructionsPanel;
        [SerializeField] private GameObject highScoresPanel;
        [SerializeField] private GameObject quitPanel;
        
        [Header("Buttons")]
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Button buttonInstructions;
        [SerializeField] private Button buttonHighScores;
        [SerializeField] private Button buttonQuit;
        [SerializeField] private Button buttonConfirmQuit;
        [SerializeField] private Button buttonCancelQuit;
        [SerializeField] private Button buttonQuitInstructions;
        [SerializeField] private Button buttonQuitHighScores;
        
        [Header("Text Mesh Pro")] 
        [SerializeField] private List<TextMeshProUGUI> _highScores;

        [SerializeField] private HighScoreData _highScoreData;
        
        private void Awake()
        {
            buttonPlay.onClick.AddListener(PlayGame);
            buttonInstructions.onClick.AddListener(DisplayInstructionsPanel);
            buttonHighScores.onClick.AddListener(DisplayHighScorePanel);
            buttonQuit.onClick.AddListener(DisplayQuitPanel);
            buttonConfirmQuit.onClick.AddListener(QuitGame);
            buttonCancelQuit.onClick.AddListener(CloseQuitPanel);
            buttonQuitInstructions.onClick.AddListener(CloseInstructionsPanel);
            buttonQuitHighScores.onClick.AddListener(CloseHighScorePanel);
      
            instructionsPanel.SetActive(false);
            quitPanel.SetActive(false);
            highScoresPanel.SetActive(false);
        }
        private void PlayGame()
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }

        private void DisplayHighScorePanel()
        {
            SetHighScoresText();
            mainMenuContainer.SetActive(false);
            highScoresPanel.SetActive(true);
        }
        
        private void SetHighScoresText()
        {
            int i = 0;
            
            foreach (int score in _highScoreData.HighScores)
            {
                _highScores[i].text = $"{i + 1}:  {score.ToString()}";
                i++;
            }
        }
        
        private void CloseHighScorePanel()
        {
            highScoresPanel.SetActive(false);
            mainMenuContainer.SetActive(true);
        }

        private void DisplayInstructionsPanel()
        {
            mainMenuContainer.SetActive(false);
            instructionsPanel.SetActive(true);
        }
        
        private void CloseInstructionsPanel()
        {
            instructionsPanel.SetActive(false);
            mainMenuContainer.SetActive(true);
        }
        
        private void DisplayQuitPanel()
        {
            mainMenuContainer.SetActive(false);
            quitPanel.SetActive(true);
        }
        
        private void CloseQuitPanel()
        {
            quitPanel.SetActive(false);
            mainMenuContainer.SetActive(true);
        }

        private void QuitGame()
        {
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #else
                    Application.Quit();
            #endif
        }
        
        private void OnDestroy()
        {
            buttonPlay.onClick.RemoveListener(PlayGame);
            buttonInstructions.onClick.RemoveListener(DisplayInstructionsPanel);
            buttonHighScores.onClick.RemoveListener(DisplayHighScorePanel);
            buttonQuit.onClick.RemoveListener(DisplayQuitPanel);
            buttonConfirmQuit.onClick.RemoveListener(QuitGame);
            buttonCancelQuit.onClick.RemoveListener(CloseQuitPanel);
            buttonQuitInstructions.onClick.RemoveListener(CloseInstructionsPanel);
            buttonQuitHighScores.onClick.RemoveListener(CloseHighScorePanel);
        }
    }
}
