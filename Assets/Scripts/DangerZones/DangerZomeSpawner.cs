using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class DangerZomeSpawner : MonoBehaviour
{
    [SerializeField]
    private DecelerationZone _decelerationZonePrefab;

    [SerializeField]
    private int _decelerationZoneCount;

    [SerializeField]
    private KillZone _killZonePrefab;

    [SerializeField]
    private int _killZoneCount;

    private List<DangertZone> _dangertZones = new List<DangertZone>();

    private Bounds _mapBounds;

    [SerializeField]
    private float _zoneOffset;

    [Inject]
    public void Construct(MapLimiter mapLimiter)
    {
        _mapBounds = mapLimiter.GetBounds();
        _mapBounds = new Bounds(_mapBounds.center, _mapBounds.size - new Vector3(_zoneOffset * 2, _zoneOffset * 2, 0));
    }

    private void Awake()
    {
        foreach (DangertZone zone in _dangertZones)
        {
            Destroy(zone.gameObject);
        }
        _dangertZones.Clear();
        try
        {
            for (int i = 0; i < _decelerationZoneCount; i++)
            {
                _dangertZones.Add(Instantiate(_decelerationZonePrefab, GetFreePosition(_decelerationZonePrefab.GetRadius), Quaternion.identity));
            }
            for (int i = 0; i < _killZoneCount; i++)
            {
                _dangertZones.Add(Instantiate(_killZonePrefab, GetFreePosition(_killZonePrefab.GetRadius), Quaternion.identity));
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Переполнение стека из-за недостаточного размера карты {ex}");
        }
    }

    private Vector2 GetFreePosition(float radius)
    {
        Vector2 randomPosition = new Vector2(Random.Range(_mapBounds.min.x + radius, _mapBounds.max.x - radius), Random.Range(_mapBounds.min.y + radius, _mapBounds.max.y - radius));
        if (_dangertZones.Count == 0 || IsValidPosition(randomPosition, radius))
        {
            return randomPosition;
        }
        return GetFreePosition(radius);
    }

    bool IsValidPosition(Vector2 position, float radius)
    {
        foreach (DangertZone zone in _dangertZones)
        {
            if (Vector2.Distance(position, zone.transform.position) < radius + zone.GetRadius + _zoneOffset)
            {
                return false;
            }
        }
        return true;
    }

}
