using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenInput : MonoBehaviour{

	void Update(){
		#if UNITY_STANDALONE_WIN
		if(Input.anyKey)
			SceneManager.LoadScene(1);
		#endif

		#if UNITY_ANDROID
		if (Input.touchCount > 0)
			SceneManager.LoadScene(1);
		#endif
	}

}
