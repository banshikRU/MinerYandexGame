
using System;
using UnityEngine;

public interface IInput
{
    public event Action MoveRight;
    public event Action MoveLeft;
    public event Action Jump;
    public event Action SuperJump;
}
