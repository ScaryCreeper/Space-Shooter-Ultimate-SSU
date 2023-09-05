using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyCointainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerups;

    
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
            
    }


    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        float xAxis = Random.Range(-9.5f, 9.5f);
        while (_stopSpawning == false)
        { 
          
           GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3 (xAxis, 7, 0), Quaternion.identity );
            newEnemy.transform.parent = _enemyCointainer.transform;
           yield return new WaitForSeconds(2.5f);

        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_stopSpawning == false)
        {
            float xAxis = Random.Range(-9.5f, 9.5f);
            float randomTime = Random.Range(6f, 12f);
            yield return new WaitForSeconds(randomTime);
            int randomPowerup = Random.Range(0,3);
            Instantiate(_powerups[randomPowerup], new Vector3(xAxis, 7, 0), Quaternion.identity);
        } 
        
           
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;   
    }

}
