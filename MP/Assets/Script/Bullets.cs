using UnityEngine;
using Photon.Pun;
public class Bullets : MonoBehaviourPunCallbacks
{
    private Transform target;
    [SerializeField]
    private float bulletspeed = 25f;
    [SerializeField]
    private float exlposionradius = 2f;
    [SerializeField]
    private GameObject BulletHitParticalEffects;
    [SerializeField]
    private int Dmg = 25;

    public void Go_Bullet(Transform _traget)
    {
        target = _traget;
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,exlposionradius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    public bool AttachedToArrow()
    {
        if (gameObject.CompareTag("Arrow"))
            return true;
        
        return false;
    }
    private void TargetHit()
    {
        GameObject Effects = PhotonNetwork.Instantiate(BulletHitParticalEffects.name, transform.position, transform.rotation);
        Destroy(Effects, 3.5f);
        if (exlposionradius > 0f)
        {
            Explosion();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
    }
    private void Damage(Transform enemy)
    {
        Enemy _enemy = enemy.GetComponent<Enemy>();
        if(_enemy == null)
        {
            Debug.Log("Enemy is null inside bullet script");
        }
        if (AttachedToArrow())
        {
            _enemy.arrowThatHitMe = gameObject;
            Debug.Log("Arrow component got attached to the enemy");
        }
            _enemy.TakeDamage(Dmg);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, exlposionradius);
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 Direction = target.position - transform.position;
        float DistanceThisFrame = bulletspeed * Time.deltaTime;

        //dir.mag = lenght of the direction vector - current distance to target
        if(Direction.magnitude <= DistanceThisFrame)// less than means that we alredy have hit the traget
        {
            TargetHit();
            return;
        }

        transform.Translate(Direction.normalized * DistanceThisFrame, Space.World);
        transform.LookAt(target);
    }
}
