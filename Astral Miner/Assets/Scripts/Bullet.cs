using UnityEngine;

public class Bullet : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if(collision.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            //add to score/money

        }
    }





}
