using AdvanceWars.Runtime.Application;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DrawMapInput : MonoBehaviour
{
    [Inject] DrawMap controller;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(async () => { await controller.Run(); });
    }
}