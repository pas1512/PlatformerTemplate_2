using UnityEngine;

public class Hitable : MonoBehaviour
{
    [Header("Collision")]
    [Range(0, 180)] public float area = 0;
    public float force = 5;

    [Header("Score")]
    public int score = 1;
    public string scoreName = "Enemy";

    [Header("Components")]
    public Animator animator;
    public Collider2D trigger;
    public MonoBehaviour movement;
    public MonoBehaviour attack;

    [Header("Other")]
    public AudioClip sound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 direction = collision.transform.position - transform.position;

            if (CheckArea(direction))
            {
                animator.SetTrigger("Hurt");
                trigger.enabled = false;
                movement.enabled = false;
                attack.enabled = false;
                var impact = collision.gameObject.GetComponent<Impact>();
                impact.Apply(transform.position, force);

                int value = PlayerPrefs.GetInt(scoreName, 0);
                value += score;
                PlayerPrefs.SetInt(scoreName, value);

                if(sound != null)
                    AudioSource.PlayClipAtPoint(sound, transform.position);
            }
        }
    }

    private bool CheckArea(Vector2 direction)
    {
        if (area == 0)
        {
            return false;
        }

        float saveAngle = area / 2f;

        Vector2 upward = Vector2.up;
        float playerAngle = Vector2.Angle(upward, direction);

        return playerAngle < saveAngle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 origin = transform.position;
        float angle = area * Mathf.Deg2Rad;
        float x = Mathf.Sin(angle / 2);
        float y = Mathf.Cos(angle / 2);
        Vector3 direction = new Vector3(x, y);
        Vector3 direction2 = new Vector3(-x, y);
        Gizmos.DrawRay(origin, direction);
        Gizmos.DrawRay(origin, direction2);
    }
}