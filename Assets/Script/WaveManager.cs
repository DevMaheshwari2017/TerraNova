using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private Transform EnemyPrefab;
    [SerializeField]
    private Transform SpawnPosition;
    [SerializeField]
    private float TimeBetweenWaves = 10.0f;
    [SerializeField]
    private float WaveStarter = 3f;
    private int WaveIndex =0;
    [SerializeField]
    private TextMeshProUGUI NumofWaveText;

    IEnumerator SpawnWave()
    {
        WaveIndex++;

        NumofWaveText.text = "Wave " + WaveIndex.ToString();
        int NumofEnemy = WaveIndex + WaveIndex;        
        for (int i = 0; i < NumofEnemy; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(EnemyPrefab, SpawnPosition.position, SpawnPosition.rotation);
    }

    private void Update()
    {
        if(WaveStarter <= 0)
        {
            StartCoroutine(SpawnWave());
            WaveStarter = TimeBetweenWaves;
        }

        WaveStarter -= Time.deltaTime;
    }
}
