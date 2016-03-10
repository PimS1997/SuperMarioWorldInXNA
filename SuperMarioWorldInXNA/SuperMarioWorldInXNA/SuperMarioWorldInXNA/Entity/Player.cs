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
        private Animation idleAnimation;

        private SpriteEffects flip = SpriteEffects.None;
        private AnimationPlayer sprite;

        //Current user movement input for support with game controllers
        private float movement;

        private const float MaxMoveSpeed = 500f;
        private const float MoveAcceleration = 13000f;

        private int spriteSheetWidth = 32; //Tiles
        private int spriteSheetHeight = 16; //Tiles

        private bool IsOnGround = true;

        private const float GroundDragFactor = 0.48f;

        ContentManager content;

        public Level Level
        {
            get { return level; }
        }
        Level level;

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

        public Player(Vector2 position, IServiceProvider services)
        {
            //this.level = level;

            LoadContent(services);

            Reset(position);
        }

        public void LoadContent(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");

            runAnimation = new Animation(content.Load<Texture2D>("Sprites/Mario/mario"), 0.1f, true, spriteSheetWidth, spriteSheetHeight, 2);
            idleAnimation = new Animation(content.Load<Texture2D>("Sprites/Mario/mario"), 0.1f, true, spriteSheetWidth, spriteSheetHeight, 1);
        }
        public void Reset(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
            isAlive = true;
            sprite.PlayAnimation(idleAnimation);
        }
        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            GetInput(keyboardState, gameTime);

            //ApplyPhysics(gameTime);

            if (Math.Abs(Velocity.X) - 0.02f > 0)
            {
                sprite.PlayAnimation(runAnimation);
            }
            else
            {
                sprite.PlayAnimation(idleAnimation);
            }
        }

        public void GetInput(KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Move(false, gameTime);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                Move(true, gameTime);
            }

        }

        private void Move(bool movingRight, GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (movingRight)
            {
                velocity.X = 250.0f;
            }
            else
            {
                velocity.X = -250.0f;
            }
            Position += velocity * elapsed;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));
        }

        public void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 previousPosition = Position;
            velocity.X += movement * MoveAcceleration * elapsed;


            //Prevents the player from walking faster than his max speed
            velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);
            velocity.X *= GroundDragFactor;

            Position += velocity * elapsed;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));


        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Velocity.X > 0)
                flip = SpriteEffects.None;
            else if (Velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;

            sprite.Draw(gameTime, spriteBatch, Position, flip);
        }
    }
}
