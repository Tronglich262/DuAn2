using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class Dialogue : MonoBehaviour
{
   public GameObject window;
   public GameObject indicator;
   public List<string> dialogues;
   private int index;
   private int charIndex;
   public float writingspeed;
   public TMP_Text dialoguetext;
   private bool started;
   private bool waiForNext;

   private void Awake()
   {
      ToggleIndicator(false);
      ToggleWindow(false);
   }

   private void Start()
   {
      
   }

   private void ToggleWindow(bool show)
   {
      window.SetActive(show);
   }
   public void ToggleIndicator(bool show)
   {
      indicator.SetActive(show);
   }

   public void StartDialogue()
   {
      if (started) return;
      started = true;
      window.SetActive(true);
      ToggleIndicator(false);
      GetDialogue(0);
   }

   private void GetDialogue(int i)
   {
      index = i;
      charIndex = 0;
      dialoguetext.text = string.Empty;
      StartCoroutine(Writing());
   }

   public void EndDialogue()
   {
      started = false;
      waiForNext = false;
      StopAllCoroutines();
      ToggleWindow(false);
   }

   IEnumerator Writing()
   {
      yield return new WaitForSeconds(writingspeed);
      string currentDialogue = dialogues[index];
      dialoguetext.text += currentDialogue[charIndex];
      charIndex++;
      if (charIndex < currentDialogue.Length)
      {
         yield return new WaitForSeconds(writingspeed);
         StartCoroutine(Writing());  
      }
      else
      {
         waiForNext = true;
      }
   }

   private void Update()
   {
      if(!started)
         return;
      if (waiForNext && Input.GetKeyDown(KeyCode.E))
      {
         waiForNext = false;
         index++;
         if (index < dialogues.Count)
         {
            GetDialogue(index);
         }
         else
         {
            ToggleIndicator(true);
            EndDialogue();
         }
      }
      
   }
}
