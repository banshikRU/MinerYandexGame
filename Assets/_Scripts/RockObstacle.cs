using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name =="Player")
        {
            HeartManager.hitMeInstance.Invoke(1,true);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
