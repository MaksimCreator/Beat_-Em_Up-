using UniRx;
using System;

public static class Timer
{
    public static void StartTimer(float cooldown, Action onEnd)
    {
        CompositeDisposable disposables = new CompositeDisposable();

        Observable.Timer(TimeSpan.FromSeconds(cooldown))
        .Subscribe(_ =>
        {
            onEnd?.Invoke();
            disposables.Dispose();
        }).AddTo(disposables);
    }
}
