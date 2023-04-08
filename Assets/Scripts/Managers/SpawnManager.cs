using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject easyEnemyPrefab;
    public GameObject mediumEnemyPrefab;
    public GameObject hardEnemyPrefab; 
    public GameObject rapidFirePrefab;
    public GameObject strengthPrefab;
    public GameObject bombPickupPrefab;
    public GameObject player;
    public PlayerHealthManager playerHealthManager;
    public GunController gunController;

    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI enemyCountText;

    private float spawnRange = 25.0f;
    private float boundaryRange = 10;
    public int enemyCount;
    public int waveNumber = 1;

    private int rapidFireCount;
    private int rapidFireCountLimit = 8;
    private float rapidFireSpawnTimer;
    public static bool rapidFireUnlocked;

    public int strengthCount;
    private int strengthCountLimit = 8;
    private float strengthSpawnTimer;

    private int bombCount;
    private int bombCountLimit = 6;
    private float bombSpawnTimer;
    public static bool bombUnlocked;


    // Start is called before the first frame update
    void Start()
    {
        SpawnEasyEnemyWave(waveNumber);
        rapidFireSpawnTimer = 4;
    }


    // Update is called once per frame
    void Update()
    {
        // Spawn Enemies
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        enemyCountText.text = "Enemies Left: " + enemyCount;
        if(enemyCount == 0) {
            waveNumber++;
            waveNumberText.text = "Wave: " + waveNumber;
            SpawnEasyEnemyWave(waveNumber);
            if(waveNumber % 10 == 0)
            {
                SpawnHardEnemyWave(waveNumber);
            }
            else if(waveNumber % 5 == 0){
                SpawnMediumEnemyWave(waveNumber);
            }
            if((waveNumber - 1) % 5 == 0)
            {
                healPlayer();
                increaseFireRate();
            }
        }

        //Spawn RapidFire Buff
        if(rapidFireUnlocked){
            rapidFireCount = FindObjectsOfType<RapidFire>().Length;
            if(rapidFireCount < rapidFireCountLimit)
            {
                rapidFireSpawnTimer -= Time.deltaTime;
                if(rapidFireSpawnTimer <= 0)
                {
                    rapidFireSpawnTimer = 4;
                    Instantiate(rapidFirePrefab, GenerateSpawnPosition(), rapidFirePrefab.transform.rotation);
                }
            }
        }

        //Spawn Strength Buff
        strengthCount = FindObjectsOfType<Strength>().Length;
        if(strengthCount < strengthCountLimit)
        {
            strengthSpawnTimer -= Time.deltaTime;
            if(strengthSpawnTimer <= 0)
            {
                strengthSpawnTimer = 4;
                Instantiate(strengthPrefab, GenerateSpawnPosition(), strengthPrefab.transform.rotation);
            }
        }

        //Spawn Bomb Pickup
        if(bombUnlocked){
            bombCount = FindObjectsOfType<BombPickup>().Length;
            if(bombCount < bombCountLimit)
            {
                bombSpawnTimer -= Time.deltaTime;
                if(bombSpawnTimer <= 0)
                {
                    bombSpawnTimer = 4;
                    Instantiate(bombPickupPrefab, GenerateSpawnPosition(), rapidFirePrefab.transform.rotation);
                }
            }
        }
    }

    // Spawn Position of Enemy and Powerups
    private Vector3 GenerateSpawnPosition() {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private Vector3 returnEnemySpawnPosition()
    {
        Vector3 spawn = GenerateSpawnPosition();
        while(checkPlayerPosition(spawn) == false)
        {
            spawn = GenerateSpawnPosition();
        }
        return spawn;
    }

    //Checks if Enemy Spawn overlaps with Player boundary
    private bool checkPlayerPosition(Vector3 spawnPosition)
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;
        if(spawnPosition.x > (x-boundaryRange) && spawnPosition.x < (x+boundaryRange) && spawnPosition.z < (z+boundaryRange) && spawnPosition.z > (z-boundaryRange))
        {
            return false;
        }
        return true;
    }

    // Spawns Easy Enemies every wave
    void SpawnEasyEnemyWave(int enemiesToSpawn) {
        for(int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(easyEnemyPrefab, returnEnemySpawnPosition(),
            easyEnemyPrefab.transform.rotation);
        }
    }

    // Spawns Medium enemies every 5 waves
    void SpawnMediumEnemyWave(int wave) {
        float spawn = wave / 5;
        for(int i = 0; i < spawn; i++)
        {
            Instantiate(mediumEnemyPrefab, returnEnemySpawnPosition(),
            mediumEnemyPrefab.transform.rotation);
        }
    }

    // Spanws Hard enemies every 10 waves
    void SpawnHardEnemyWave(int wave) {
        float spawn = wave / 10;
        for(int i = 0; i < spawn; i++)
        {
            Instantiate(hardEnemyPrefab, returnEnemySpawnPosition(),
            hardEnemyPrefab.transform.rotation);
        }
    }

    void healPlayer() {
        playerHealthManager.currentHealth = playerHealthManager.maxHealth;
    }

    void increaseFireRate() 
    {
        gunController.timeBetweenShots *= 0.9f;
    }
}
