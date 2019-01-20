// System
using System;

namespace rtUtility
{
    public class TDisposableObject : IDisposable
    {
        ~TDisposableObject()
        {
            Dispose(false);
            return;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return;
        }

        protected virtual void Dispose(bool aDisposing)
        {
            return;
        }
    }
}
