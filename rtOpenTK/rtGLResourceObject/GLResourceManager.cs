using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtOpenTK.rtGLResourceObject
{
    internal class TGLResourceManager
    {
        public void Process(TrtGLControl aGL)
        {
            while (p_CreateQueue.Count > 0)
                p_CreateQueue.Dequeue().CreateGLResource(aGL);
            while (p_TaskQueue.Count > 0) {
                var task = p_TaskQueue.Dequeue();
                task.Key?.Invoke(aGL, task.Value);
            }
            while (p_DisposeQueue.Count > 0)
                p_DisposeQueue.Dequeue().DisposeGLResource(aGL);
            return;
        }

        public void EnqueueTask(TrtGLTask aTask, object aObject)
        {
            p_TaskQueue.Enqueue(new KeyValuePair<TrtGLTask, object>(aTask, aObject));
            return;
        }

        public void EnqueueCreateResourceObject(TGLResourceObject aObject)
        {
            p_CreateQueue.Enqueue(aObject);
            return;
        }

        public void EnqueueDisposeResourceObject(TGLResourceObject aObject)
        {
            p_DisposeQueue.Enqueue(aObject);
            return;
        }

        private Queue<KeyValuePair<TrtGLTask, object>> p_TaskQueue = new Queue<KeyValuePair<TrtGLTask, object>>();
        private Queue<TGLResourceObject> p_CreateQueue = new Queue<TGLResourceObject>();
        private Queue<TGLResourceObject> p_DisposeQueue = new Queue<TGLResourceObject>();
    }
}
