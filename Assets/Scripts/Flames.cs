using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour{

	private BoxCollider flamesCheckbox;

	public ParticleSystem particle;
	private ParticleSystem p;
	//private bool takeDamage;
	//private short enemysPass;

	private TowerInfo towerInfoScript;

	void Awake(){

		flamesCheckbox = gameObject.GetComponent<BoxCollider>();
		towerInfoScript = transform.parent.gameObject.GetComponent<TowerInfo>();
		MyGameManager.Instance.AddHasNotEnemiesListener(StopFlamethrower);
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Enemy")){
			//Debug.Log("Enter");
			other.gameObject.GetComponent<Enemy>().DecreaseSpeed(towerInfoScript.slow);
			other.gameObject.GetComponent<Enemy>().DecreaseHealth(towerInfoScript.damage);
			//takeDamage = true;
			//if(p == null)
			PlayFlamethrower();
		}
	}

	/*void OnTriggerStay(Collider other){
		if(other.gameObject.CompareTag("Enemy"))
		{
			//Debug.Log("Stay");
			//other.gameObject.GetComponentInChildren<Enemy>().DecreaseSpeed(slow);
			//other.gameObject.GetComponentInChildren<Enemy>().DecreaseHealth(damage);
			//takeDamage = true;
		}
	}*/

	void OnTriggerExit(Collider other){

		//Enemy enemy = other.gameObject.GetComponent<Enemy>();

		if(other.gameObject.CompareTag("Enemy")){
			if(other.gameObject.GetComponent<Enemy>().lastSpawned){
				StopFlamethrower();
			}

			//Debug.Log(enemy.lastSpawned);
		}
		
	}

	public void PlayFlamethrower(){

		if(p == null){
			short mul = gameObject.transform.position.x <= 10 ? (short)1 : (short)-1;

			p = Instantiate(particle, new Vector3(gameObject.transform.position.x + mul * 6, 15.5f, gameObject.transform.position.z), Quaternion.Inverse(gameObject.transform.rotation));
		}	

		if(!p.isEmitting)
			p.Play();
	}

	public void StopFlamethrower(){

		//if (!takeDamage || SCRIPTS.enemies.Count == 0){
		//enemysPass = 0;
		if(p != null && p.isEmitting){
			p.Stop();
			//Destroy(p.gameObject, 8.0f);
		}
		//flamesCheckbox.enabled = false;
		//}

	}

	//public void SetDamage(bool takeDamage){
	//	this.takeDamage = takeDamage;
	//}

}
