using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject explosionGO;
    [SerializeField] private int enemyLives;
    [SerializeField] private int enemyPoints;
    [SerializeField] private AudioClip enemyShootShound;

    private float speed;
    private float timer;
    private float timeToMove;
    private int movementLimits;
    private int levelMovementLimits;
    private GameScore gameScore;
    private AudioSource playEnemyShootSound;
  
    private void Awake()
    {
       
    }
    void Start()
    {
        gameScore = FindObjectOfType<GameScore>();
        playEnemyShootSound = GetComponent<AudioSource>();
        speed = 5;
        timer = 0;
        timeToMove = 0.5f;
        movementLimits = 0;
        SetEnemyLevel();

    }

    void SetEnemyLevel()
    {
        switch (GameManager.instance.levelNumber)
        {
            case 0:
                InvokeRepeating("ShootEnemy", Random.Range(1, 20), 7);
                speed = speed / 10;
                levelMovementLimits = 18;
                break;

            case 1:        
                InvokeRepeating("ShootEnemy", Random.Range(1, 25), 5);
                speed = (speed * 2) / 10;
                levelMovementLimits = 9;

                break;


            case 2:
                InvokeRepeating("ShootEnemy", Random.Range(1, 25), 5);
                speed = (speed) / 10;
                levelMovementLimits = 6;
                break;


        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();       
    }

  
    void MoveEnemy()
    {
       
        timer += Time.deltaTime;
        if (timer > timeToMove && movementLimits < levelMovementLimits)
        {
            transform.Translate(new Vector3(speed, 0, 0));
            timer = 0;
            movementLimits++;
        }

        if (movementLimits == levelMovementLimits)
        {
            transform.Translate(new Vector3(0, -.5f, 0));
            movementLimits = 0;
            speed = -speed;
            timer = 0;
        }
    }
    
    public void ShootEnemy()
    {
        GameObject bullet = Instantiate(enemyBullet);
        bullet.transform.position = transform.position;
        bullet.GetComponent<EnemyBullet>().isSet = true; 
        playEnemyShootSound.PlayOneShot(enemyShootShound, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBullet"))
        {
            enemyLives--;
            if (enemyLives == 0)
            {
                DestroyAllEnemies();
                PlayExplosion();
                gameScore.Score += enemyPoints;
                Destroy(gameObject);
                collision.gameObject.SetActive(false);                
            }
            else 
            {
                collision.gameObject.SetActive(false);
                GetComponent<SimpleFlash>().Flash();
            }
          
        }
        if (collision.transform.CompareTag("Player"))
        {
            PlayExplosion();
            Destroy(gameObject);
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = Instantiate(explosionGO);
        explosion.transform.position = transform.position;
    }

    void DestroyAllEnemies()
    {
        GameManager.instance.enemiesNumber--;
        if (GameManager.instance.enemiesNumber == 0)
        {
            GameManager.instance.SetGameManagerState(GameManager.GameManagerState.NextLevel);
        }
    }


}
