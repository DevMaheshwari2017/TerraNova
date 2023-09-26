using UnityEngine;

public class TurrentsBuildManager : MonoBehaviour
{
    public static TurrentsBuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of the turretbuildmanager");
            return;
        }
        instance = this;
    }


    public GameObject DoubleBarrel_turrentprefab;
    public GameObject cannon_turretprefab;
    private Vector3 BuildingPosition;
    [SerializeField]
    private GameObject buildParticleEffect;

    private TurretBlueprint turretToBuild;

    public bool CanBuild {get { return turretToBuild != null; } }
    public bool HasMoney {get { return PlayerStats.Money >= turretToBuild.cost; } }
  
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void BuildingTurretOn(GroundHighlight node)
    {
        if(PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough crystals");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        Debug.Log(" AMount of crystals left after purcahsing turrets " + PlayerStats.Money);

        if (turretToBuild.turretprefab.tag == "DoubleBarrelTurret")
        {
            Debug.Log("Double barrel offset");
            BuildingPosition = node.GetDoubleShooterBuildPos();
        }
        else if (turretToBuild.turretprefab.tag == "CannonTurret")
        {
            Debug.Log("Cannon offset");
            BuildingPosition = node.GetCannonBuildPos();
        }

        GameObject Turret = Instantiate(turretToBuild.turretprefab, BuildingPosition,  Quaternion.identity);
        GameObject Effects = Instantiate(buildParticleEffect, node.transform.position, Quaternion.identity);
        Destroy(Effects, 4f);
        //setting the GroundHighlight/ our grounds turret object to out turret that we are instaniating, node.turret is empty/optional in our inspector. 
        node.turret = Turret;
    }
   
}
