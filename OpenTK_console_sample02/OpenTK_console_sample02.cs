using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK_console_sample02
{
    class SimpleWindow3D : GameWindow
    {
        const float rotation_speed = 180.0f;
        float angle;
        bool showCube = true;
        KeyboardState lastKeyPress;

        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Blue); // Setarea culorii de fundal
            GL.Enable(EnableCap.DepthTest); // Activarea testului de adâncime pentru a gestiona obiectele în 3D
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            double aspect_ratio = Width / (double)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective); // Definirea matricei de proiecție pentru perspectivă
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit(); // Ieșire din aplicație la apăsarea tastei Escape
                return;
            }
            else if (keyboard[OpenTK.Input.Key.P] && !keyboard.Equals(lastKeyPress))
            {
                // Comutare afișare/ascundere cub la apăsarea tastei P
                showCube = !showCube;
            }

            lastKeyPress = keyboard;

            if (mouse[OpenTK.Input.MouseButton.Left])
            {
                // Comutare afișare/ascundere cub la apăsarea butonului stâng al mouse-ului
                showCube = !showCube;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Curățarea buffer-ului de culoare și a celui de adâncime
            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0); // Definirea matricei de vedere
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Rotate(angle, 0.0f, 1.0f, 0.0f); // Rotirea obiectului

            if (showCube == true)
            {
                DrawCube(); // Desenarea unui cub colorat
                DrawAxes_OLD(); // Desenarea axelor X, Y, Z
            }
            SwapBuffers(); // Schimbarea buffer-ului pentru a afișa scena
        }

        private void DrawAxes_OLD()
        {
            GL.Begin(PrimitiveType.Lines);

            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(20, 0, 0);

            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 20, 0);

            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 20);

            GL.End();
        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D())
            {
                example.Run(30.0, 0.0); // Rularea ferestrei de joc cu o rată de actualizare specificată
            }
        }
    }
}
