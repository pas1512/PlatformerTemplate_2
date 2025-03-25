using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button pause;
    public Button resume;
    public Button restart;
    public Button toMenu;
    public RectTransform panel;

    private void Start()
    {
        Resume();
    }

    private void OnEnable()
    {
        pause.onClick.AddListener(Stop);
        resume.onClick.AddListener(Resume);
        restart.onClick.AddListener(Restart);
        toMenu.onClick.AddListener(ToMenu);
    }

    private void OnDisable()
    {
        pause.onClick.RemoveListener(Stop);
        resume.onClick.RemoveListener(Resume);
        restart.onClick.RemoveListener(Restart);
        toMenu.onClick.RemoveListener(ToMenu);
    }

    public void Stop()
    {
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panel.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}