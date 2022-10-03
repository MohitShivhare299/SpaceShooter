using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObjectsSpwanner : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> powerObjects = new List<GameObject>();

    [SerializeField]
    private float timeBetweenPowerSpawn;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(StartSpawningPowerObjects());
    }


    private IEnumerator StartSpawningPowerObjects()
    {
        GameObject powerTemp = Instantiate(powerObjects[Random.Range(0,3)]);
        Vector3 randomPoint = new Vector3(Random.Range(GameManager.screenBounds.x * -1 + 0.5f, GameManager.screenBounds.x - 0.5f), GameManager.screenBounds.y + 1, 0);
        powerTemp.transform.position = randomPoint;
        powerTemp.transform.parent = null;
        powerTemp.SetActive(true);
        yield return new WaitForSeconds(timeBetweenPowerSpawn);
        StartCoroutine(StartSpawningPowerObjects());
    }
}
