using UnityEngine;

public class GroundHighlight : MonoBehaviour
{
    [SerializeField]
    private Color hovercolor;
    private Renderer _rend;
    private Color originalcolor;
    private GameObject turret;
    [SerializeField]
    private Vector3 offset = new Vector3(0,0,1);

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        originalcolor = _rend.materials[1].color;

    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Already a turret there...");
            return;
        }
        GameObject turrettobuild = TurrentsBuildManager.instance.BuildingTurret();
        Vector3 BuildingPosition = offset + transform.position;
        turret = Instantiate(turrettobuild, BuildingPosition, Quaternion.identity);
    }
    private void OnMouseEnter()
    {
        _rend.materials[1].color = hovercolor;
    }

    private void OnMouseExit()
    {
        _rend.materials[1].color = originalcolor;
    }
}
