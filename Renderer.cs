using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Renderer {
	int Vao;
	int Vbo;
	int Program;

	public Renderer() {
		Vao = GL.GenVertexArray();
		GL.BindVertexArray(Vao);

		Vbo = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ArrayBuffer, Vbo);
		GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * 18, new float[] {
			-0.5f, -0.5f, 0.0f,
			0.5f, -0.5f, 0.0f,
			0.0f,  0.5f, 0.0f
		}, BufferUsageHint.StaticDraw);

		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
		GL.EnableVertexAttribArray(0);

		int vshader = GL.CreateShader(ShaderType.VertexShader);
		int fshader = GL.CreateShader(ShaderType.FragmentShader);

		string vsrc = File.ReadAllText("assets/shader.vert");
		string fsrc = File.ReadAllText("assets/shader.frag");

		GL.ShaderSource(vshader, vsrc);
		GL.ShaderSource(fshader, fsrc);

		GL.CompileShader(vshader);

		GL.GetShader(vshader, ShaderParameter.CompileStatus, out int success);
		if (success == 0) {
			Console.WriteLine("OpenGL vertex shader error: " + GL.GetShaderInfoLog(vshader));
		}

		GL.CompileShader(fshader);

		GL.GetShader(fshader, ShaderParameter.CompileStatus, out success);
		if (success == 0) {
			Console.WriteLine("OpenGL fragment shader error: " + GL.GetShaderInfoLog(fshader));
		}

		Program = GL.CreateProgram();

		GL.AttachShader(Program, vshader);
		GL.AttachShader(Program, fshader);

		GL.LinkProgram(Program);

		GL.DetachShader(Program, vshader);
		GL.DetachShader(Program, fshader);
		GL.DeleteShader(vshader);
		GL.DeleteShader(fshader);

		GL.UseProgram(Program);
	}

	public void Render() {
		GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
	}

	public void Resize(int width, int height) {
		GL.Viewport(0, 0, width, height);
	}
}