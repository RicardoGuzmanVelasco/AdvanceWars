using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace AdvanceWars.Tests.Runtime
{
    public abstract class WithMapFixture
    {
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            SceneManager.LoadScene("WalkingSkeleton");
            yield return null;
            yield return null;
        }
    }
}