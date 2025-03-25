using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button toMenu;
    public Button firstLevel;

    [Tooltip("This can be either a continue button or a start again button")]
    public Button continueButton;

    public Button quitButton;

    private void OnEnable()
    {
        if (toMenu != null)
        {
            toMenu.onClick.AddListener(ToMenu);
        }

        if (firstLevel != null)
        {
            firstLevel.onClick.AddListener(NewGame);
        }

        if (continueButton != null)
        {
            string keyOfSaved = LevelController.SAVED_SCENE;
            continueButton.interactable = PlayerPrefs.HasKey(keyOfSaved);
            continueButton.onClick.AddListener(LoadLevel);
        }

        if(quitButton != null)
        {
            quitButton.onClick.AddListener(Quit);
        }
    }

    private void OnDisable()
    {
        if (toMenu != null)
        {
            toMenu.onClick.RemoveListener(ToMenu);
        }

        if (firstLevel != null)
        {
            firstLevel.onClick.RemoveListener(NewGame);
        }

        if (continueButton != null)
        {
            continueButton.onClick.RemoveListener(LoadLevel);
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(Quit);
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel()
    {
        string keyOfSaved = LevelController.SAVED_SCENE;
        string savedScene = PlayerPrefs.GetString(keyOfSaved);
        SceneManager.LoadScene(savedScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}