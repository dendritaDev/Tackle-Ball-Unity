using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;

    public int enemyCount;
    public int enemyWave = 1;

    public GameObject powerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(1);
    }

    private void Update()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            int numberOfPowerUps = Random.Range(0, 3);
            for (int i = 0; i < numberOfPowerUps; i++)
            {
                Instantiate(powerUpPrefab, RandomPosition(), powerUpPrefab.transform.rotation);
            }
            
        }
    }

    /// <summary>
    /// Genera una posición aleatoria dentro de la zona del juego
    /// </summary>
    /// <param name="Vector3"> parametro para </param>
    /// <returns>Devuelve una posicion aleatoria dewntro de la zona del juego</returns>
    private Vector3 RandomPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    /// <summary>
    /// Método que genera un número determinado de enemigos en pantalla
    /// </summary>
    private void SpawnEnemyWave(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, RandomPosition(), enemyPrefab.transform.rotation);
        }

    }
}
