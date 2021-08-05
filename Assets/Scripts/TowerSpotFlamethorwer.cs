using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotFlamethorwer : MonoBehaviour, IPlayerInteractable
{
	public void OnInteraction()
	{
		Debug.Log("TowerSpot clicked.");

		if (MyGameManager.Instance.buildingManagerScript.selectedTower == null)
		{

			if (MyGameManager.Instance.scoreManagerScript.money < MyGameManager.Instance.buildingManagerScript.specialTower.GetComponent<TowerInfo>().cost)
			{
				Debug.Log("No enough money!");
				return;
			}

			MyGameManager.Instance.scoreManagerScript.money -= MyGameManager.Instance.buildingManagerScript.specialTower.GetComponentInChildren<TowerInfo>().cost;
			MyGameManager.Instance.scoreManagerScript.UIUpdate();

			Instantiate(MyGameManager.Instance.buildingManagerScript.specialTower, transform.parent.position, transform.parent.rotation);
			//Destroy(transform.parent.gameObject);

			transform.parent.gameObject.SetActive(false);

		}
	}
}
