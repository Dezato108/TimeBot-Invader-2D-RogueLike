using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    private Rigidbody2D bulletRigidBody;

    [SerializeField] GameObject bulletImpactEffect;
    [SerializeField] GameObject[] damageEffects;
    

    [SerializeField] int damageAmount = 10;
    



    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidBody.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.CompareTag("Enemy"))
        {
            InstantiateBloodEffect();

            collision.GetComponent<EnemyController>().DamageEnemy(damageAmount);
        }
        else if (collision.CompareTag("Boss"))
        {
            InstantiateBloodEffect();
            collision.GetComponent<BossHealthHandler>().TakeDamage(damageAmount);
        }
        else
        {
            Instantiate(bulletImpactEffect.transform, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    private void InstantiateBloodEffect()
    {
        int selectedSplatter = Random.Range(0, damageEffects.Length);
        Instantiate(damageEffects[selectedSplatter], transform.position, transform.rotation);
    }
}
