using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float mMovementSpeed;
    public bool bIsGoingRight;
    void Start()
    {
        mMovementSpeed = Random.Range(1, 6);
        int a = Random.Range(0, 3);
        if (a == 0)
        {
            bIsGoingRight = false;
        }
        else bIsGoingRight = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CloudsWall")
        {
            bIsGoingRight = !bIsGoingRight;
            RestartMovementSpeed();
        }
    }
    private void Update()
    {
        Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * mMovementSpeed;
        transform.Translate(directionTranslation);
    }
    public void RestartMovementSpeed()
    {
        mMovementSpeed = Random.Range(2, 5);
    }

}
