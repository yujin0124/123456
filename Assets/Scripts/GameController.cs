using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Hazard;
    public Vector3 SpawnValues;
    public int HazardCount;
    public float SpawnWait;
    public float StartWait;
    public float WaveWait;

    public GUIText ScoreText;
    public GUIText RestartText;
    public GUIText GameOverText;

    private bool _gameOver;
    private bool _restart;
    private int _score;

    private void Start()
    {
        _gameOver = false;
        _restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        _score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    private void Update()
    {
        if (_restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel (Application.loadedLevel);
            }
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(StartWait);
        while (true)
        {
            for (int i = 0; i < HazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(Hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(SpawnWait);
            }
            yield return new WaitForSeconds(WaveWait);

            if (_gameOver)
            {
                RestartText.text = "Press 'R' for Restart";
                _restart = true;
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
    {
        _score += newScoreValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        ScoreText.text = "Score:" + _score;
    }
    public void GameOver()
    {
        GameOverText.text = "Game Over!";
        _gameOver = true;
    }
}
