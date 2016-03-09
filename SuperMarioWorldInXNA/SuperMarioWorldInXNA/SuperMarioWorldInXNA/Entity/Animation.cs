

using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioWorldInXNA
{
    public class Animation
    {
        public Texture2D Texture
        {
            get { return Texture; }
        }
        Texture2D texture;

        public float FrameTime
        {
            get { return frameTime; }
        }
        float frameTime;
    }
}