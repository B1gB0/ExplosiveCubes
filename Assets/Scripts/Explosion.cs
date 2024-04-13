using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    [SerializeField] private ParticleSystem _effect;

    private MiniCubesSpawnChecker _spawnChecker;
    private AudioSource _source;

    public event Action<Transform> MiniCubesSpawned;

    private void OnMouseUpAsButton()
    {
        Explode();
    }

    public void SetParameters(MiniCubesSpawnChecker spawnChecker, AudioSource source)
    {
        _spawnChecker = spawnChecker;
        _source = source;
    }

    private void Explode()
    {
        _source.Play();

        if (_spawnChecker.GetChanceToSpawn())
            MiniCubesSpawned?.Invoke(transform);

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
