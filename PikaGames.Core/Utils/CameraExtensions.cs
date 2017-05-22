using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace PikaGames.Games.Core.Utils
{
    public static class CameraExtensions
    {
        private const float CameraMoveSmooth = 0.1f;
        private const float CameraZoomSmooth = 0.05f;

        public static void LookAtMultiple(this Camera2D camera, Vector2 contentSize, int safeBound, params Vector2[] positions)
        {
            var minPos = Vector2.Zero;
            var maxPos = Vector2.Zero;

            var first = true;
            foreach (var position in positions)
            {
                if (first)
                {
                    first = false;
                    minPos = new Vector2(position.X, position.Y);
                    maxPos = new Vector2(position.X, position.Y);
                }
                else
                {
                    minPos.X = Math.Min(minPos.X, position.X);
                    minPos.Y = Math.Min(minPos.Y, position.Y);

                    maxPos.X = Math.Max(maxPos.X, position.X);
                    maxPos.Y = Math.Max(maxPos.Y, position.Y);
                }
            }

            minPos.X -= safeBound;
            minPos.Y -= safeBound;
            maxPos.X += safeBound;
            maxPos.Y += safeBound;

            var bounds = new Rectangle((int) minPos.X, (int) minPos.Y, (int)Math.Max(GameBase.Instance.ViewportAdapter.VirtualWidth, maxPos.X-minPos.X), (int)Math.Max(GameBase.Instance.ViewportAdapter.VirtualHeight, maxPos.Y - minPos.Y));

            //Debug.WriteLine(bounds);

            // zoom

            var zoom = 1f / Math.Max((float)bounds.Width / (float)(GameBase.Instance.ViewportAdapter.VirtualWidth - 2*safeBound), (float)bounds.Height / (float)(GameBase.Instance.ViewportAdapter.VirtualHeight - 2*safeBound));
            
            camera.ZoomSmooth(zoom);
            camera.LookAtSmooth(contentSize, new Vector2(minPos.X + bounds.Width/2f, minPos.Y + bounds.Height/2f));
        }

        public static void ZoomSmooth(this Camera2D camera, float targetZoom)
        {
            //Debug.WriteLine(camera.Zoom + " - " + targetZoom);
            camera.Zoom = MathHelper.Lerp(camera.Zoom, targetZoom, CameraZoomSmooth);
        }

        public static void LookAtSmooth(this Camera2D camera, Vector2 contentSize, Vector2 target)
        {
            var newPosition = target - new Vector2(GameBase.Instance.ViewportAdapter.VirtualWidth, GameBase.Instance.ViewportAdapter.VirtualHeight) /2f;

            var x = MathHelper.Lerp(camera.Position.X, newPosition.X, CameraMoveSmooth);
            x = MathHelper.Clamp(x, 0.0f, contentSize.X - GameBase.Instance.ViewportAdapter.VirtualWidth);

            var y = MathHelper.Lerp(camera.Position.Y, newPosition.Y, CameraMoveSmooth);
            y = MathHelper.Clamp(y, 0.0f, contentSize.Y - GameBase.Instance.ViewportAdapter.VirtualHeight);

            //Debug.WriteLine(contentSize.ToString() + " - " + target.ToString() + " - " + x + "," + y);

            camera.Position = new Vector2((int) x, (int) y);
        }

    }
}
