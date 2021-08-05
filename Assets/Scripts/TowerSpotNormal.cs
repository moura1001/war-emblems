using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotNormal : MonoBehaviour, IPlayerInteractable {

	public void OnInteraction()
    {
		Debug.Log("TowerSpot clicked.");

		if (MyGameManager.Instance.buildingManagerScript.selectedTower != null)
		{

			if (MyGameManager.Instance.scoreManagerScript.money < MyGameManager.Instance.buildingManagerScript.selectedTower.GetComponent<TowerInfo>().cost)
			{
				Debug.Log("No enough money!");
				return;
			}

			MyGameManager.Instance.scoreManagerScript.money -= MyGameManager.Instance.buildingManagerScript.selectedTower.GetComponent<TowerInfo>().cost;
			MyGameManager.Instance.scoreManagerScript.UIUpdate();

			// FIXME: Rigth now we assume that we're an object nested in a parent.
			Instantiate(MyGameManager.Instance.buildingManagerScript.selectedTower, transform.parent.position, transform.parent.rotation);
			//Destroy(transform.parent.gameObject);
			transform.parent.gameObject.SetActive(false);

		}

		MyGameManager.Instance.buildingManagerScript.selectedTower = null;
	}

}
