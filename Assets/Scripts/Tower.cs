using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public GameObject turretGO;
	public GameObject baseGO;

	public GameObject bulletPrefab;

	private Transform turretTransform;
	private Transform baseTransform;

	private float fireCooldownLeft = 0f;

	private TowerInfo towerInfoScript;

	private Animator animator;

	public bool shoot;

	// Use this for initialization
	void Start(){

		MyGameManager.Instance.AddHasEnemiesListener(DeactivateAnimatiorComponent);
		MyGameManager.Instance.AddHasNotEnemiesListener(ActivateAnimatiorComponent);
		
		shoot = true;

		turretTransform = turretGO.transform;;
		baseTransform = baseGO.transform;

		towerInfoScript = gameObject.GetComponent<TowerInfo>();

		animator = gameObject.GetComponentInChildren<Animator>();
		animator.enabled = (MyGameManager.Instance.enemies.Count == 0);
	}
	
	// Update is called once per frame
	void Update(){

		// TODO: Optimize this!
		//Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

		GameObject nearestEnemy = null;
		float dist = Mathf.Infinity;

		for(int i = 0; i < MyGameManager.Instance.enemies.Count; i++){
			GameObject e = (GameObject) MyGameManager.Instance.enemies[i];
			//float d = Vector3.Distance(transform.position, e.transform.position);
			float d = (e.transform.position - transform.position).sqrMagnitude;
			if (nearestEnemy == null || d < dist){
				nearestEnemy = e;
				dist = d;
			}
		}

		if(nearestEnemy == null){
			//Debug.Log("No enemies");
			return;
		}

		Vector3 dir = transform.position - nearestEnemy.transform.position;

		Quaternion lookRot = Quaternion.LookRotation(dir);

		turretTransform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);

		fireCooldownLeft -= Time.deltaTime;
		if(fireCooldownLeft <= 0 && dir.magnitude <= towerInfoScript.range){
			fireCooldownLeft = towerInfoScript.fireCooldown;
			if(shoot){
				shoot = false;
				ShootAt(nearestEnemy);
			}
		}

	}

	void ShootAt(GameObject e){
		// TODO: Fire out the tipe!
		Vector3 pos = new Vector3(transform.position.x, transform.position.y + 6.0f, transform.position.z); 
		Bullet b = Instantiate(bulletPrefab, pos, transform.rotation).GetComponent<Bullet>();

		b.shooter = this;
		b.target = e.transform;
		b.damage = towerInfoScript.damage;

	}

	private void ActivateAnimatiorComponent()
	{
		if (!animator.enabled)
			animator.enabled = true;
	}

	private void DeactivateAnimatiorComponent(){
		if (animator.enabled)
			animator.enabled = false;
	}

}
