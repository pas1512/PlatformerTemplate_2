using UnityEngine;

public class Impact : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 force;
    public float maxForce;

    public void Apply(Vector3 position, float force)
    {
        //Розраховуєм горизонтальну силу
        float h = transform.position.x - position.x;
        float hForce = Mathf.Sign(h) * force * this.force.x;

        //Розраховуєм вертикальну силу
        float v = force * this.force.y;

        //Розраховуєм кінцеву силу
        Vector2 finalForce = new Vector2(hForce, v);
        finalForce = Vector2.ClampMagnitude(finalForce, maxForce);

        rb.velocity = Vector2.zero;
        rb.AddForce(finalForce, ForceMode2D.Impulse);
    }
}