using System.Collections;
using UnityEngine;

public class PlatformDisabler: MonoBehaviour
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
           Input.GetAxis("Vertical") < 0)
        {
            StartCoroutine(Dissactivate(collision.collider));
        }
    }

    private IEnumerator Dissactivate(Collider2D collusion)
    {
        Physics2D.IgnoreCollision(_collider, collusion);
        yield return new WaitForSeconds(0.45f);
        Physics2D.IgnoreCollision(_collider, collusion, false);
    }
}