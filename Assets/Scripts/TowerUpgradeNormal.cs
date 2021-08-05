using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeNormal : MonoBehaviour, ITowerUpgrade
{
	private bool activeUpgradePanel;
	private TowerInfo towerInfo;

	// Start is called before the first frame update
	void Start()
	{
		activeUpgradePanel = false;
		towerInfo = gameObject.GetComponent<TowerInfo>();
	}

	public void UpgradeRequest(int attributeUpgrade)
    {
		switch (attributeUpgrade)
		{

			case 0:
				if (MyGameManager.Instance.scoreManagerScript.money >= towerInfo.damageUpCost && towerInfo.damage < 20)
				{
					towerInfo.damage += 2;
					MyGameManager.Instance.scoreManagerScript.money -= towerInfo.damageUpCost;
					towerInfo.damageUpCost *= 2;
				}
				break;

			case 1:
				if (MyGameManager.Instance.scoreManagerScript.money >= towerInfo.fireCooldownUpCost && towerInfo.fireCooldown > 0.1f)
				{
					towerInfo.fireCooldown -= 0.05f;
					MyGameManager.Instance.scoreManagerScript.money -= towerInfo.fireCooldownUpCost;
					towerInfo.fireCooldownUpCost *= 2;
				}
				break;

			/*case 2:
				if(SCRIPTS.scoreManagerScript.money >= towerInfo.radiusUpCost && t.radius > 0 && t.radius <= 1){
					t.radius += 0.2f;
					SCRIPTS.scoreManagerScript.money -= towerInfo.radiusUpCost;
					towerInfo.radiusUpCost *= 2;
				}
				break;*/

			case 2:
				if (MyGameManager.Instance.scoreManagerScript.money >= towerInfo.rangeUpCost && towerInfo.range < 200)
				{
					towerInfo.range += 20;
					MyGameManager.Instance.scoreManagerScript.money -= towerInfo.rangeUpCost;
					towerInfo.rangeUpCost *= 2;
				}
				break;

		}

		MyGameManager.Instance.scoreManagerScript.UIUpdate();
		activeUpgradePanel = false;
		UpdateTowerUpgradePanel();
	}

	private void UpdateTowerUpgradePanel()
	{
		activeUpgradePanel = !activeUpgradePanel;

		if (activeUpgradePanel)
		{
			MyGameManager.Instance.flamethrowerPanelUI.SetActive(false);
			MyGameManager.Instance.towersPanelUI.SetActive(activeUpgradePanel);
			MyGameManager.Instance.towersPanelUI.GetComponent<TowerUpgradePanelUIUpdate>().UIUpdateTowerNormal(towerInfo);

		}
		else
		{
			MyGameManager.Instance.towersPanelUI.SetActive(false);
			MyGameManager.Instance.flamethrowerPanelUI.SetActive(false);
		}

	}

    public void UIPanelUpdate()
    {
		UpdateTowerUpgradePanel();
    }
}
