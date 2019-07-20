// System
using System;
using System.Collections.Generic;
// OpenTK
using OpenTK.Graphics.OpenGL4;
// rtUtility
using rtUtility.Collections;

namespace rtOpenTK
{
    public enum TGLAttribGroup
    {
        EnableAttrib = 1,
        AllAttrib = 0xFFFF
    }

    public class TGLAttribStack
    {
        public TGLAttribStack()
        {
            groups.Add(new KeyValuePair<TGLAttribGroup, IGLAttribGroupStack>(TGLAttribGroup.EnableAttrib, EnableGroup));
            return;
        }

        public void PushAttrib(TGLAttribGroup aAttrib)
        {
            foreach (var group in groups) {
                if ((aAttrib & group.Key) != 0)
                    group.Value.Push();
            }
            return;
        }

        public void PopAttrib(TGLAttribGroup aAttrib)
        {
            foreach (var group in groups) {
                if ((aAttrib & group.Key) != 0)
                    group.Value.Pop();
            }
            return;
        }

        public TGLEnableAttribStack EnableGroup
        { get; } = new TGLEnableAttribStack();

        public IGLAttribGroupStack GetAttribGroupStack(TGLAttribGroup aAttrib)
        {
            switch (aAttrib) {
                case TGLAttribGroup.EnableAttrib:
                    return EnableGroup;
            }
            return null;
        }

        private IList<KeyValuePair<TGLAttribGroup, IGLAttribGroupStack>> groups = new List<KeyValuePair<TGLAttribGroup, IGLAttribGroupStack>>();
    }

    public interface IGLAttribGroupStack
    {
        void Push();
        void Pop();
    }

    public class TGLEnableAttribStack : IGLAttribGroupStack
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
