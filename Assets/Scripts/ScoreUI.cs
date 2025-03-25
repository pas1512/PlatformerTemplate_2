using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public string itemName;
    public Text text;

    private void Update()
    {
        int value = PlayerPrefs.GetInt(itemName);
        text.text = value.ToString();
    }
}