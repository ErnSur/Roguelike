using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Items/Stone")]
public class Stone : ScriptableObject
{
    [field: SerializeField]
    public Vector2Int PlayerOnPatternGrid { get; private set; }

    [SerializeField]
    private GameObject _spawnedFieldPrefab;

    [SerializeField]
    private StoneType _stoneType;

    private const int _range = 5;

    public AttackPattern pattern;

    private void OnEnable()
    {
        if (pattern.Length < _range * _range)
        {
            pattern = new AttackPattern(_range, _range);
        }
    }

    public void SpawnField(Vector3 playerPos, string direction) // direction as enum
    {
        for (int y = 0; y < pattern.Height; y++)
        {
            for (int x = 0; x < pattern.Width; x++)
            {
                if (pattern[x, y])
                {
                    Spawn(playerPos, direction, x, y);
                }
            }
        }
    }

    private void Spawn(Vector3 playerPos, string direction, int x, int y)
    {
        int nx = x;
        int ny = y;

        // ROTATION
        switch (direction)
        {
            case "up":
                nx = y;
                ny = x;
                break;
            case "down":
                nx = 2 * PlayerOnPatternGrid.y - y;
                ny = 2 * PlayerOnPatternGrid.x - x;
                break;
            case "left":
                nx = 2 * PlayerOnPatternGrid.x - x;
                ny = 2 * PlayerOnPatternGrid.y - y;
                break;
            case "right":
                nx = x;
                ny = y;
                break;
            default:
                nx = x;
                ny = y;
                break;
        }

        Vector3 spawnPos = new Vector3((nx - PlayerOnPatternGrid.x), (ny - PlayerOnPatternGrid.y), 0);
        spawnPos += playerPos;

        Instantiate(_spawnedFieldPrefab, spawnPos, Quaternion.identity);
    }

    public enum StoneType
    {
        Red,
        Green,
        Blue,
        Yellow
    };

    [Serializable]
    public class AttackPattern : Array2D<bool>
    {
        public AttackPattern(int width, int height) : base(width, height)
        {
            {
            }
        }
    }
}