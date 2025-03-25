using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 4;
    public LayerMask groundMask;
    private bool groundedToCail;

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        transform.Translate(move * Time.fixedDeltaTime * speed, 0, 0);

        if (move != 0)
        {
            GetComponent<SpriteRenderer>().flipX = move < 0;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        bool grounded = CheckGround() && groundedToCail;

        if (Input.GetAxis("Jump") > 0 && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            groundedToCail = false;
        }

        Animator animator = GetComponent<Animator>();

        if(animator == null)
        {
            return;
        }

        animator.SetBool("Move", move != 0);
        animator.SetBool("Jump", !grounded);
        animator.SetFloat("Velocity", rb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        groundedToCail = collision.contacts[0].normal.y > 0.7f && rb.velocity.y <= speed * Time.deltaTime;
    }

    private bool CheckGround()
    {
        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        float radius = collider.size.x * transform.localScale.x * 0.5f - 0.05f;
        float hegiht = (collider.size.y * transform.localScale.y * 0.5f) + 0.05f - radius;
        Vector3 offset = collider.offset * transform.localScale;
        Vector3 point = transform.position + Vector3.down * hegiht + offset;
        return Physics2D.OverlapCircle(point, radius, groundMask);
    }

    private void OnDrawGizmos()
    {
        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        float radius = collider.size.x * transform.localScale.x * 0.5f - 0.05f;
        float hegiht = (collider.size.y * transform.localScale.y * 0.5f) + 0.05f - radius; Vector3 offset = collider.offset * transform.localScale;
        Vector3 point = transform.position + Vector3.down * hegiht + offset;
        Gizmos.color = new Color(0.5f, 0.5f, 0.1f);
        Gizmos.DrawWireSphere(point, radius);
    }
}
