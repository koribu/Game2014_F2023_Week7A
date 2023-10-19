using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;
    int _previousScore = 0;
    [SerializeField]
    int score = 0;

    [SerializeField]
    [Range(0, 5)]
    int _enemyNumber;

    GameObject _enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        for(int i = 0; i < _enemyNumber; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            
        }
    }



    public void ChangeSceneToGamePlay()
    {
        SceneManager.LoadScene("Main");
    }

    public void ChangeScore(int scoreIncrease)
    {
        score += scoreIncrease;
        UpdateScore();
    }

    void UpdateScore()
    {
        _scoreText.text = "Score: " + score;
    }
}
