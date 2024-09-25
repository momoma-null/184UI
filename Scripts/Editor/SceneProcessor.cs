
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

namespace Iwashi.UI
{
    sealed class SceneProcessor : IProcessSceneWithReport
    {
        int IOrderedCallback.callbackOrder => 0;

        void IProcessSceneWithReport.OnProcessScene(UnityEngine.SceneManagement.Scene scene, UnityEditor.Build.Reporting.BuildReport report)
        {
            var roots = scene.GetRootGameObjects();
            var preprocessors = new List<IPreprocessBehaviour>();
            foreach (var root in roots)
            {
                root.GetComponentsInChildren(true, preprocessors);
                foreach (var i in preprocessors)
                {
                    try
                    {
                        i.Process();
                        Debug.Log($"{i.GetType().Name} process done.({i})");
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        Object.DestroyImmediate(i as Component);
                    }
                }
            }
        }
    }
}
