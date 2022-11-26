using UnityEngine;

public class Randomizer : MonoBehaviour
{
    [SerializeField] Vector3 rotMin;
    [SerializeField] Vector3 rotMax;
    [SerializeField] Vector3 posMin;
    [SerializeField] Vector3 posMax;

    private void Awake()
    {
        Randomize();
    }

    private void Randomize() 
    {
        transform.rotation = Quaternion.Euler(Random.Range(rotMin.x, rotMax.x), Random.Range(rotMin.y, rotMax.y), Random.Range(rotMin.z, rotMax.z));
        transform.position += new Vector3(Random.Range(posMin.x, posMax.x), Random.Range(posMin.y, posMax.y), Random.Range(posMin.z, posMax.z));
    }
}
