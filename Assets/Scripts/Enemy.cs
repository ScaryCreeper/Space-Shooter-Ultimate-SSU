using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _laser;
    private Player _player;
    private Animator _animator;
    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null )
        {
            Debug.Log("player is null");
        }
        _animator = GetComponent<Animator>();  
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down* _speed * Time.deltaTime );  
        
        if (transform.position.y < -5f) { 

            float randomXAxis = Random.Range( -9.5f, 9.5f );

            transform.position =new Vector3(randomXAxis, 7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null )
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.6f);
        }
        if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddToScore(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.6f);
        }


    }


}
