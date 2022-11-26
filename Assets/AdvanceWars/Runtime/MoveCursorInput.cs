using AdvanceWars.Runtime.Application;
using UnityEngine;
using Zenject;
using static UnityEngine.Vector2Int;

public class MoveCursorInput : MonoBehaviour
{
    [Inject] CursorController controller;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            Upwards();

        if(Input.GetKeyDown(KeyCode.DownArrow))
            Downwards();

        if(Input.GetKeyDown(KeyCode.LeftArrow))
            Leftwards();

        if(Input.GetKeyDown(KeyCode.RightArrow))
            Rightwards();
    }

    public void Upwards()
    {
        controller.Towards(up);
    }

    public void Downwards()
    {
        controller.Towards(down);
    }

    public void Leftwards()
    {
        controller.Towards(left);
    }

    public void Rightwards()
    {
        controller.Towards(right);
    }
}