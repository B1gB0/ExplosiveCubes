using System;
using UnityEngine;

public class MiniCubesSpawner : MonoBehaviour
{
    private const int MinCount = 2;
    private const int MaxCount = 4;
    private const int ScaleFactor = 2;

    [SerializeField] private ExplosiveCube _prefab;
    [SerializeField] private Transform _container;

    private int _minRangeToMiniCubes = 1;
    private int _maxRangeToMiniCubes = 101;
    private int _currentMaxChance = 100;
    private int _chanceFactor = 2;

    public void Spawn(Transform transform)
    {
        int chance = UnityEngine.Random.Range(_minRangeToMiniCubes, _maxRangeToMiniCubes);

        if (chance > _currentMaxChance)
        {
            return;
        }

        _currentMaxChance /= _chanceFactor;

        int count = UnityEngine.Random.Range(MinCount, MaxCount);

        for (int i = 0; i < count; i++)
        {
            ExplosiveCube newCube = Instantiate(_prefab, _container);
            newCube.transform.position = transform.position;
            newCube.transform.localScale = transform.localScale;
            newCube.transform.localScale /= ScaleFactor;
        }
    }
}
