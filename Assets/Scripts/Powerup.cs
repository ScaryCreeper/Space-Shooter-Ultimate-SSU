using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private AudioSource _audioSourcePickUp;
    [SerializeField]
    private AudioClip _audioClipPickUp;
    
  

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(0, -1, 0) *_speed* Time.deltaTime);
        if(transform.position.y < -6f)
        {
            Object.Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null) 
            {

                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive(); break;
                        case 1:
                        player.SpeedPowerupActive(); break;
                        case 2:
                        player.ShieldsActive(); break;
                    default:
                        Debug.Log("default"); break; 
                }

            }
            _audioSourcePickUp.clip = _audioClipPickUp;
            _audioSourcePickUp.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _speed = 0;
            Object.Destroy(this.gameObject,1f);
        }
    }

}
