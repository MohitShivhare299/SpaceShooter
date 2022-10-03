using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float fireRate;

    private float timePassedFromLastFire;

    public static PlayerController Instance;

    private bool playerDestroyed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        timePassedFromLastFire += Time.deltaTime;

        if(gameObject.GetComponent<SpriteRenderer>().enabled == true)
        {
            HandlePlayerMovement();
            if (Input.GetKey(KeyCode.Space) && !playerDestroyed)
            {
                if (timePassedFromLastFire > fireRate)
                {
                    ShootBullet();
                    timePassedFromLastFire = 0;
                }
            }
        }
    }

    private void HandlePlayerMovement()
    {
        float inputX = Input.GetAxis("Vertical");
        float inputY = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveSpeed * inputX, -moveSpeed * inputY, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);

        float width = GetComponent<Collider2D>().bounds.extents.x;
        float height = GetComponent<Collider2D>().bounds.extents.y;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, GameManager.screenBounds.x * -1 + width, GameManager.screenBounds.x - width),
                                        Mathf.Clamp(transform.position.y, GameManager.screenBounds.y * -1 + height, GameManager.screenBounds.y - height), 0);
    }

    private void ShootBullet()
    {
        GameObject bulletTemp = Instantiate(bulletPrefab, firePoint);
        bulletTemp.transform.parent = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Enemy") || collision.gameObject.tag.Contains("AlienBullet"))
        {
            GameManager.Instance.OnPlayerHit();
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerDelay());
    }

    public IEnumerator RespawnPlayerDelay()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        playerDestroyed = true;

        yield return new WaitForSeconds(0.5f);
        tmp.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        fireRate = 0.2f;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        playerDestroyed = false;

        yield return new WaitForSeconds(1f);
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void PowerUp(int i)
    {
        if(i==0)
        {
            StartCoroutine(BlockShooting());
        }
        else if(i == 1)
        {
            StartCoroutine(IncreaseFireRate());
        }
        else if (i == 2)
        {
            StartCoroutine(Sheild());
        }
    }

    public IEnumerator BlockShooting()
    {
        playerDestroyed = true;
        yield return new WaitForSeconds(4f);
        playerDestroyed = false;
    }

    public IEnumerator IncreaseFireRate()
    {
        fireRate = 0.05f;
        yield return new WaitForSeconds(4f);
        fireRate = 0.2f;
    }

    public IEnumerator Sheild()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(4f);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
    }
}
