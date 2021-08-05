using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public ParticleSystem particle;

	public void Explode(GameObject go){

		var p = Instantiate(particle, go.transform.position, go.transform.rotation);
		p.Play();
		Destroy(p.gameObject, 8.0f);
	}
}
