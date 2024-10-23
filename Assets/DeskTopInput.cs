using UnityEngine;
using System;
public class DeskTopInput :MonoBehaviour, IInput
{
    public event Action MoveRight;
    public event Action MoveLeft;
    public event Action Jump;
    public event Action SuperJump;
    public void IsJump()
    {
        Debug.Log("a");
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump.Invoke();
        }
    }
    public void Update()
    {
        IsJump();
    }
    public void IsMoveLeft()
    {
        throw new System.NotImplementedException();
    }

    public void IsMoveRight()
    {
        throw new System.NotImplementedException();
    }

    public void IsSuperJump()
    {
        throw new System.NotImplementedException();
    }
}
