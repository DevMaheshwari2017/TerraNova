using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class WaveManager : MonoBehaviourPunCallbacks
{
    public static int EnemiesAlive = 0;

    [SerializeField]
    private Waves[] waves;
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
        if(WaveIndex == waves.Length && EnemiesAlive == 0)
        {           
            Debug.Log("game completed");
            GameManager.Instance.GameWon = true;
            this.enabled = false;
        }
        Waves wave = waves[WaveIndex];       
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }
        WaveIndex++;
        NumofWaveText.text = "Wave " + WaveIndex.ToString();
    }

    private void SpawnEnemy(GameObject enemy)
    {
        PhotonNetwork.Instantiate(enemy.name, SpawnPosition.position, SpawnPosition.rotation);
        EnemiesAlive++;
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }
        if(WaveStarter <= 0)
        {
            StartCoroutine(SpawnWave());
            WaveStarter = TimeBetweenWaves;
            return;
        }

        WaveStarter -= Time.deltaTime;
    }
}
