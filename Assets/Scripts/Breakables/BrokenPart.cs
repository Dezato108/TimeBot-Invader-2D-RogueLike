using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPart : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    private Vector3 movementDirection;

    [SerializeField] float haltingFactor = 5f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float fadeSpeed = 1f;

    private SpriteRenderer partSprite;
    // Start is called before the first frame update
    void Start()
    {
        partSprite = GetComponent<SpriteRenderer>();
        movementDirection.x = Random.Range(-moveSpeed, moveSpeed);
        movementDirection.y = Random.Range(-moveSpeed, moveSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementDirection * Time.deltaTime;

        movementDirection = Vector3.Lerp(movementDirection, Vector3.zero, haltingFactor * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            partSprite.color = 
                new Color(
                    partSprite.color.r,
                    partSprite.color.g,
                    partSprite.color.b,
                    Mathf.MoveTowards(partSprite.color.a, 0f, fadeSpeed * Time.deltaTime)
                    );
            if (partSprite.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
