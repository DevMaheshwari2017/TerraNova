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
    private Vector3 offset_DoubelBarrel = new Vector3(0, 0, 1);
    [SerializeField]
    private Vector3 offset_Cannon = new Vector3(0, 0, 1);

    [Header("optional")]
    public GameObject turret;
   

    TurrentsBuildManager buildmanager;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        originalcolor = _rend.materials[1].color;
        buildmanager = TurrentsBuildManager.instance;
    }

    public Vector3 GetDoubleShooterBuildPos()
    {
        return transform.position + offset_DoubelBarrel;
    } 
    public Vector3 GetCannonBuildPos()
    {
        return transform.position + offset_Cannon;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (!buildmanager.CanBuild)
        {
            return;
        }
        if (turret != null)
        {
            Debug.Log("Already a turret there...");
            return;
        }
        buildmanager.BuildingTurretOn(this);
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
