using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public GameObject shotEnemy;

    private Vector3 horizontalMove = new Vector3(0.05f, 0, 0);
    private Vector3 verticalMove = new Vector3(0, 0.05f, 0);

    private const float minLeft = -2.5f;
    private const float maxLeft = 2.5f;
    private const float moveTime = 0.005f;
    private const float moveSpeed = -0.2f;

    [SerializeField] private float moveTimer = 0.01f;
    private Vector2 min;
    private Vector2 max;


    public static List<GameObject> allEnemies = new List<GameObject>();
    private bool movingRight;
    private void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            allEnemies.Add(go);
        }
    }

    private void Update()
    {
        if (moveTimer <= 0)
        {
            MoveEnemies();
        }
        moveTimer -= Time.deltaTime;
    }
    void MoveEnemies()
    {

        if (allEnemies.Count > 0)
        {
            int hitMax = 0;

            for (int i = 0; i < allEnemies.Count; i++)
            {
                if (movingRight)
                {
                    allEnemies[i].transform.position += horizontalMove;
                }

                else
                {
                    allEnemies[i].transform.position -= horizontalMove;
                   
                }


                if (allEnemies[i].transform.position.x > min.x || allEnemies[i].transform.position.x < max.x)
                {
                    hitMax++;
                }
                   
            }

            if (hitMax > 0)
            {
                for (int i = 0; i < allEnemies.Count; i++)
                    allEnemies[i].transform.position -= verticalMove;

                movingRight = !movingRight;
            }

            moveTimer = GetMoveSpeed();
        }
    }

    private float GetMoveSpeed()
    {
        float f = allEnemies.Count * moveTime;

        if (f < moveSpeed)
            return moveSpeed;
        else
            return f;
    }
}
