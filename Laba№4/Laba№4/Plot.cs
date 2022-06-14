using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Laba_4
{
    class Plot
    {
        protected GameWindow window;
        private HalfSphere halfsphere;

        private float scaling = 10.0f;
        private float xAngle = 0.0f;
        private float yAngle = 0.0f;
        private float lightPositionX = 20.0f;

        private bool toggleAnimation = false;


        public Plot(int width, int height)
        {
            window = new GameWindow(width, height, GraphicsMode.Default, "ЛР4 Псеунов | Вариант-4: Полушарие");

            window.Load += Window_Load;
            window.Resize += Window_Resize;
            window.RenderFrame += Window_RenderFrame;
            window.UpdateFrame += Window_UpdateFrame;
            window.KeyDown += Window_KeyDown;
        }

        public void Start()
        {
            halfsphere = new HalfSphere(2, 20, 1.0f, 0.4f, 0.3f);

            window.Run(1.0 / 60.0);
        }

        private void Window_Load(object sender, EventArgs e)
        {
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
        }

        private void Window_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);

            GL.LoadIdentity();

            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, window.Width / window.Height, 1.0f, 100.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void Window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                halfsphere.Precision += 1;
            }
            else if (e.Key == Key.N)
            {
                halfsphere.Precision -= 1;
            }

            if (e.Key == Key.Plus)
            {
                scaling -= 0.5f;
            }
            else if (e.Key == Key.Minus)
            {
                scaling += 0.5f;
            }

            if (e.Key == Key.Down)
            {
                xAngle += 10.0f;
                if (xAngle > 360.0f)
                    xAngle = 0.0f;
            }
            else if (e.Key == Key.Up)
            {
                xAngle -= 10.0f;
                if (xAngle < 0.0f)
                    xAngle = 360.0f;
            }
            else if (e.Key == Key.Right)
            {
                yAngle += 10.0f;
                if (yAngle > 360.0f)
                    yAngle = 0.0f;
            }
            else if (e.Key == Key.Left)
            {
                yAngle -= 10.0f;
                if (yAngle < 0.0f)
                    yAngle = 360.0f;
            }
            if (e.Key == Key.L)
            {
                if (GL.IsEnabled(EnableCap.Light0))
                    GL.Disable(EnableCap.Light0);
                else
                    GL.Enable(EnableCap.Light0);
            }

            if (e.Key == Key.BracketLeft)
            {
                lightPositionX -= 10.0f;
                if (lightPositionX < -180.0f)
                    lightPositionX = 180.0f;
            }
            else if (e.Key == Key.BracketRight)
            {
                lightPositionX += 10.0f;
                if (lightPositionX > 180.0f)
                    lightPositionX = -180.0f;
            }

            if (e.Key == Key.Enter)
            {
                toggleAnimation = !toggleAnimation;
            }


        }

        private void Window_UpdateFrame(object sender, FrameEventArgs e)
        {
            window.Title = $"ЛР4 Псеунов | Вариант-4: Полушарие [{halfsphere.Precision}]";

            Matrix4 rotation = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yAngle));

            if (toggleAnimation)
            {
                yAngle += 5.0f;
                if (yAngle > 360.0f)
                    yAngle = 0.0f;
  //              GL.UniformMatrix4(1, false, ref rotation);
                halfsphere.Animate(yAngle);
            }
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Translate(0.0, 0.0, -scaling);

            GL.Rotate(xAngle, 1.0, 0.0, 0.0);
            GL.Rotate(yAngle, 0.0, 1.0, 0.0);

            halfsphere.Draw();

            halfsphere.LightConfigure(lightPositionX);

            window.SwapBuffers();
        }
    }
}
