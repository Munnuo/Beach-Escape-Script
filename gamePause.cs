using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gamePause : MonoBehaviour
{
    private Time normalT;
    private GUISkin guiSkin;
    private Rect PauseMenu;
    private bool pause;
    public string MenuScreen;
    public string curLevel;

	// Use this for initialization
	void Start ()
    {
        PauseMenu = new Rect(355, 40, 110, 200);

	}

    void FixedUpdate()
    {
       
    }

    void PauseGame(bool pause)
    {
        if (pause && Time.timeScale != 0.0f)
        {
            Time.timeScale = 0.0f;

        }
        else if (pause && Time.timeScale != 1.0f) 
        {
            Time.timeScale = 1.0f;
        }

    }

    void OnGUI()
    {
        if (Time.timeScale == 0.0f)
        {
            /*if (guiSkin != null)
                GUI.skin = guiSkin;*/
            GUI.Window(0, PauseMenu, showPauseMenu, "Pause Menu");
        }
    }

   void showPauseMenu(int windowID)
    {

        if (GUI.Button(new Rect(5, 20, 100, 20), "Resume"))
            Time.timeScale = 1.0f;
        if(GUI.Button(new Rect(5, 50, 100, 20), "Restart"))
        {
            SceneManager.LoadScene(curLevel);
            Time.timeScale = 1.0f;
        }
        if (GUI.Button(new Rect(5, 80, 100, 20), "Main Menu"))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(MenuScreen);
        }
        if (GUI.Button(new Rect(5, 110, 100, 20), "Quit"))
            Application.Quit();

    }

    // Update is called once per frame
    void Update()
    {
        pause = Input.GetButtonDown("Pause");

        PauseGame(pause);

    }
}
