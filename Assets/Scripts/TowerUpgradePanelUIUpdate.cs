using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradePanelUIUpdate : MonoBehaviour {

	public Text upgradeInfoPanel;
	public Text upgradeValuePanel;

	public void UIUpdateTowerNormal(TowerInfo ti)
	{
		upgradeInfoPanel.text = "Damage: " + ti.damage + "\n\n" +
			"Fire Colldown: " + ti.fireCooldown + "\n\n" +
			"Range: " + ti.range;

		upgradeValuePanel.text = "$" + ti.damageUpCost + "\n\n" +
			"$" + ti.fireCooldownUpCost + "\n\n" +
			"$" + ti.rangeUpCost;		

	}

	public void UIUpdateFlamethrower(TowerInfo ti)
	{
		upgradeInfoPanel.text = "Slow: " + ti.slow * 100 + "%\n\n" +
			"Damage: " + ti.damage * 100 + "%";

		upgradeValuePanel.text = "$" + ti.slowUpCost + "\n\n" +
			"$" + ti.damageUpCost;

	}

}
