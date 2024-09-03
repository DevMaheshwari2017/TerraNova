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

    public GameObject buildParticleEffect;
    public GameObject sellingParticleEffect;
    private TurretBlueprint turretToBuild;
    private GroundHighlight SelectedGround;
    [SerializeField]
    private TurretUI turretui;

    public bool CanBuild {get { return turretToBuild != null; } }
    public bool HasMoney {get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectGround(GroundHighlight node)
    {
        if(SelectedGround == node)
        {
            DeselectGround();
            return;
        }
        SelectedGround = node;
        turretToBuild = null;
        turretui.SetTarget(node);
    }

    public void DeselectGround()
    {
        SelectedGround = null;
        turretui.HideUI();
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectGround();
    }
    
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
