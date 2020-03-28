using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float force;
    public GameObject particles;

    public void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5);
        Instantiate(particles, transform);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 vec = collider.transform.position - transform.position;
                float mag = vec.magnitude;
                vec.Normalize();
                collider.GetComponent<Rigidbody2D>().
                    AddForce(vec * force * Mathf.Clamp01(1 / mag), ForceMode2D.Impulse);
            }
        }
    }
}
