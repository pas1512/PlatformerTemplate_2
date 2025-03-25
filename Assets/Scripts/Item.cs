using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int value;
    public Animator animator;
    public Collider2D disabledCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator?.SetTrigger("Take");
            disabledCollider.enabled = false;

            if (PlayerPrefs.HasKey(itemName))
            {
                int score = PlayerPrefs.GetInt(itemName);
                score += value;
                PlayerPrefs.SetInt(itemName, score);
            }
            else
            {
                PlayerPrefs.SetInt(itemName, value);
            }
        }
    }
}