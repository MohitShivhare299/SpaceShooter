using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletShootSpeed;


    // Update is called once per frame
    void Update()
    {
        ShootBullet();
        DisablingBullet();
    }

    private void ShootBullet()
    {
        transform.position += new Vector3(0,bulletShootSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Contains("Enemy"))
        {
            GameManager.Instance.OnEnemyKilled();
            ScoreManager.Instance.UpdateScore(10);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Boss")
        {
            ScoreManager.Instance.UpdateScore(100);
        }
    }

    public void DisablingBullet()
    {
        if (transform.position.y > GameManager.screenBounds.y + 3f)
        {
            Destroy(gameObject);
        }
    }
}
