using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Data
{ 
    public string Key;
    public GameObject StartVFX;
    public GameObject ExplosionVFX;
    public float LiveTime;
    public float ReactionTime;
}

public class Bullet : MonoBehaviour
{
    public List<Data> Data;

    void Start() 
    {
        var data = Data.Single(s => gameObject.name.Contains(s.Key));
        var effectInstance = Instantiate(data.StartVFX, transform.position, Quaternion.identity);
        Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);

        var timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = 3.0f;
        timer.OnTime = () =>
        {
            Destroy(gameObject);
        };
    }

    void Update() { }

    private void OnCollisionEnter(Collision collision)
    {
        var data = Data.Single(s => gameObject.name.Contains(s.Key));

        var timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = data.ReactionTime;
        timer.OnTime = () => 
        {
            var data = Data.Single(s => gameObject.name.Contains(s.Key));
            var effectInstance = Instantiate(data.ExplosionVFX, collision.GetContact(0).point, Quaternion.identity);
            effectInstance.transform.rotation *= Quaternion.FromToRotation(effectInstance.transform.up, collision.GetContact(0).normal);
            Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);
        };

        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = data.LiveTime;
        timer.OnTime = () =>
        {
            Destroy(gameObject);
        };
    }
}
