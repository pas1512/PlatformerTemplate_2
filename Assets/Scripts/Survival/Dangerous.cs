using Unity.VisualScripting;
using UnityEngine;

public class Dangerous : MonoBehaviour
{
    public string healthName = "Cherry";
    public int damage = 1;
    [Range(0, 180)] public float safeArea = 0;
    public float force = 1;
    public AudioClip impactSound;
    public AudioClip loseSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") &&
            InDangerArea(collision.collider))
        {
            int current = PlayerPrefs.GetInt(healthName);
            current -= damage;
            PlayerPrefs.SetInt(healthName, current);

            if(current > 0)
            {
                Impact impact = collision.collider.GetComponent<Impact>();
                impact?.Apply(transform.position, force);

                if (impactSound != null)
                    AudioSource.PlayClipAtPoint(impactSound, transform.position);
            }
            else
            {
                Lose lose = collision.collider.GetComponent<Lose>();
                lose?.Apply(transform.position);

                if (loseSound != null)
                    AudioSource.PlayClipAtPoint(loseSound, transform.position);
            }
        }
    }

    private bool InDangerArea(Collider2D target)
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        return !InSafeArea(direction);
    }

    private bool InSafeArea(Vector2 direction)
    {
        if (safeArea == 0)
        {
            return false;
        }

        float saveAngle = safeArea / 2f;

        Vector2 upward = Vector2.up;
        float playerAngle = Vector2.Angle(upward, direction);

        return playerAngle < saveAngle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 origin = transform.position;
        float angle = safeArea * Mathf.Deg2Rad;
        float x = Mathf.Sin(angle / 2);
        float y = Mathf.Cos(angle / 2);
        Vector3 direction = new Vector3(x, y);
        Vector3 direction2 = new Vector3(-x, y);
        Gizmos.DrawRay(origin, direction);
        Gizmos.DrawRay(origin, direction2);
    }
}