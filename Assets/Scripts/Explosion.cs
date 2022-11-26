using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float range = 2.5f;
    public float force = 150f;

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("box"));
        foreach (var collider in colliders)
        {
            collider.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, range);
        }
    }
}