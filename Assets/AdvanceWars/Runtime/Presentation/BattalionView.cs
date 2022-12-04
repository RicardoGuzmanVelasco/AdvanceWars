using System;
using UnityEngine;

namespace AdvanceWars.Runtime.Presentation
{
    public class BattalionView : MonoBehaviour
    {
        Color normalColor;

        public bool Tired => GetComponent<SpriteRenderer>().color != normalColor;
        void Start()
        {
            normalColor = GetComponent<SpriteRenderer>().color;
        }

        public void ShowTired()
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }

        public void ShowFresh()
        {
            GetComponent<SpriteRenderer>().color = normalColor;
        }
    }
}