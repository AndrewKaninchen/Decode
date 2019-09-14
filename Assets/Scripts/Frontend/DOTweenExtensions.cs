using System.Threading.Tasks;
using DG.Tweening;

namespace Decode
{
    // ReSharper disable once InconsistentNaming
    public static class DOTweenExtensions
    {
        public static async Task<T> IsComplete<T>(this T t) where T : Tween 
        {
            var completionSource = new TaskCompletionSource<T>();
            t.OnComplete(() => completionSource.SetResult(t));
            return await completionSource.Task;
        }
    }
}