using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [Space]
    [SerializeField]
    private GameObject enemyBulletPrefab;
    [SerializeField]
    private Transform firePoint;

    [Space]
    [SerializeField]
    private float fireRate;

    private float timePassedFromLastFire;
    private bool moveRight;

    private float health = 50;
    void Update()
    {
        MoveEnemy();

        timePassedFromLastFire += Time.deltaTime;

        if (timePassedFromLastFire > fireRate)
        {
            ShootBullet();
            timePassedFromLastFire = 0;
        }
    }

    private void MoveEnemy()
    {
        float width = GetComponent<Collider2D>().bounds.extents.x;
        if (moveRight)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime,0, 0);
            if (transform.position.x > GameManager.screenBounds.x - width)
                moveRight = false;
        }
        else if (!moveRight)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x < - GameManager.screenBounds.x + width)
                moveRight = true;
        }
    }

    private void ShootBullet()
    {
        GameObject bulletTemp = Instantiate(enemyBulletPrefab, firePoint);
        bulletTemp.transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            Destroy(collision.gameObject);
            if(health <=0)
            {
                UIManager.Instance.GameWin();
            }
        }
    }
}
