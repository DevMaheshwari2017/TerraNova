using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float bulletspeed = 25f;
    [SerializeField]
    private GameObject BulletHitParticalEffects;

    public void Go_Bullet(Transform _traget)
    {
        target = _traget;
    }

    private void TargetHit()
    {
        GameObject Effects = Instantiate(BulletHitParticalEffects, transform.position, transform.rotation);
        Destroy(Effects, 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);
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
    }
}
