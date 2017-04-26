using System;
using System.Collections.Generic;

namespace Chronicle.Log4Net
{
    public class DisposableStackItem : IDisposable
    {
        readonly Stack<object> _stack;

        public DisposableStackItem(Stack<object> stack, object state)
        {
            _stack = stack;
            stack.Push(state);
        }

        public void Dispose()
        {
            _stack.Pop();
        }
    }
}