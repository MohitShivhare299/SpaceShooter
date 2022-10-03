using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField]
    private int numberOfEnemiesInStart;
    [SerializeField]
    private int numberOfEnemiesObjectsInStart = 10;

    [SerializeField]
    private float timeBetweenEnemySpawn;

    [SerializeField]
    private List<GameObject> enemyPrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> enemyObjects = new List<GameObject>();

    [SerializeField]
    private GameObject bossPrefab;

    [SerializeField]
    private Transform poolContainer;

    private Stack<GameObject> enemyPool = new Stack<GameObject>();


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

    // Start is called before the first frame update
    void Start()
    {
        CreateEnemyPool();
        StartCoroutine(StartSpawningEnemies());
    }

    private void CreateEnemyPool()
    {
        for (int i = 0; i < numberOfEnemiesInStart; i++)
        {
            GameObject enemyTemp = Instantiate(enemyPrefabs[Random.Range(0, 3)], poolContainer);
            enemyTemp.transform.position = Vector3.zero;
            enemyTemp.SetActive(false);

            enemyPool.Push(enemyTemp);

            if(i%5==0)
            {
                GameObject enemyObjectTemp = Instantiate(enemyObjects[Random.Range(0, 3)], poolContainer);
                enemyObjectTemp.transform.position = Vector3.zero;
                enemyObjectTemp.SetActive(false);
                enemyPool.Push(enemyObjectTemp);
            }
        }
    }

    private IEnumerator StartSpawningEnemies()
    {
        if (enemyPool.Count > 0)
        {
            float width = enemyPool.Peek().GetComponent<Collider2D>().bounds.extents.x;
            Vector3 randomPoint =new Vector3(Random.Range(GameManager.screenBounds.x * -1 + 0.5f,GameManager.screenBounds.x - 0.5f), GameManager.screenBounds.y+1,0);
            GameObject enemyTemp = enemyPool.Pop();
            enemyTemp.transform.position = randomPoint;
            enemyTemp.transform.parent = null;

            enemyTemp.SetActive(true);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
            StartCoroutine(StartSpawningEnemies());
        }
        else
        {
            StartBossMode();
            yield return null;
        }
    }

    private void StartBossMode()
    {
        GameObject boss = Instantiate(bossPrefab);
        boss.transform.localPosition = new Vector3(GameManager.screenBounds.x, GameManager.screenBounds.y - 1f, 0);
    }
}
