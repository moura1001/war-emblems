using UnityEngine;

public class HomeScreenTarget : MonoBehaviour {

    public static Vector3 GetTarget(EnemyType enemyType){

		int index = Random.Range(0, MyGameManager.Instance.targetsGO.transform.childCount - 1);
		Transform t = MyGameManager.Instance.targetsGO.transform.GetChild(index);
		
		Vector3 targetPosition = new Vector3(t.position.x, 0, t.position.z);

		/*switch (enemyType)
		{
			case EnemyType.ELITE:
				break;

			case EnemyType.NORMAL:
				targetPosition.y = Random.Range(0, 14);
				break;
		}*/

		if(enemyType == EnemyType.NORMAL)
			targetPosition.y = Random.Range(0, 14);

		return targetPosition;
	}

}
