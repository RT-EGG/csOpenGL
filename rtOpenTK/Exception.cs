using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtOpenTK
{
    public class TGLException : Exception
    {

    }

    public class TGLStackOverflow : TGLException
    {
    }

    public class TGLStackUnderflow : TGLException
    {

    }
}
