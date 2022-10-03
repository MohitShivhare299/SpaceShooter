using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    public int powerType;
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        MoveObject();
        DisablingObject();
    }
    private void MoveObject()
    {
        transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    public void DisablingObject()
    {
        if (transform.position.y < GameManager.screenBounds.y * -1 - 3f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            Destroy(gameObject);
            PlayerController.Instance.PowerUp(powerType);
        }
    }
}
