using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class Cube : MonoBehaviour
{
    private Explosion _explosion;

    public Explosion Explosion => _explosion;

    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
        gameObject.GetComponent<Renderer>().material.color =
        new Color(Random.value, Random.value, Random.value, 1f);
    }
}
