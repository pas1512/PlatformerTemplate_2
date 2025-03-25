using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public const string SAVED_SCENE = "LoadedScene";

    [Serializable]
    public class Option
    {
        public string key;
        public int value;
    }

    public static bool taskIsComplited;

    [Header("Task")]
    public bool tryDoTask;
    public string compliteText = "Завдання виконано! Ідіть до фініша!";
    public string taskText = "Зберіть {0} кристаликів!";
    public string scoreName = "Gem";
    public int scoreObjective = 15;
    public Text scoreText;

    [Header("InitialOptions")]
    public Option[] startOptions;

    private void Start()
    {
        foreach (var option in startOptions)
            PlayerPrefs.SetInt(option.key, option.value);

        Scene current = SceneManager.GetActiveScene();
        PlayerPrefs.SetString(SAVED_SCENE, current.name);
    }

    private void Update()
    {
        if(!tryDoTask)
        {
            taskIsComplited = true;
            return;
        }

        int currentScore = PlayerPrefs.GetInt(scoreName);
        taskIsComplited = currentScore >= scoreObjective;

        if(scoreText == null)
        {
            return;
        }

        if (taskIsComplited)
        {
            scoreText.text = compliteText;
        }
        else
        {
            int rest = Mathf.Clamp(scoreObjective - currentScore, 0, scoreObjective);
            scoreText.text = string.Format(taskText, rest);
        }
    }
}