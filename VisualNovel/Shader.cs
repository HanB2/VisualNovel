﻿using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Minalear
{
    public class Shader : IDisposable
    {
        private int shaderProgram;

        public Shader()
        {
            shaderProgram = GL.CreateProgram();
        }

        public void LoadSource(ShaderType type, string @source)
        {
            int shaderID = GL.CreateShader(type);
            GL.ShaderSource(shaderID, source);
            GL.CompileShader(shaderID);

            checkCompStatus(shaderID, type);

            GL.AttachShader(shaderProgram, shaderID);
            GL.DeleteShader(shaderID);
        }
        public void LinkProgram()
        {
            GL.LinkProgram(shaderProgram);
        }
        public void UseProgram()
        {
            GL.UseProgram(shaderProgram);
        }
        public void Dispose()
        {
            GL.DeleteProgram(shaderProgram);
        }

        private void checkCompStatus(int id, ShaderType type)
        {
            int compileStatus = 0;
            GL.GetShader(id, ShaderParameter.CompileStatus, out compileStatus);

            if (compileStatus == COMPILATION_ERROR)
            {
                string errorMsg = String.Format("COMPILE ERROR #{0} ({1}): {2}", id, type.ToString(), GL.GetShaderInfoLog(id));

                #if DEBUG
                Console.WriteLine(errorMsg);
                #endif

                throw new ShaderCompilationException(errorMsg);
            }
        }

        private const int COMPILATION_ERROR = 0;

        public int ID { get { return shaderProgram; } }
    }
}
