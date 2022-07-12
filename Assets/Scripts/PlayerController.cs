using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosionGO;
    [SerializeField] private TextMeshProUGUI livesUITExt;
    [SerializeField] private AudioClip fireAudio;
    [SerializeField] private int maxLives;

    private AudioSource playerAudio;
    private int lives;
   
    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
       
    }

    public void Init()
    {
        gameObject.SetActive(true);
        lives = maxLives;
        livesUITExt.text = lives.ToString();
        transform.position = new Vector2(0, -4);
    }


   
    void Update()
    {        
        Move();       
        Shoot();
    }
    
    void Move() 
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            
            float x = Input.GetAxis("Horizontal");

            Vector2 direction = new Vector2(x, 0).normalized;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            max.x = max.x - 0.235f;
            min.x = min.x + 0.235f;

            Vector2 pos = transform.position;

            pos += direction * speed * Time.deltaTime;

            pos.x = Mathf.Clamp(pos.x, min.x, max.x);

            transform.position = pos;

        }
       
    }

    void Shoot() 
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
         
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); 
                pooledProjectile.transform.position = transform.position; 
                playerAudio.PlayOneShot(fireAudio, 1f);          
            }
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            lives--;
            livesUITExt.text = lives.ToString();
            

            if (lives == 0)
            {
                PlayExplosion(true);
                gameObject.SetActive(false);  
                GameManager.instance.SetGameManagerState(GameManager.GameManagerState.GameOver);

            }
            else 
            {
                PlayExplosion(false);
            }
            
               
        }
    }

    void PlayExplosion(bool dead)
    {
        GameObject explosion = Instantiate(explosionGO);
        if (dead)
        {
            explosion.transform.position = transform.position;
        }
        else 
        {
            explosion.transform.SetParent(this.transform);
            explosion.transform.position = transform.position;
        }
        
    }

   

}
