using UnityEngine;

public class Bullets : MonoBehaviour
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

    private void TargetHit()
    {
        GameObject Effects = Instantiate(BulletHitParticalEffects, transform.position, transform.rotation);
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
        EnemyMovement _enemy = enemy.GetComponent<EnemyMovement>();
        if(_enemy == null)
        {
            Debug.Log("EnemyMovement is null inside bullet script");
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
