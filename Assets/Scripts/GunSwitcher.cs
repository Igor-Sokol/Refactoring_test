using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    [SerializeField] private Gun gun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.ChangeGun(gun);
        }
    }
}
