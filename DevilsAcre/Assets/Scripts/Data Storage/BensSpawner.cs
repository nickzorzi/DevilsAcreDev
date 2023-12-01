using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BensSpawner : MonoBehaviour
{
    [Header("Add Scene Objects")]
    [SerializeField] private TextMeshProUGUI waveCountText;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject loadZone;
    [SerializeField] private Transform[] spawnPoints;
    [Space(10)]
    [Header("Wave config")]
    [SerializeField] private float waveWaitTime = 2;
    [SerializeField] private float keyWaveWaitTime = 5;

    [SerializeField] private WaveData[] waveData;

    
    [SerializeField] private List<Transform> aliveEnemies;
    private int currentWave;
    private bool wavePaused = true;

    private void Start()
    {
        // SINGLETON DEPENDENCE
        currentWave = PlayerData.Instance.lastWave;

        if(spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points");
        }
        if(waveCountText != null)
        {
            waveCountText.text = "Wave: " + (currentWave+1);
        }
    }

    private void Update()
    {
        // Wave Cool Down
        if(wavePaused && waveData[currentWave].isKeyWave)
        {
            StartCoroutine(waveCoolDown(keyWaveWaitTime));
        }
        else if (wavePaused)
        {
            StartCoroutine(waveCoolDown(waveWaitTime));
        }

        // Wave Spawning
        if (!wavePaused) 
        {
            if(loadZone != null)
            {
                loadZone.SetActive(false);
            }
            Spawner();  
        }

        // Key activate
        if (waveData[currentWave].isKeyWave && key != null)
        {
            key.SetActive(true);
        }

        // Zone Loading
        if(currentWave+1 > waveData.Length - 1 && loadZone != null)
        {
            loadZone.SetActive(true) ;
        }
        else if (wavePaused && PlayerData.Instance.hasKey && loadZone != null)
        {
            loadZone.SetActive(true);
        }
    }


    // Goes through current wave and spawns enemies over time.
    // Checks for currently alive enemies while clearing dead from list
    // Goes to next wave once spawning is done and enemies are cleared
    private void Spawner()
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
            wavePaused = true;
            // SINGLETON DEPENDENCE
            PlayerData.Instance.lastWave = currentWave;
            
            if(waveCountText != null)
            {
                waveCountText.text = "Wave: " + (currentWave+1);
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


    private IEnumerator waveCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        wavePaused = false;
    }

    [System.Serializable]
    public struct WaveData
    {
        public string name;
        public bool isKeyWave;
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
