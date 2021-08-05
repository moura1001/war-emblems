using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeFlamethorwer : MonoBehaviour, ITowerUpgrade
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
				if (MyGameManager.Instance.scoreManagerScript.money >= towerInfo.slowUpCost && towerInfo.slow < 0.5f)
				{
					towerInfo.slow += 0.1f;
					MyGameManager.Instance.scoreManagerScript.money -= towerInfo.slowUpCost;
					towerInfo.slowUpCost *= 2;
				}
				break;

			case 1:
				if (MyGameManager.Instance.scoreManagerScript.money >= towerInfo.damageUpCost && towerInfo.damage < 0.5f)
				{
					towerInfo.damage += 0.1f;
					MyGameManager.Instance.scoreManagerScript.money -= towerInfo.damageUpCost;
					towerInfo.damageUpCost *= 2;
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
			MyGameManager.Instance.towersPanelUI.SetActive(false);
			MyGameManager.Instance.flamethrowerPanelUI.SetActive(activeUpgradePanel);
			MyGameManager.Instance.flamethrowerPanelUI.GetComponent<TowerUpgradePanelUIUpdate>().UIUpdateFlamethrower(towerInfo);
		}
		else
		{
			MyGameManager.Instance.flamethrowerPanelUI.SetActive(false);
			MyGameManager.Instance.towersPanelUI.SetActive(false);
		}

	}

	public void UIPanelUpdate()
	{
		UpdateTowerUpgradePanel();
	}
}
