using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 bulletDirection;
    [SerializeField] int bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        bulletDirection = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletDirection * speed * Time.deltaTime;

        if (!FindObjectOfType<BossController>().gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))        
            collision.GetComponent<PlayerHealthHandler>().DamagePlayer(bulletDamage);
        
        Destroy(gameObject);
    }
}
