using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText, HighScoreText;
    public GameObject GameOverText, EnterNameText;
    
    private bool m_Started = false;
    private int m_Points, highScore;
    
    private bool m_GameOver = false;
    private string playerName;
    private GameObject inputStore;


    private void Awake()
    {
        string path = Application.persistentDataPath + "/savefile.Json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            HighScoreText.text = "Best Score: " + data.playerName + " :" + data.highScore;
            highScore = data.highScore;

        }

       
        inputStore = GameObject.Find("InputStore");
       playerName = inputStore.GetComponent<TitleScreenManager>().currentPlayername;


        }




    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }


    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string playerName;


    }

    public void LoadData()
    {




    }


    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if ( m_Points > highScore)
            {
               EnterNameText.SetActive(true);
                UpdateHighScore();
            }

                GameOverText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                Destroy(inputStore);
                    SceneManager.LoadScene(0);
                }
            
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
       
    }

    public void UpdateHighScore()
    {
        SaveData data = new SaveData();

        data.highScore = m_Points;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);


      

    }


}
