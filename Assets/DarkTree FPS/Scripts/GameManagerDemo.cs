using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerDemo : MonoBehaviour {

    public Text help;
    bool showHelp = true;

	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (help != null)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                showHelp = !showHelp;
            }

            help.enabled = showHelp;
        }
	}
}
