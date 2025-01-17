using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class WaveModeGameManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text enemyCountText;

    [SerializeField] private Vector3 spawnDistance;
    [SerializeField] private AnimationCurve WaveLvl;
    [SerializeField] private GameObject[] portalLvl;

    private int currentWave = 1;
    private float enemyCount = 0;

    public void AddEnemy()
    {
        enemyCount++;
        EnemyCountText();
    }
    public void RemoveEnemy()
    {
        enemyCount--;
        EnemyCountText();
        if (enemyCount <= 0)
        {
            enemyCount = 0;
            EnemyCountText();
            currentWave++;
            StartCoroutine(spawnWave());
        }
    }

    private void EnemyCountText()
    {
        enemyCountText.text = enemyCount.ToString();
    }
    private void Start()
    {
        StartCoroutine(spawnWave());
    }

    private IEnumerator spawnWave()
    {
        int portalsCount = Mathf.RoundToInt(WaveLvl.Evaluate(currentWave));

        for (int i = 0; i < portalsCount; i++)
        {
            spawnPortals();
            Debug.Log("Spawned wave: " + currentWave);
            Debug.Log("Portals count: " + portalsCount);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void spawnPortals()
    {
        int xDirection = UnityEngine.Random.Range(-1, 2);
        int zDirection = UnityEngine.Random.Range(-1, 2);
        

        Vector3 spawnPos = new Vector3(spawnDistance.x * xDirection, 0, spawnDistance.z * zDirection);        
        GameObject portal = Instantiate(portalLvl[0], spawnPos * UnityEngine.Random.Range(-1, 1), Quaternion.identity);
        //enemyCount += portal.GetComponent<EnemySpawns>().maxEnemies;


    }



}
