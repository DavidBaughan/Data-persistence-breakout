using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
   
        public static TitleScreenManager Instance;
    private bool isNameEntered = false;
    public string currentPlayername;
    public Text playerEnteredText;


        private void Awake()
        {

            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

           
        }

        // Update is called once per frame


     public  void PlayerNameEntered()
    {
        isNameEntered = true;
        currentPlayername = playerEnteredText.text;

    }


   public void StartButtonPressed()
    {
        if (isNameEntered == true)
        {
            SceneManager.LoadScene(1);
        }
    }


}
