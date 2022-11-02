using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    float enemyIntervalMax = 2.0f;
    float enemyIntervalMin = 0.2f;
    float timeToMinimumInterval = 30.0f;

    public Camera MainCamera;
    public Text ScoreText;
    public Text GameOverText;
    public ShipController Ship;
    public GameObject EnemyPrefab;

    Vector3 topLeftBound;

    int score = 0;
    float gameTimer;
    float enemyTimer;
    bool gameOver;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        GameOverText.enabled = false;
        enemyTimer = enemyIntervalMax;
        topLeftBound = MainCamera.ViewportToWorldPoint(new Vector3(0, 1, -MainCamera.transform.localPosition.z));
        //rightBound = MainCamera.ViewportToWorldPoint(new Vector3(1, 0, -MainCamera.transform.localPosition.z));
        Ship.GetComponent<ShipController>().OnGameOver = OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        gameOverLogic();
        timerLogic();
    }

    void gameOverLogic()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            ScoreText.enabled = false;
            GameOverText.enabled = true;
            GameOverText.text = $"GameOver! Total Score: {score}\nPress R to restart!";
            return;
        }
    }

    void timerLogic()
    {
        gameTimer += Time.deltaTime;
        enemyTimer -= Time.deltaTime;

        if (enemyTimer <= 0)
        {
            float intervalPercentage = Mathf.Min(gameTimer / timeToMinimumInterval, 1);
            enemyTimer = enemyIntervalMax - (enemyIntervalMax - enemyIntervalMin) * intervalPercentage;
            GameObject enemy = GameObject.Instantiate<GameObject>(EnemyPrefab);
            enemy.transform.SetParent(this.transform);
            enemy.transform.position = new Vector3(Random.Range(topLeftBound.x, topLeftBound.y), topLeftBound.y + 2, 0);
            enemy.GetComponent<EnemyController>().OnKill += OnKillEnemy;
        }
    }

    public void OnKillEnemy()
    {
        score = score + 100;
        ScoreText.text = $"Score: {score}";
    }

    public void OnGameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
    }
}
