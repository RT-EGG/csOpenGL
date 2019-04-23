// System
using System.Collections.Generic;
// OpenTK
using OpenTK.Graphics.OpenGL4;

namespace rtOpenTK
{
    public class TGLAttribStack
    {
        public TGLEnableAttribStack EnableBit
        { get; } = new TGLEnableAttribStack();
    }

    public class TGLEnableAttribStack
    {
        public TGLEnableAttribStack()
        {
            Push();
            return;
        }

        public void Push()
        {
            p_Stack.Push(new Dictionary<EnableCap, bool>());
            return;
        }

        public void Pop()
        {
            if (p_Stack.Count == 1)
                throw new TGLStackUnderflow();

            foreach (KeyValuePair<EnableCap, bool> cap in p_Stack.Pop()) {
                if (cap.Value)
                    GL.Enable(cap.Key);
                else
                    GL.Disable(cap.Key);
            }

            return;
        }

        public void Enable(EnableCap aCap)
        {
            Save(aCap);
            GL.Enable(aCap);
            return;
        }

        public void Disable(EnableCap aCap)
        {
            Save(aCap);
            GL.Disable(aCap);
            return;
        }

        private void Save(EnableCap aCap)
        {
            var peek = p_Stack.Peek();
            if (!peek.ContainsKey(aCap))
                peek.Add(aCap, GL.IsEnabled(aCap));
            return;
        }

        private Stack<Dictionary<EnableCap, bool>> p_Stack = new Stack<Dictionary<EnableCap, bool>>();
    }
}
