using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _firerate = 0.5f;
    private float _canfire = -1;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private float _speedPowerup = 8.5f;
    [SerializeField]
    private bool _isSpeedPowerupActive = false;
    [SerializeField]
    private bool _isShieldPowerupActive = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]    
    public int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _rightThruster;
    [SerializeField]
    private GameObject _leftThruster;
    [SerializeField]
    private AudioClip _laserSFX;
    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //take current pos = new pos (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _rightThruster.SetActive(false);
        _leftThruster.SetActive(false);
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        _shield.SetActive(false);
        if (_uiManager == null)
        {
            Debug.Log("uimanager is null");
        }
        if(_audioSource == null)
        {
            Debug.Log("audio source on player is null");
        }
        else
        {
            _audioSource.clip = _laserSFX;
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            fire();
        }
            
    }

    void fire()
    {
        _canfire = Time.time + _firerate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
        }else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        
        _audioSource.Play();
      
    }

    void calculateMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");

        float horizontalInput = Input.GetAxis("Horizontal");

        if (_isSpeedPowerupActive == false)
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * _speedPowerup * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speedPowerup * Time.deltaTime);
        } 
        

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11.3)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage() 
    {
        if (_isShieldPowerupActive == true)
        {
            _isShieldPowerupActive = false;
            _shield.SetActive(false);
            return;
        }
        
        _lives -= 1;

        if(_lives == 2)
        {
            _rightThruster.SetActive(true);
        }else if(_lives == 1)
        {
            _leftThruster.SetActive(true);
        }

        
        if(_lives < 1)
        {
            Destroy(this.gameObject);
            //UIManager uIManager = GetComponent<UIManager>();
            _spawnManager.OnPlayerDeath();
            
                
            
        }
        _uiManager.UpdateLives(_lives);

    }
    public void SpeedPowerupActive()
    {
        StartCoroutine(SpeedPowerupPowerDown());
    }
    IEnumerator SpeedPowerupPowerDown(){
        _isSpeedPowerupActive = true;
        yield return new WaitForSeconds(5);
        _isSpeedPowerupActive = false;
    }

    public void TripleShotActive()
    {
        StartCoroutine(TripleShotPowerDownRoutine());   
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
         _isTripleShotActive = true;
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void ShieldsActive()
    {
        _shield.SetActive(true);
        _isShieldPowerupActive = true;
    }
    
    public void AddToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}