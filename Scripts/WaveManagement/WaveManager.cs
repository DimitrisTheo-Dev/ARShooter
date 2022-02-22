using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WaveManager : MonoBehaviour
{
    public List<EnemyWave> enemyWaves = new();
    public List<GameObject> spawnedEnemies = new();
    public event Action<EnemyWave> OnWaveChanged;
    private int _enemiesKilledThisWave;
    private int CurrentWave { get; set; }

    public void SpawnWave(int index)
    {
        if (index >= enemyWaves.Count) 
            return;
        
        OnWaveChanged?.Invoke(enemyWaves[index]);

        StartCoroutine(SpawnWaveRoutine(index));
    }

    private IEnumerator SpawnWaveRoutine(int index)
    {
        Debug.Log("Wave " + index + " spawning... " + enemyWaves[index].waveName);

        List<GameObject> enemiesForThisWave = enemyWaves[index].enemies;

        foreach (var enemy in enemiesForThisWave)
        {
            var currentEnemy = Instantiate(enemy, transform);
            spawnedEnemies.Add(currentEnemy);
            currentEnemy.GetComponent<Health>().OnDied += EnemyDied;
            yield return new WaitForSeconds(enemyWaves[index].enemySpawnTime);

        }
    }

    private void EnemyDied()
    {
        GetComponent<AudioSource>().Play();
        _enemiesKilledThisWave++;

        if (_enemiesKilledThisWave == enemyWaves[CurrentWave].enemies.Count)
        {
            if(CurrentWave == enemyWaves.Count - 1)
            {
                Debug.Log("<<<<<<<< GAME OVER YOU WON >>>>>>>>");
                GameManager.instance.StartGameOver();
            }
            else
            {
                _enemiesKilledThisWave = 0;
                SpawnWave(++CurrentWave);
            }
        }
    }

    public void CleanUpEnemies()
    {
        CurrentWave = 0;
        foreach (var enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
    }

}
