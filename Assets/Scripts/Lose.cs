using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public Vector2 force;

    [Header("Defeat Scene")]
    public string sceneName = "Defeat";
    public float loadDelay = 3;

    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;

    [Header("Disabled")]
    public Impact imp;
    public Collider2D col2d;
    public PlayerControll mov;
    public WindowedCamera cam;

    public void Apply(Vector3 position)
    {
        float h = transform.position.x - position.x;
        float hForce = Mathf.Sign(h) * force.x;
        Vector2 resultForce = new Vector2(hForce, force.y);

        Destroy(imp);
        rb.velocity = Vector2.zero;
        rb.AddForce(resultForce, ForceMode2D.Impulse);

        col2d.enabled = false;
        mov.enabled = false;
        cam.enabled = false;
        anim.SetBool("Hurt", true);
        sr.sortingOrder = 100;

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }
}