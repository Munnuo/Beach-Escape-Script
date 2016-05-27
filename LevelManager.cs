
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform mainMenu, helpMenu;

    private Scene game;
    void Awake()
    {
      
        
    }

    public void LoadScene(string name)
    {
        
        SceneManager.LoadScene(name);
        
    }

    public void helpmenu(bool buttonPressed)
    {
        if(buttonPressed)
        {
            helpMenu.gameObject.SetActive(buttonPressed);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            helpMenu.gameObject.SetActive(buttonPressed);
            mainMenu.gameObject.SetActive(!buttonPressed);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
}
