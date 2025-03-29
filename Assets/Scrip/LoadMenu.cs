using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject setting;
    public GameObject help;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (menu == null)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startgame()
    {
        SceneManager.LoadScene("Demo");
    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
        setting.SetActive(true);
    } 
    public void HelpMenu()
    {
        menu.SetActive(!menu.activeSelf);
        help.SetActive(true);
    }

    public void BackMenu()
    {
        menu.SetActive(!menu.activeSelf);
        setting.SetActive(false);
    }
    public void HelpBackMenu()
    {
        menu.SetActive(!menu.activeSelf);
        help.SetActive(false);
    }
    

   

}