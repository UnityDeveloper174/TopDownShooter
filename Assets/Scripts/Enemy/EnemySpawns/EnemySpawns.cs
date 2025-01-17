using System.Collections;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    [Header("Base Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    [Header("Spawn Settings")]
    public int maxEnemies = 10;
    public float spawnInterval = 3.5f;

    private int currentEnemies = 0;
    private Animator animator;

   




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        animator.SetTrigger("open");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 2f);
        while (currentEnemies < maxEnemies)
        {
          
            currentEnemies++;
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            //enemy.GetComponent<Enemy>().SetEnemySpawner(this);
            
            yield return new WaitForSeconds(spawnInterval);
        }
        closePortal();
    }


    /*public void EnemyDied()
    {
        currentEnemies--;
      
    }*/

    private void closePortal()
    { 
        animator.SetTrigger("close");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void PortalDestroy()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
}
