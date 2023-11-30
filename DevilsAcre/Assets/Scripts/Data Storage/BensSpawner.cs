using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BensSpawner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveCountText;
    [SerializeField] private GameObject key;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private WaveData[] waveData;

    
    private List<Transform> aliveEnemies;
    private int currentWave;

    private void Start()
    {
        // SINGLETON DEPENDENCE
        currentWave = PlayerData.Instance.lastWave;

        if(spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points");
        }
    }

    private void Update()
    {
        Spawner();
    }


    // Goes through current wave and spawns enemies over time.
    // Checks for currently alive enemies while clearing dead from list
    // Goes to next wave once spawning is done and enemies are cleared
    private void Spawner()
    {
        if (!waveData[currentWave].isPauseWave) // don't spawn if Pause Wave bool is active
        {

            bool allCleared = true;
            for (int i = 0; i < waveData[currentWave].Enemies.Length; i++)
            {
                var enemy = waveData[currentWave].Enemies[i];


                if (!enemy.spawnCoolDown && enemy.spawnCount > 0)
                {
                    allCleared = false;
                    StartCoroutine(enemySpawning(i));
                }
                else if (enemy.spawnCount > 0)
                {
                    allCleared = false;
                }

            }
            if (allCleared && aliveEnemies.Count == 0 && currentWave < waveData.Length - 1)
            {
                Debug.Log("Next Wave");
                currentWave++;
            }

        }
        for (int i = 0; i < aliveEnemies.Count; i++)
        {
            if (aliveEnemies[i] == null)
            {
                aliveEnemies.Remove(aliveEnemies[i]);
                break;
            }
        }
    }


    // Spawns enemy than waits or should that be reversed? idk
    private IEnumerator enemySpawning(int selectedEnemy)
    {

        waveData[currentWave].Enemies[selectedEnemy].spawnCoolDown = true;
        waveData[currentWave].Enemies[selectedEnemy].spawnCount--;
        

        //Debug.Log("Spawn " + waveData[currentWave].Enemies[selectedEnemy].EnemyPrefab.name + " " + waveData[currentWave].Enemies[selectedEnemy].spawnCount);

        GameObject newSpawn = Instantiate(waveData[currentWave].Enemies[selectedEnemy].EnemyPrefab);
        newSpawn.transform.position = spawnPoints[(int)UnityEngine.Random.Range(0, spawnPoints.Length)].position;
        aliveEnemies.Add(newSpawn.transform); // adds to list

        yield return new WaitForSeconds(waveData[currentWave].Enemies[selectedEnemy].spawnSpeed);

        waveData[currentWave].Enemies[selectedEnemy].spawnCoolDown = false;
    }




    [System.Serializable]
    public struct WaveData
    {
        public string name;
        public bool isPauseWave;
        public EnemyData[] Enemies;

    }

    [System.Serializable]
    public struct EnemyData 
    {
        public GameObject EnemyPrefab;
        public int spawnCount;
        public float spawnSpeed;
        [HideInInspector]
        public bool spawnCoolDown;
    }

}
