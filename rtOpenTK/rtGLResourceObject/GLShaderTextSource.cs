// System
using System;
using System.IO;
using System.Collections.Generic;
// OpenTK
using OpenTK.Graphics.OpenGL4;

namespace rtOpenTK.rtGLResourceObject
{
    public class TGLShaderTextSource : TGLShaderSource
    {
        public static TGLShaderTextSource CreateFileSource(string aFilePath)
        {
            TGLShaderTextSource result = new TGLShaderTextSource();
            result.LoadFromFile(aFilePath);

            return result;    
        }

        public static TGLShaderTextSource CreateTextSource(string aText)
        {
            TGLShaderTextSource result = new TGLShaderTextSource();
            result.Text = aText;

            return result;
        }

        public static TGLShaderTextSource CreateTextSource(IEnumerable<string> aText)
        {
            TGLShaderTextSource result = new TGLShaderTextSource();
            result.LoadText(aText);

            return result;
        }

        public bool LoadFromFile(string aFilePath)
        {
            if (!File.Exists(aFilePath))
                return false;

            Uri pathcheck = null;
            if (!Uri.TryCreate(aFilePath, UriKind.Absolute, out pathcheck))
                aFilePath = Path.GetFullPath(aFilePath);

            StreamReader reader = new StreamReader(aFilePath);
            try {
                Text = reader.ReadToEnd();

            } finally {
                reader.Dispose();
            }
            return true;
        }

        public void LoadText(IEnumerable<string> aText)
        {
            Text = "";
            foreach (string item in aText)
                Text += item + Environment.NewLine;
            return;
        }

        protected override void DoLoad(TrtGLControl aGL, int aShaderID)
        {
            GL.ShaderSource(aShaderID, Text);
            return;
        }

        /* with "include"
        private List<string> LoadFromFile(string aFileName)
        {
            List<string> result = new List<string>();
            if (!File.Exists(aFileName)) {
                result.Add("not found file \"" + aFileName + "\"");
                return result;
            }

            StreamReader reader = new StreamReader(aFileName);
            try {
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();

                    int pos = line.IndexOf("#include");
                    if (pos != -1) {
                        pos += "#include".Length;
                        int begin = line.IndexOf("\"", pos);
                        int end = line.IndexOf("\"", begin + 1);

                        if ((begin == -1) || (end == -1)) {
                            result.Add("Invalid line.");
                        } else {
                            Uri directory = new Uri(Path.GetDirectoryName(aFileName) + "\\");
                            Uri incFileName = new Uri(directory, line.Substring(begin + 1, end - begin - 1));
                            string fullPath = incFileName.LocalPath;

                            result.AddRange(LoadFromFile(fullPath));
                        }

                    } else {
                        result.Add(line);
                    }
                }

            } finally {
                reader.Dispose();
            }

            return result;
        }
        */

        public string Text
        { get; set; }
    }
}
