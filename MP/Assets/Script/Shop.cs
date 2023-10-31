using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private TurretBlueprint DoublebarrelTurret;
    [SerializeField]
    private TurretBlueprint CannonTurret;
    [SerializeField]
    private TurretBlueprint CrossbowTurret;

    TurrentsBuildManager buidmanager;
    private void Start()
    {
        buidmanager = TurrentsBuildManager.instance;
    }
    public void SelectedDoubleBarrelTurret()
    {
        buidmanager.SelectTurretToBuild(DoublebarrelTurret);
    }

    public void SelectedCannon()
    {
        buidmanager.SelectTurretToBuild(CannonTurret);
    }

    public void SelectedCrossbow()
    {
        buidmanager.SelectTurretToBuild(CrossbowTurret);
    }
}
