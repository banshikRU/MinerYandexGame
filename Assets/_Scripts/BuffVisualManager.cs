using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CreateNewBuff:UnityEvent<GameObject> { }
public class BuffVisualManager : MonoBehaviour
{
    public static CreateNewBuff buffCreate = new CreateNewBuff();
    private void Awake()
    {
        buffCreate.AddListener(CreateNewBuff);
    }
    private void CreateNewBuff(GameObject buff)
    {
        Instantiate(buff, gameObject.transform);
    }

}
