using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] liveSprites;
    [SerializeField]
    private Image _LivesImg;
    private Player Player;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartLevel;

    private GameManager _gameManager;
    


    // Start is called before the first frame update
    void Start()
    {
        _restartLevel.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if ( _gameManager == null ) {
            Debug.Log("Game manager is not working");
        }

    }


    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: "+ playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = liveSprites[currentLives];
       
        
        if(currentLives == 0)
        {
            
            GameOverSequence();
            
            
        }
    }
    
    public void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartLevel.gameObject.SetActive(true);
        StartCoroutine(GameOverTextFlickerRoutine());
        
    }

    IEnumerator GameOverTextFlickerRoutine()
    {
        while (true)
        {
            Debug.Log("ca marche");
            _gameOverText.gameObject.SetActive(true);
            _restartLevel.gameObject.SetActive(true);    
            yield return new WaitForSeconds(0.4f);
            _gameOverText.gameObject.SetActive(false);
            _restartLevel.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            
        }
    }

    

}
