using UnityEngine;
using Photon.Pun;
public class Turrets : MonoBehaviourPunCallbacks
{
    [Header("Unity Inspector field")]
    [SerializeField]
    private Transform target;
    private Enemy targetenemy;
     
    [Header("Bullet field")]
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private Transform BulletFirePoint;
    private Transform BulletFirePoint_2;

    [Header("Turrent Functionalities")]
    [SerializeField]
    private float range;
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private float Firerate = 1f;
    private float fireCountDown;



    private void Awake()
    {
        target = null;

    }
    void Start()
    {
        if (gameObject.CompareTag("DoubleBarrelTurret"))
        {
            BulletFirePoint_2 = transform.Find("BulletFirePoint_2");
            if (BulletFirePoint_2 == null)
            {
                Debug.LogError("BulletFirePoint_2 not found as a child of the DoubleBarrelTurret.");
            }
        }

        //instead of checking if there's any enemy near us 60 times a sec we are invoking/checking for enemy every 0.5sec means 2 times only.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject NearestEnemy = null;
        float ShortestDistance = Mathf.Infinity; // shortest distance from enemy if enemy is not near then by deafult its infinity
        foreach (GameObject Enemy in Enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);
            if (DistanceToEnemy < ShortestDistance) // if we have found new enemy near us 
            {
                ShortestDistance = DistanceToEnemy;
                NearestEnemy = Enemy; // nearest enemy that we have found so far 
            }
        }
        // if we have found an enemy near us withinh our range
        if (NearestEnemy != null && ShortestDistance <= range)
        {
            target = NearestEnemy.transform;
            targetenemy = NearestEnemy.GetComponent<Enemy>();

        }
        else
        {
            target = null;
        }
    }

    //Gizmo sphere for the range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Shoot()
    {
        if (target != null)
        {
            GameObject bulletshooter = PhotonNetwork.Instantiate(BulletPrefab.name, BulletFirePoint.position, BulletFirePoint.rotation);
            Bullets bullet = bulletshooter.GetComponent<Bullets>();
            if (bullet != null)
            {
                bullet.Go_Bullet(target);
            }

            //spawning 2 bullets as DoubleBarrel turrent have 2 shooting barrels
            if (gameObject.tag == "DoubleBarrelTurret")
            {
                GameObject bulletshooter_2 = PhotonNetwork.Instantiate(BulletPrefab.name, BulletFirePoint_2.position, BulletFirePoint_2.rotation);
                Bullets bullet_2 = bulletshooter_2.GetComponent<Bullets>();

                if (bullet_2 != null)
                {

                    bullet_2.Go_Bullet(target);
                }
            }

        }

    }

    private void LookOnTarget()
    {
        // if target gets locked on rotating our head with it 
        Vector3 Direction = target.position - transform.position;
        Quaternion TurrentRoatation = Quaternion.LookRotation(Direction);
        Vector3 Rotation = Quaternion.Lerp(gameObject.transform.rotation, TurrentRoatation, rotationSpeed * Time.deltaTime).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, Rotation.y, 0f);
    }

    private void BulletFirerate()
    {

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / Firerate;
        }

        fireCountDown -= Time.deltaTime;

    }
    private void Update()
    {
        if (target == null)
            return;

        LookOnTarget();
        BulletFirerate();

    }
}
