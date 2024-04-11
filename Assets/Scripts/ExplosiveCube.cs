using System;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    [SerializeField] private ParticleSystem _effect;

    private AudioSource _source;
    private MiniCubesSpawner _spawner;

    public event Action<Transform> Exploded;

    private void Awake()
    {
        _spawner = GetComponentInParent<MiniCubesSpawner>();
        _source = GetComponentInParent<AudioSource>();

        gameObject.GetComponent<Renderer>().material.color =
        new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1f);
    }

    private void OnEnable()
    {
        Exploded += _spawner.Spawn; 
    }

    private void OnMouseUpAsButton()
    {
        Explode();
    }

    private void OnDisable()
    {
        Exploded -= _spawner.Spawn;
    }

    private void Explode()
    {
        Exploded?.Invoke(transform);

        _source.Play();

        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);

        Instantiate(_effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}
