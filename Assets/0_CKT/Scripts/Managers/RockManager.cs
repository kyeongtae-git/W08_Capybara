using System;
using UnityEngine;

public class RockManager
{
    public float MaxHealth => _maxHealth;
    float _maxHealth;

    public float MoveSpeed => _moveSpeed;
    float _moveSpeed;

    public Vector3 SpawnPoint => _spawnPoint;
    Vector3 _spawnPoint = new Vector3(11.25f, -0.5f, 0);

    public Vector3 StopPoint => _stopPoint;
    Vector3 _stopPoint = new Vector3(2.25f, -0.5f, 0);

    public float MoveTime => _moveTime;
    float _moveTime = 1.5f;

    public Action<float> OnGetDamageEvent;

    public void Init()
    {
        _maxHealth = 100f;
        _moveSpeed = (_spawnPoint.x - _stopPoint.x) / _moveTime;
    }
}
