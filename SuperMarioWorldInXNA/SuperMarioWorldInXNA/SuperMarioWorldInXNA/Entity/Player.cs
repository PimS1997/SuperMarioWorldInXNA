using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        //Animations
        private Animation runAnimation;

        private SpriteEffects flip = SpriteEffects.None;
        private AnimationPlayer sprite;
        ContentManager content;

        //Current user movement input for support with game controllers
        private float movement;

        private const float MaxMoveSpeed = 1800f;
        private const float MoveAcceleration = 13000f;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;
        public bool IsAlive
        {
            get { return isAlive; }
        }
        bool isAlive;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;

        public Player(Vector2 position)
        {
            //this.level = level;
            content = new ContentManager(serviceProvider);
            content.RootDirectory = "Content";
            LoadContent();

            Reset(position);
        }

        public void LoadContent()
        {
            runAnimation = new Animation(content.Load<Texture2D>("Sprites/Mario/mario_walk"), 0.1f, true);
        }
        public void Reset(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
            isAlive = true;
            sprite.PlayAnimation(runAnimation);
        }
        public void Update()
        {

        }

        public void GetInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                movement = -1.0f;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                movement = 1.0f;
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
            if (Velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X < 0)
                flip = SpriteEffects.None;

            sprite.Draw(gameTime, spriteBatch, Position, flip);
        }
    }
}
