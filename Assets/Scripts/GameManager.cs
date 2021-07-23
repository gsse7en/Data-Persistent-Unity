using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI bestScore;
    public static GameManager Instance;
    public SaveData highScore;
    public string name;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetBestScore();
    }
    public void SetBestScore()
    {
        if (highScore != null)
        {
            bestScore.text = "Best Score: " + highScore.name + ": " + highScore.score;
        } else {
            bestScore.text = "Best Score: ";
        }
    }
    public void SetHighScore(string name, int score)
    {
        highScore.name = name;
        highScore.score = score;
        SetBestScore();
        SaveHighScore();
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable] public class SaveData
    {
        public string name;
        public int score;
    }
    public void SaveName(string nameInput)
    {
        name = nameInput;
    }
    public void SaveScore(int score)
    {
        highScore.score = score;
        Debug.Log(score);
        SetBestScore();
        SaveHighScore();
    }
    public void SaveHighScore()
    {
        SaveData data = highScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScore = JsonUtility.FromJson<SaveData>(json);
            Debug.Log(highScore);
        }
    }
    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
