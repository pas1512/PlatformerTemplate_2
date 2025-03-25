using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public const string SAVED_SCENE = "LoadedScene";

    [Serializable]
    public class Option
    {
        public string key;
        public int value;
    }

    public Option[] startOptions;

    private void Start()
    {
        foreach (var option in startOptions)
            PlayerPrefs.SetInt(option.key, option.value);

        Scene current = SceneManager.GetActiveScene();
        PlayerPrefs.SetString(SAVED_SCENE, current.name);
    }
}