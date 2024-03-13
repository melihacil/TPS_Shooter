using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public static SpawnController instance;

    [SerializeField] private Transform[] _spawnLocations;
    [SerializeField] private Transform _player;
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private float _randomRadius = 1.0f;
    private Wave _currentWave;

    private GameObject[] _enemyTypes;
    private int _enemyCount;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    private void InitializeObjects()
    {
        if (_waves.Count.Equals(0))
        {
            Debug.Log("Finished");
            return;
        }

        _currentWave = _waves[0];
        _waves.RemoveAt(0);
        _enemyTypes = _currentWave.enemyTypes;
        _enemyCount = _currentWave.enemyCount;
        StartWave();
    }
    private void StartWave()
    {
        GameObject currentEnemy;
        Vector3 randomPos;
        for (int i = 0; i < _enemyCount; i++)
        {
            //randomPos = _player.position * Random.insideUnitCircle * _randomRadius;

            randomPos = _spawnLocations[Random.Range(0, _spawnLocations.Length)].position + Random.insideUnitSphere;

            currentEnemy = Instantiate(_enemyTypes[Random.Range(0, _enemyTypes.Length)], randomPos, Quaternion.identity, transform);
            currentEnemy.GetComponent<EnemyAI>().SetPlayer(_player);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_player.position, _randomRadius);
    }

    public void DecreaseEnemyCount()
    {
        
        if (_enemyCount-- <= 0)
        {
            InitializeObjects();
        }

    }

}
