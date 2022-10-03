using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletShootSpeed;


    // Update is called once per frame
    void Update()
    {
        ShootBullet();
        DisableBullet();
    }

    private void ShootBullet()
    {
        transform.position -= new Vector3(0,bulletShootSpeed * Time.deltaTime,0);
    }

    public void DisableBullet()
    {
        if (transform.position.y < GameManager.screenBounds.y * -1 - 3f)
        {
            Destroy(gameObject);
        }
    }
}
