using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public IInput Input { get; set; }
    public MovementHandler(IInput input)
    {
        Input = input;

        Input.MoveLeft += MoveLeft;
        Input.MoveRight += MoveRight;
        Input.Jump += Jump;
        Input.SuperJump += SuperJump;
    }
    private void MoveLeft()
    {

    }
    private void MoveRight()
    {

    }
    private void Jump()
    {

    }
    private void SuperJump()
    {

    }
}
