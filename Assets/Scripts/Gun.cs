using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{ 
    [SerializeField] private Bullet bullet;
    [FormerlySerializedAs("Angle")] [SerializeField] private float angle = 0.0f;
    [FormerlySerializedAs("Force")] [SerializeField] private float force = 150.0f;

    public void Shoot(Transform pivot)
    {
        var bulletInstance = Instantiate(bullet, pivot.position, pivot.rotation);
        bulletInstance.transform.Rotate(Vector3.left, angle);
        bulletInstance.Fire(force);
    }
}
