using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MiniCubesSpawnChecker))]
[RequireComponent(typeof(AudioSource))]
public class MiniCubesSpawner : MonoBehaviour
{
    private const int MinCount = 2;
    private const int MaxCount = 4;
    private const int ScaleFactor = 2;

    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private List<Explosion> _explosions;

    private MiniCubesSpawnChecker _spawnChecker;
    private AudioSource _source;

    private void Awake()
    {
        _spawnChecker = GetComponent<MiniCubesSpawnChecker>();
        _source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        foreach (Explosion explosion in _explosions)
        {
            explosion.SetParameters(_spawnChecker, _source);
            explosion.MiniCubesSpawned += Spawn;
        }
    }

    private void OnDisable()
    {
        foreach (Explosion explosion in _explosions)
        {
            explosion.MiniCubesSpawned -= Spawn;
        }
    }

    private void Spawn(Transform transform)
    {
        int count = Random.Range(MinCount, MaxCount);

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_prefab, _container);
            newCube.transform.position = transform.position;
            newCube.transform.localScale = transform.localScale;
            newCube.transform.localScale /= ScaleFactor;
            newCube.Explosion.SetParameters(_spawnChecker, _source);
            newCube.Explosion.MiniCubesSpawned += Spawn;
            _explosions.Add(newCube.Explosion);
        }
    }
}
