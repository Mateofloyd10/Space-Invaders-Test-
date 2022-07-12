using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed;
    public bool isMoving;
    
    private Vector2 min;
    private Vector2 max;
    private void Awake()
    {
        isMoving = false;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

         max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y + 2f;
         min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y - 2f;

    }
    void Start()
    {
        
    }


    void Update()
    {
        if (!isMoving)
            return;

        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;

        if (transform.position.y < min.y)
        {
            isMoving = false;
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}
