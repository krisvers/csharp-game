using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Game : GameWindow {
	Renderer Renderer = new Renderer();

	public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title }) {
		Renderer.Resize(width, height);
	}

	protected override void OnUpdateFrame(FrameEventArgs args) {
		base.OnUpdateFrame(args);

		Renderer.Render();
		SwapBuffers();
		Input();
	}

	protected override void OnResize(ResizeEventArgs e) {
		base.OnResize(e);

		Renderer.Resize(e.Width, e.Height);
	}

	public void Input() {
		if (KeyboardState.IsKeyDown(Keys.Escape)) {
			Close();
		}
	}
}