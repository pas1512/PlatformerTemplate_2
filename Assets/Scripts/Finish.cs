using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public string nextLevel;
    public string victoryScene = "Victory";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && LevelController.taskIsComplited)
        {
            PlayerPrefs.SetString(LevelController.SAVED_SCENE, nextLevel);
            SceneManager.LoadScene(victoryScene);
        }
    }
}