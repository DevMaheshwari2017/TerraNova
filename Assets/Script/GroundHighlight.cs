using UnityEngine;
using UnityEngine.EventSystems;

public class GroundHighlight : MonoBehaviour
{
    [SerializeField]
    private Color hovercolor;
    private Renderer _rend;
    private Color originalcolor;
    [SerializeField]
    private Color notEnoughMoneycolor;
    [SerializeField]
    private Vector3 offset= new Vector3(0, 0, 1);

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretblueprint;
    [HideInInspector]
    public bool isUpgraded = false;


    TurrentsBuildManager buildmanager;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        originalcolor = _rend.materials[1].color;
        buildmanager = TurrentsBuildManager.instance;
    }

    public Vector3 GetTurretBuildPos()
    {
        return transform.position + offset;
    } 

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null)
        {
            buildmanager.SelectGround(this);
            return;
        }

        if (!buildmanager.CanBuild)
            return;

        BuildTurret(buildmanager.GetTurretToBuild());
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough crystals");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        Debug.Log(" AMount of crystals left after purcahsing turrets " + PlayerStats.Money);

        GameObject Turret = Instantiate(blueprint.turretprefab, GetTurretBuildPos(), Quaternion.identity);
        //setting the GroundHighlight/ our grounds turret object to out turret that we are instaniating, node.turret is empty/optional in our inspector. 
        turret = Turret;
        turretblueprint = blueprint;
        GameObject Effects = Instantiate(buildmanager.buildParticleEffect, transform.position, Quaternion.identity);
        Destroy(Effects, 4f);        
    }

    public void UpgradeTurret()
    {
        //int lv = 0;
        //if (lv == 2)
        //{
        //    Debug.Log("Turret is upgraded to max level");
        //    return;
        //}
        if (PlayerStats.Money <turretblueprint.upgrade_cost)
        {
            Debug.Log("Not enough crystals to do an upgrade");
            return;
        }
        
        PlayerStats.Money -= turretblueprint.upgrade_cost;
      
        //removing the old turret and placing the upgraded one
        Destroy(turret);

        //building the upgaraded version of the turret
        GameObject Turret = Instantiate(turretblueprint.lv2prefab, GetTurretBuildPos(), Quaternion.identity);
        
        //setting the GroundHighlight/ our grounds turret object to out turret that we are instaniating, turret is hidden in our inspector. 
        turret = Turret;
        //An particle effect that shows up when we upgrade our turret
        GameObject Effects = Instantiate(buildmanager.buildParticleEffect, transform.position, Quaternion.identity);
        Destroy(Effects, 4f);

        isUpgraded = true;
        Debug.Log("Turret upgraded succesfully");
        //lv++;
    
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretblueprint.sellAmount;
        GameObject sellEffect = Instantiate(buildmanager.sellingParticleEffect, GetTurretBuildPos(), Quaternion.identity);
        Destroy(turret);
        Destroy(sellEffect, 4f);        
        turretblueprint = null;
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (!buildmanager.CanBuild)
            return;
        if (buildmanager.HasMoney)
        {
            _rend.materials[1].color = hovercolor;
        }
        else
        {
            _rend.materials[1].color = notEnoughMoneycolor;
        }
        
    }

    private void OnMouseExit()
    {
        _rend.materials[1].color = originalcolor;
    }
}
