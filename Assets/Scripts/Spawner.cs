using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour, IDamagable
{
    public Action deathHandler;
    private float curHealth;
    [SerializeField] private float maxHealth = 500f;
    public float spawnCD = 15f;
    private int amountForSpawn = 3;
    public float spawnRange = 3f;
    private float nextTimeToSpawn = 0f;
    private bool isLowHP = false;

    private bool spawnIsActive = false;
    private Enemy[] spawnedEnemies = new Enemy[4];
    private int enemyLeft;
    private bool isDied = false;

    public HealthBar healthbar;
    public Enemy[] enemies;
    public GameObject drop;

    void Start()
    {
        curHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(Time.time >= nextTimeToSpawn && spawnIsActive)
        {
            nextTimeToSpawn = Time.time + spawnCD;
            Spawn();
        }
    }

    public void ActivateSpawn()
    {
        spawnIsActive = true;
    }

    private void Spawn()
    {
        for(int i = 0; i < spawnedEnemies.Length; i++)
        {
            if (spawnedEnemies[i] != null) continue;
            int toSpawn = Random.Range(0, enemies.Length);
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));

            Enemy spawnedEnemy = Instantiate(enemies[toSpawn], pos, this.transform.rotation);
            spawnedEnemy.DiedCallBackAction += DiedHandler;

            spawnedEnemies[i] = spawnedEnemy;
        }
    }

    private void DiedHandler(Enemy killed)
    {
        for(int i = 0; i < spawnedEnemies.Length; i++)
        {
            if (spawnedEnemies[i] == killed)
            {
                spawnedEnemies[i] = null;
            }
        }
        if (isDied) EnemyDetect(killed);
    }

    public void TakeDamage(float amount)
    {
        curHealth -= amount;
        healthbar.SetHealth(curHealth);
        if(curHealth <= 0f)
        {
            DieState();
        }
        else if(!isLowHP && curHealth <= 0.3 * maxHealth)
        {
            isLowHP = true;
            spawnCD = 8f;
            Array.Resize(ref spawnedEnemies, 7);
            Spawn();
        }
    }

    public void DieState()
    {
        MessageWindow.instance.SendMessage("Kill all enemies to open the door");
        this.gameObject.SetActive(false);
        enemyLeft = spawnedEnemies.Length;
        foreach(Enemy enemy in spawnedEnemies)
        {
            if (enemy == null) enemyLeft--;
        }
        isDied = true;
    }

    public void EnemyDetect(Enemy enemy)
    {
        if(--enemyLeft <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        deathHandler?.Invoke();
        Destroy(gameObject);
    }
}
