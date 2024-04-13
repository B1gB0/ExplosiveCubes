using UnityEngine;

public class MiniCubesSpawnChecker : MonoBehaviour
{
    private int _minRangeToMiniCubes = 1;
    private int _maxRangeToMiniCubes = 101;
    private int _currentMaxChance = 100;
    private int _chanceFactor = 2;

    public bool IsSpawn()
    {
        int chance = Random.Range(_minRangeToMiniCubes, _maxRangeToMiniCubes);

        if (chance > _currentMaxChance)
        {
            return false;
        }

        _currentMaxChance /= _chanceFactor;

        return true;
    }
}
