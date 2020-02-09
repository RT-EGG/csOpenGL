// System
using System.Collections.Generic;
// OpenTK
using OpenTK.Graphics.OpenGL4;

namespace rtOpenTK
{
    using EnableCap = OpenTK.Graphics.OpenGL.EnableCap;
    using EnableCap4 = OpenTK.Graphics.OpenGL4.EnableCap;

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
            p_Stack4.Push(new Dictionary<EnableCap4, bool>());
            return;
        }

        public void Pop()
        {
            if (p_Stack.Count == 1)
                throw new TGLStackUnderflow();

            foreach (var cap in p_Stack.Pop()) {
                if (cap.Value)
                    _Enable(cap.Key);
                else
                    _Disable(cap.Key);
            }

            foreach (var cap in p_Stack4.Pop()) {
                if (cap.Value)
                    _Enable(cap.Key);
                else
                    _Disable(cap.Key);
            }

            return;
        }

        public void Enable(EnableCap aCap)
        {
            Save(aCap);
            _Enable(aCap);
            return;
        }

        public void Enable(EnableCap4 aCap)
        {
            Save(aCap);
            _Enable(aCap);
            return;
        }

        public void Disable(EnableCap aCap)
        {
            Save(aCap);
            _Disable(aCap);
            return;
        }

        public void Disable(EnableCap4 aCap)
        {
            Save(aCap);
            _Disable(aCap);
            return;
        }

        private void Save(EnableCap aCap)
        {
            var peek = p_Stack.Peek();
            if (!peek.ContainsKey(aCap))
                peek.Add(aCap, OpenTK.Graphics.OpenGL.GL.IsEnabled(aCap));
            return;
        }

        private void Save(EnableCap4 aCap)
        {
            var peek = p_Stack4.Peek();
            if (!peek.ContainsKey(aCap))
                peek.Add(aCap, GL.IsEnabled(aCap));
            return;
        }

        private void _Enable(EnableCap aCap)
        {
            OpenTK.Graphics.OpenGL.GL.Enable(aCap);
            return;
        }

        private void _Enable(EnableCap4 aCap)
        {
            OpenTK.Graphics.OpenGL4.GL.Enable(aCap);
            return;
        }

        private void _Disable(EnableCap aCap)
        {
            OpenTK.Graphics.OpenGL.GL.Disable(aCap);
            return;
        }

        private void _Disable(EnableCap4 aCap)
        {
            OpenTK.Graphics.OpenGL4.GL.Disable(aCap);
            return;
        }

        private Stack<Dictionary<EnableCap, bool>> p_Stack = new Stack<Dictionary<EnableCap, bool>>();
        private Stack<Dictionary<EnableCap4, bool>> p_Stack4 = new Stack<Dictionary<EnableCap4, bool>>();
    }
}
