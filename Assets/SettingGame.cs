using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingGame : MonoBehaviour
{
   public GameObject menu;
   public GameObject setting;
   public GameObject help;

   private void Update()
   {
      
   }

   public void MenuGame()
   {
      menu.SetActive(!menu.activeSelf);
   }

   public void SettingGame1()
   {
      menu.SetActive(!setting.activeSelf);
      setting.SetActive(true);
   }

   public void HelpGame()
   {
      menu.SetActive(!menu.activeSelf);
      help.SetActive(true);
   }

   public void SettingBackToMenu()
   {
      setting.SetActive(!setting.activeSelf);
      menu.SetActive(true);
   }
   public void HelpBackToMenu()
   {
      help.SetActive(false);
      menu.SetActive(true);
   }

   public void backmenu()
   {
      menu.SetActive(!menu.activeSelf);
   }
   public void Back()
    {
        SceneManager.LoadScene("MENU");
    }
}
