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

    [SerializeField]
    private GameObject turrentprefab;
    private GameObject turretToBuild;

    private void Start()
    {
        turretToBuild = turrentprefab;
    }
    public GameObject BuildingTurret()
    {
        return turretToBuild;
    }
}
