using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    private int lifeLeft;
    public int cubeOnScreen;
    [SerializeField] private TMP_Text scoreText;
    
    private Difficulty currentLevel;
    public Difficulty CurrentLevel => currentLevel;
    public bool CanGenerateCube => cubeOnScreen < currentLevel.maxCubeOnScreen;
    
    public int score = 0;
    private float coef;

    public int CalculateScore(Cube cube)
    {
        return Mathf.RoundToInt(data.point * data.pointCurve.Evaluate(cube.LifeTime) * coef);
    }

    private void FixedUpdate()
    {

        if (Time.time % 60 == 0)
            coef += data.coef;
    }
    private void Update()
    {
        scoreText.text = $"Score : {score}";

        if (Time.time - elapsedTime > currentLevel.duration)
        {
            elapsedTime = Time.time;
            if (levelIndex < data.difficulties.Length)
            {
                levelIndex++;
                currentLevel = data.difficulties[levelIndex];
            }          
        }
       
    }

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject menu;
    public void GameOver()
    {
        Time.timeScale = 0;
        menu.SetActive(false);
        gameOverScreen.SetActive(true);
    }
    public int Life
    {
        get => lifeLeft;
        set => lifeLeft = value;
    }

    private float elapsedTime = 0;
    private int levelIndex = 0;
    void Start()
    {
        lifeLeft = data.lifeNumber;
        //coef = data.coef;
        
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        currentLevel = data.difficulties[0];
    }
}
