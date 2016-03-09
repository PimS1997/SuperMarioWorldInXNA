using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarioWorldInXNA
{
    class Player
    {
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;

        private AnimationPlayer sprite;

        //Current user movement input for support with game controllers
        private float movement;

        private const float MaxMoveSpeed = 1800f;
        private const float MoveAcceleration = 13000f;


        public void LoadContent()
        {

        }

        public void Update()
        {

        }

        public void GetInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {

            }
        }

        public void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 previousPosition = Position;
            velocity.X += movement * MoveAcceleration * elapsed;

            //Prevents the player from walking faster than his max speed
            velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

            Position += velocity * elapsed;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriet
        }
    }
}
