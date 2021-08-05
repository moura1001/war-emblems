using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour{

	private float velocity;

	private const int finalMask = MyGameManager.towerLayerMask | MyGameManager.towerSpotLayerMask;

	private Vector3 dragOrigin;
	private Transform cameraTransform;
	//private Vector3 previusMousePosition;

	// Use this for initialization
	void Start(){
		velocity = 2f;
		cameraTransform = Camera.main.transform;
	}

	// Update is called once per frame
	void Update(){

		#if UNITY_STANDALONE_WIN

		if(Input.GetMouseButtonDown(0)){

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			//if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100.0f, finalMask)){
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, finalMask)){

				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
				//Debug.Log("Did Hit");
				MyGameManager.Instance.interactableGO = hit.transform.gameObject;
				hit.transform.gameObject.GetComponent<IPlayerInteractable>().OnInteraction();

			} else {
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 32, Color.white);
				Debug.DrawRay(ray.origin, ray.direction * 32, Color.white);
				//Debug.Log("Did not Hit");
				dragOrigin = Input.mousePosition;
				//previusMousePosition = Input.mousePosition;
			}

		}

        if(Input.GetMouseButton(0) && HasMouseMoved()){// && previusMousePosition != Input.mousePosition){

			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(pos.x * velocity, 0, pos.y * velocity);
			//Debug.Log("mousePosition:(" + pos.x + ","+pos.y+","+pos.z+")");

			//if(move.z > 200 && move.z < 256 && move.x > 84 && move.x < 116)
			cameraTransform.Translate(move, Space.World);

			//previusMousePosition = Input.mousePosition;
		}

#endif

#if UNITY_ANDROID

		// Track a single touch as a direction control.
		if(Input.touchCount > 0){

			Touch touch = Input.GetTouch(0);
			/*RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			
			// Does the ray intersect any objects excluding the player layer
			//if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100.0f, finalMask)){
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, finalMask)){

				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
				//Debug.Log("Did Hit");
				GameManager.interactableGO = hit.transform.gameObject;
				hit.transform.gameObject.GetComponent<IInteractable>().OnInteraction();

			} else{
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 32, Color.white);
				Debug.DrawRay(ray.origin, ray.direction * 32, Color.white);
				//Debug.Log("Did not Hit");
			}*/

			// Handle finger movements based on TouchPhase
			switch(touch.phase){
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay(touch.position);

					// Does the ray intersect any objects excluding the player layer
					//if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100.0f, finalMask)){
					if (Physics.Raycast(ray, out hit, Mathf.Infinity, finalMask)){

						//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
						Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
						//Debug.Log("Did Hit");
						hit.transform.gameObject.GetComponent<PlayerInteraction>().OnInteraction();

					} else{
						//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 32, Color.white);
						Debug.DrawRay(ray.origin, ray.direction * 32, Color.white);
						//Debug.Log("Did not Hit");
						dragOrigin = ray.origin;
					}
                    //message = "Begun ";
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    //direction = touch.position - startPos;
                    //message = "Moving ";
					Vector2 pos = touch.deltaPosition;
					Vector3 move = new Vector3(pos.x * velocity * Time.deltaTime, 0, pos.y * velocity * Time.deltaTime);

					cameraTransform.Translate(move, Space.World);
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    //message = "Ending ";
                    break;
            }

			// Handle finger movements based on TouchPhase
			/*switch(touch.phase){
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    message = "Begun ";
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position - startPos;
                    message = "Moving ";
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    message = "Ending ";
                    break;
            }*/
		}

#endif

	}

	private bool HasMouseMoved(){
		return ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0));	
	}
}
