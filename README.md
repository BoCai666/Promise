# 简介
一个参考UniTask而实现的极简的异步方案，也是对Async/Await背后原理学习的一个记录。
# Async/Await原理
Async/Await与IEnumerator/YieldReturn类似，其标识的方法内部都会被编译器生成一个状态机，两者的状态机运行的方法命名都一致都是MoveNext，只是前者无返回值，后者有个bool返回值。  

状态机内部通过int状态值来分割await/yieldreturn的代码段，执行状态机的MoveNext方法来逐步执行到所有被分割的代码段。

Async/Await生成的状态机MoveNext方法通过回调调回调的形式往下执行，而IEnumerator/YieldReturn生成的状态机MoveNext方法需在循环中一直检测返回值后往下执行。

# Async/Await示例
```csharp
// 源码
using System;
using System.Threading.Tasks;

public class C {
    public async void Test() {
        Console.Write("11");
        await Task.Delay(1);
        Console.Write("22");
    }
}
// 编译器生成代码
public class C
{
    [CompilerGenerated]
    private sealed class <Test>d__0 : IAsyncStateMachine
    {
        public int <>1__state;

        public AsyncVoidMethodBuilder <>t__builder;

        public C <>4__this;

        private TaskAwaiter <>u__1;

        private void MoveNext()
        {
            int num = <>1__state;
            try
            {
                TaskAwaiter awaiter;
                if (num != 0)
                {
                    Console.Write("11");
                    awaiter = Task.Delay(1).GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        num = (<>1__state = 0);
                        <>u__1 = awaiter;
                        <Test>d__0 stateMachine = this;
                        <>t__builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                        return;
                    }
                }
                else
                {
                    awaiter = <>u__1;
                    <>u__1 = default(TaskAwaiter);
                    num = (<>1__state = -1);
                }
                awaiter.GetResult();
                Console.Write("22");
            }
            catch (Exception exception)
            {
                <>1__state = -2;
                <>t__builder.SetException(exception);
                return;
            }
            <>1__state = -2;
            <>t__builder.SetResult();
        }

        void IAsyncStateMachine.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            this.MoveNext();
        }

        [DebuggerHidden]
        private void SetStateMachine([Nullable(1)] IAsyncStateMachine stateMachine)
        {
        }

        void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine stateMachine)
        {
            //ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
            this.SetStateMachine(stateMachine);
        }
    }

    [AsyncStateMachine(typeof(<Test>d__0))]
    [DebuggerStepThrough]
    public void Test()
    {
        <Test>d__0 stateMachine = new <Test>d__0();
        stateMachine.<>t__builder = AsyncVoidMethodBuilder.Create();
        stateMachine.<>4__this = this;
        stateMachine.<>1__state = -1;
        stateMachine.<>t__builder.Start(ref stateMachine);
    }
}
```
# 推荐链接
- [UniTask](https://github.com/Cysharp/UniTask)：一个为Unity提供的高性能，0GC的async/await异步方案
- [ETTask](https://github.com/egametang/ET)：ET框架中的async/await异步方案
- [SharpLab](https://sharplab.io/)：一个快速显示.net代码（如c#）的编译中间过程和结果的网站

