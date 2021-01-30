//#define PROFILING_SCENE_CHANGE
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using LDF.Core;
using LDF.Utility;

#if PROFILING_SCENE_CHANGE
using UnityEngine.Profiling;
#endif

namespace LDF.UserInterface
{
    [RequireComponent(typeof(CanvasGroupFlasher))]
    public class SceneBlender : Singleton<SceneBlender>
    {
        [Required, SerializeField]
        private CanvasGroupFlasher _screenFlasher;

        public event Action OnBeforeSceneUnloadsEvent;

        private const float _sceneUnloadThreshold = 0.9f;

        public static void ChangeScene(BuildScene scene, ScreenOrientation? orientationLock = null)
        {
            Instance.StartCoroutine(ChangeSceneCoroutine(scene, null, orientationLock));
        }

        public static IEnumerator ChangeSceneCoroutine(BuildScene scene, Action<float> onProgress, ScreenOrientation? orientationLock)
        {
#if UNITY_ANDROID || UNITY_IOS
            if (orientationLock != null)
            {
                Screen.orientation = orientationLock.Value;
                while (Screen.orientation != orientationLock.Value)
                {
                    yield return null;
                }
            }
#endif
            Instance.OnBeforeSceneUnloadsEvent?.Invoke();

#if PROFILING_SCENE_CHANGE
            var sceneLoadBench = new System.Diagnostics.Stopwatch();
            var memoryABefore = Profiler.GetTotalAllocatedMemoryLong();
            var memoryRBefore = Profiler.GetTotalReservedMemoryLong();

            sceneLoadBench.Start();
#endif
            var asyncLoad = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);
            asyncLoad.allowSceneActivation = false;
            var flashStarted = false;

            while (!asyncLoad.isDone)
            {
                onProgress?.Invoke(asyncLoad.progress);

                if (asyncLoad.progress >= _sceneUnloadThreshold && !flashStarted)
                {
                    Instance._screenFlasher.StartFlash(FinishLoadingScene);
                    flashStarted = true;

                    void FinishLoadingScene() => asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }

#if PROFILING_SCENE_CHANGE
            sceneLoadBench.Stop();

            var memoryAAfter = Profiler.GetTotalAllocatedMemoryLong();
            var memoryRAfter = Profiler.GetTotalReservedMemoryLong();
            Debug.Log($"====! SceneLoadBench: {sceneLoadBench.ElapsedMilliseconds} [ms]");
            Debug.Log($"====! Memory before scene change: Alloc: {memoryABefore} Res: {memoryRBefore}|| Memory after scene change: Alloc: {memoryAAfter} Res: {memoryRAfter}");
#endif
        }

#if UNITY_EDITOR
        [ContextMenu("Change Scene To \"0\"")]
        private void ChangeScene_EditorOnly() => ChangeScene(0);
#endif
    }
}
