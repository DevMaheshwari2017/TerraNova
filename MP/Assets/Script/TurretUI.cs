using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    [SerializeField]
    private GameObject canavs;
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private TextMeshProUGUI upgradeCost;
    [SerializeField]
    private TextMeshProUGUI sellAmount;
    private GroundHighlight target;

    private void Start()
    {
        canavs.SetActive(false);
    }
    public void SetTarget( GroundHighlight _target)
    {
        target = _target;
        transform.position = target.GetTurretBuildPos();
        if (!target.isUpgraded)
        {
            upgradeCost.text = target.turretblueprint.upgrade_cost + "C";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MaxLevel";
            upgradeButton.interactable = false;
        }

        sellAmount.text = target.turretblueprint.sellAmount + "C";
        canavs.SetActive(true);
    }

    public void HideUI()
    {
        canavs.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        //not calling the hide() directly then only the UI will disapper but the ground will still be selected 
        TurrentsBuildManager.instance.DeselectGround();
    }

    public void Sell()
    {
        target.SellTurret();
        TurrentsBuildManager.instance.DeselectGround();
    }
}
