using System;

namespace Minalear
{
    public class ShaderCompilationException : Exception
    {
        public ShaderCompilationException() : base() { }
        public ShaderCompilationException(string msg) : base(msg) { }
        public ShaderCompilationException(string msg, Exception inner) : base(msg, inner) { }
    }
}
