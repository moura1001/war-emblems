using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Tower shooter;

	public Transform target = null;

	private static float speed = 100f;
	public float damage = 0f;

	// Update is called once per frame
	void Update(){
		
		if(target == null){
			shooter.shoot = true;
			// Our enemy went away!
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		if(dir.magnitude <= distThisFrame){
			// We reached the node
			DoBulletHit();

		} else {
			// TODO: Consider ways to smooth this motion.

			// Move towards node
			transform.Translate(dir.normalized * distThisFrame, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation(dir);
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*40);
		}

	}

	void DoBulletHit(){

		target.GetComponent<Enemy>().TakeDamage(damage);

		// TODO: Maybe spawn a cool "explosion" object here?

		MyGameManager.Instance.explosionScript.Explode(gameObject);
		
		shooter.shoot = true;

		Destroy(gameObject);
	}

}
