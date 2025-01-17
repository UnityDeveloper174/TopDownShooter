using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] MeleeEnemyScriptable enemyType;
    
    private GameObject player;
    private EnemySpawns enemySpawner;
    private static WaveModeGameManager waveModeGameManager;
    private float distanceToPlayer;
    private float health;
    private bool isAttacking = false;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(waveModeGameManager == null)
        {
            waveModeGameManager = GameObject.FindGameObjectWithTag("WaveModeGameManager").GetComponent<WaveModeGameManager>();
            waveModeGameManager.AddEnemy();
        }
        else
        {
            waveModeGameManager.AddEnemy();
        }
       
        player = GameObject.FindGameObjectWithTag("Player");
        health = enemyType.health;
    }

    private void OnDestroy()
    {
        waveModeGameManager.RemoveEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToPlayer();
        CheckOtherEnemys();
        Move();

    }

    private void CheckDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
    }
    private void CheckOtherEnemys()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyType.detectionOtherEnemysRange);
        foreach (Collider collider in colliders)
        {
            float distanceToOtherEnemys = Vector3.Distance(collider.transform.position, transform.position);
            if (collider.CompareTag("Enemy") && distanceToOtherEnemys < enemyType.detectionOtherEnemysRange)
            {
                
                if (collider.gameObject != gameObject)
                {
                    Vector3 direction = (collider.transform.position - transform.position).normalized;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(-direction);
                    transform.Translate(Vector3.forward * enemyType.speed * Time.deltaTime);
                
                }
                
            }
        }
    }
    private void Move()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        
        direction.y = 0;

        transform.rotation = Quaternion.LookRotation(direction);

        if (distanceToPlayer > enemyType.attackRange)
        {
            transform.Translate(Vector3.forward * enemyType.speed * Time.deltaTime);
            if (isAttacking)
            {
                isAttacking = false;
                CancelInvoke("Attack");
            }
        }
        else
        {
            if (!isAttacking)
            {
                isAttacking = true;
                InvokeRepeating("Attack", 0, enemyType.attackSpeed);
            }
        }

        
    }

    private void Attack()
    {
        player.GetComponent<CharacterHealth>().TakeDamage(enemyType.damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject dieEffect = Instantiate(enemyType.deathEffect, transform.position, enemyType.deathEffect.transform.rotation);
            Destroy(dieEffect, 1f);
            
        }
    }

    /*public void SetEnemySpawner(EnemySpawns spawner)
    {
        enemySpawner = spawner;
    }*/

  

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyType.attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyType.detectionOtherEnemysRange);
    }

}
