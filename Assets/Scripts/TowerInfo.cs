using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour, IPlayerInteractable {

	public int cost;

	public float damage;
	public int damageUpCost;

	public float fireCooldown;
	public int fireCooldownUpCost;

	public float range;
	public int rangeUpCost;

	public float slow = 0.1f;
	public int slowUpCost;

    public void OnInteraction()
    {
		gameObject.GetComponent<ITowerUpgrade>().UIPanelUpdate();

		Debug.Log("??? clicked.");
	}

}
