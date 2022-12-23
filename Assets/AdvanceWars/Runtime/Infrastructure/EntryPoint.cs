using System;
using AdvanceWars.Runtime.Application;
using UnityEngine;
using Zenject;

namespace AdvanceWars.Runtime.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private Gameplay gameplay;

        private void Start()
        {
            gameplay.Run();
        }
    }
}