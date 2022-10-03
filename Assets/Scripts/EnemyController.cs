using UnityEngine;

public class EnemyController : MonoBehaviour
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

    void Update()
    {
        MoveEnemy();
        DisablingEnemy();
        timePassedFromLastFire += Time.deltaTime;

        if (timePassedFromLastFire > fireRate && gameObject.tag != "EnemyObjects")
        {
            ShootBullet();
            timePassedFromLastFire = 0;
        }
    }

    private void MoveEnemy()
    {
        transform.position -= new Vector3(0,moveSpeed * Time.deltaTime, 0);
    }

    public void DisablingEnemy()
    {
        if(transform.position.y < GameManager.screenBounds.y * -1 - 3f)
        {
            Destroy(gameObject);
        }
    }

    private void ShootBullet()
    {
        GameObject bulletTemp = Instantiate(enemyBulletPrefab, firePoint);
        bulletTemp.transform.parent = null;
    }

    
}
