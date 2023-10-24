using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK_console_sample01
{
    class SimpleWindow : GameWindow
    {
        private Vector2 objectPosition = Vector2.Zero; // Poziția obiectului
        private float objectSpeed = 0.02f; // Viteza de deplasare a obiectului

        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
            MouseMove += Mouse_Move;
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit(); // Ieșire din aplicație la apăsarea tastei Escape
            if (e.Key == Key.F11)
                this.WindowState = (this.WindowState == WindowState.Fullscreen) ? WindowState.Normal : WindowState.Fullscreen; // Comutare între modul fullscreen și modul normal la apăsarea tastei F11

            if (e.Key == Key.W) objectPosition.Y += objectSpeed; // Mișcare în sus la apăsarea tastei W
            if (e.Key == Key.S) objectPosition.Y -= objectSpeed; // Mișcare în jos la apăsarea tastei S
            if (e.Key == Key.A) objectPosition.X -= objectSpeed; // Mișcare în stanga la apăsarea tastei A
            if (e.Key == Key.D) objectPosition.X += objectSpeed; // Mișcare în dreapta la apăsarea tastei D
        }

        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            objectPosition.X = 2.0f * e.X / Width - 1.0f; // Controlul poziției obiectului pe axa X prin mișcarea mouse-ului
            objectPosition.Y = -(2.0f * e.Y / Height - 1.0f); // Controlul poziției obiectului pe axa Y prin mișcarea mouse-ului
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.SlateGray); // Setarea culorii de fundal
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height); // Definirea zonei de vizualizare
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0); // Definirea unei matrici ortografice
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Cod pentru actualizarea stării ferestrei (dacă este necesar)
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); // Curățarea buffer-ului de culoare
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(objectPosition.X, objectPosition.Y, -2.0); // Traducerea obiectului în spațiul 3D

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Yellow);
            GL.Vertex2(-0.5f, 0.5f);
            GL.Color3(Color.Red);
            GL.Vertex2(0.0f, -0.5f);
            GL.Color3(Color.Yellow);
            GL.Vertex2(0.5f, 0.5f);
            GL.End(); // Desenarea unui triunghi colorat

            SwapBuffers(); // Schimbarea buffer-ului pentru a afișa scena
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(30.0, 0.0); // Rularea ferestrei de joc cu o rată de actualizare specificată
            }
        }
    }
}
