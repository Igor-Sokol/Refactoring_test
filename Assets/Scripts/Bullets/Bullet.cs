using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public abstract class Bullet : MonoBehaviour
{
    private bool _isCollided;
    protected Rigidbody Rigidbody;
    
    [SerializeField] protected Data data;

    public virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Fire(float force)
    {
        var effectInstance = Instantiate(data.StartVFX, transform.position, Quaternion.identity);
        Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);

        var timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = 3.0f;
        timer.OnTime = () =>
        {
            Destroy(gameObject);
        };
        
        Rigidbody.AddForce(transform.forward * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided) return;
        _isCollided = true;
        OnFaced(collision);

        var timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = data.ReactionTime;
        timer.OnTime = () =>
        {
            Reaction();
            var effectInstance = Instantiate(data.ExplosionVFX, collision.GetContact(0).point, Quaternion.identity);
            effectInstance.transform.rotation *= Quaternion.FromToRotation(effectInstance.transform.up, collision.GetContact(0).normal);
        };

        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = data.LiveTime;
        timer.OnTime = () =>
        {
            Destroy(gameObject);
        };
    }
    
    protected abstract void OnFaced(Collision collision);
    protected abstract void Reaction();
}

[Serializable]
public struct Data
{
    public GameObject StartVFX;
    public GameObject ExplosionVFX;
    public float LiveTime;
    public float ReactionTime;
}