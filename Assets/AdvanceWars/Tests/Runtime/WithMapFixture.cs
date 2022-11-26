using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace AdvanceWars.Tests.Runtime
{
    public abstract class WithMapFixture
    {
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            SceneManager.LoadScene("WalkingSkeleton");
            yield return null;
            Object.FindObjectOfType<Button>().onClick.Invoke();
            yield return null;
        }
    }
}