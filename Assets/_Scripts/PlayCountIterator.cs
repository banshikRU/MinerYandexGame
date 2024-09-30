using System;
using UnityEngine;

public class PlayCountIterator : MonoBehaviour
{
    public static PlayCountIterator instance;
    [NonSerialized]public int _counter;
    private void Awake()
    {
        
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void CounterUpdater()
    {
        _counter++;
    }
}
