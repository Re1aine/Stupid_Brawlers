using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
   private Button _button;

   private void Awake()
   {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(LoadNextLevel);
   }
   
   private void LoadNextLevel()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
   }
}