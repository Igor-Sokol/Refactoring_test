using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField] private Explosion _explosion;
    
    protected override void OnFaced(Collision collision) { }

    protected override void Reaction()
    {
        _explosion.Explode();
    }
}