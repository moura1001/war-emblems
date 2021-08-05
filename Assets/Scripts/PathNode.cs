using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour {

	[SerializeField]
	private Transform nextTransformNode;

    public Transform getNextNode()
    {
		return this.nextTransformNode;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        enemy?.SetNextPathNode(nextTransformNode);
    }
}
