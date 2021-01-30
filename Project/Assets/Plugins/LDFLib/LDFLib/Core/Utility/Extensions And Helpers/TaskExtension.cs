using System.Collections;
using System.Threading.Tasks;

namespace LDF.Core
{
    public static class TaskExtensions
    {
        public static IEnumerator AsIEnumerator(this Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        }

        /// <summary>
        /// Checks is the task in progress.
        /// </summary>
        /// <returns><c>true</c>, if task in progress was ised, <c>false</c> otherwise.</returns>
        /// <param name="t">T.</param>
        public static bool IsTaskInProgress(this Task t)
        {
            return !(t.IsCompleted || t.IsFaulted || t.IsCanceled);
        }

        /// <summary>
        /// Checks is the task in progress.
        /// </summary>
        /// <returns><c>true</c>, if task in progress was ised, <c>false</c> otherwise.</returns>
        /// <param name="t">T.</param>
        public static bool IsTaskInProgress<T>(this Task<T> t)
        {
            return !(t.IsCompleted || t.IsFaulted || t.IsCanceled);
        }
    }
}