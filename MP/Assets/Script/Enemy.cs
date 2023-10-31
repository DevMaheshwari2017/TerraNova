using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Enemy : MonoBehaviourPunCallbacks
{
    [Header("Enemy properties")]
    [SerializeField]
    private float enemyStartHealth = 100;
    [SerializeField]
    private float currentEnemyHealth;
    [SerializeField]
    private int MoneyGain = 15;
    [SerializeField]
    private GameObject DeathEffect;
    [SerializeField]
    private Image healthBar;

    //private Bullets bullet;

   [Header("Bleeding Effects")]
    [SerializeField]
    private int bleedingovertime;
    [SerializeField]
    private GameObject bleedinglighteffect;
    [SerializeField]
    private bool isbleeding = false;
    public GameObject arrowThatHitMe;

    private void Start()
    {       
        bleedinglighteffect.SetActive(false);
        currentEnemyHealth = enemyStartHealth;
    }
    public void TakeDamage(float dmg)
    {
        currentEnemyHealth -= dmg;
        // the filled that we are using inside inspector works btw 0-1 so we need something like 100/100 but insead of hard coding we use currentenemyhealth.
        healthBar.fillAmount = currentEnemyHealth / enemyStartHealth;
        if (currentEnemyHealth <= 0)
        {
            Debug.Log("Enemy is dead " + currentEnemyHealth);
            EnemyDead();
        }
    }
    private void Bleeding()
    {
        if (arrowThatHitMe != null)
        {
            Debug.Log("Arrow component has been found");
            Bullets bullet = arrowThatHitMe.GetComponent<Bullets>();
            if (bullet != null)
            {
                isbleeding = true;
                Debug.Log("Enemy bleeding effect is active");
                bleedinglighteffect.SetActive(true);
            }
        }
    }

    private void DamageOverTime()
    {
        if (arrowThatHitMe != null)
        {
            TakeDamage(bleedingovertime * Time.deltaTime);
            Debug.Log("Enemy health is depleting over time " + bleedingovertime);
            Debug.Log("EnemyHeal is " + currentEnemyHealth);
        }
    }

    private void EnemyDead()
    {
        PlayerStats.Money += MoneyGain;
        GameObject Effects =PhotonNetwork.Instantiate(DeathEffect.name, transform.position, Quaternion.identity);
        Destroy(Effects, 4f);
        WaveManager.EnemiesAlive--;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!isbleeding)
        {
            Debug.Log("Bleeding is on");
            Bleeding();
        }
        if (isbleeding)
        {
            DamageOverTime();
        }
    }
}
