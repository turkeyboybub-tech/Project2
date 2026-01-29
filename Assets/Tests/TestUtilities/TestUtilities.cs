using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StormFishingVessel.Tests
{
    public static class TestUtilities
    {
        public static IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        public static T FindObjectOfType<T>() where T : Object
        {
            return Object.FindObjectOfType<T>();
        }

        public static IEnumerator WaitForCondition(System.Func<bool> condition, float timeoutSeconds)
        {
            var timer = 0f;
            while (timer < timeoutSeconds)
            {
                if (condition())
                {
                    yield break;
                }
                timer += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        public static void SimulateActivate(GameObject target)
        {
            if (target == null)
            {
                return;
            }

            target.SendMessage("OnActivate", SendMessageOptions.DontRequireReceiver);
        }
    }
}
