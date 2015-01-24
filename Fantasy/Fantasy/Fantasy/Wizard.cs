using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fantasy
{
    class Wizard
    {
        int attackStr = 4;
        public int health = 50;
        Vector2 position;
        Texture2D image;
        enum wizAttack
        {
            none,
            heal,
            attack,
            both
        }

        wizAttack attackStates;
        MouseState oldMouse;
        float timer = 0.0f;
        bool startTimer;
        SpriteFont font;

        public Wizard(ContentManager content)
        {
            attackStates = wizAttack.none;
            startTimer = false;
            font = content.Load<SpriteFont>("SpriteFont1");
            image = content.Load<Texture2D>("wizard");
            position = new Vector2(0, 50);
        }

        public Vector3 Attack(GameTime gameTime)
        {
            /*Random num = new Random();*/
            
            Vector3 result = new Vector3(0,0,0); //= num.Next(1, attackStr);
            
            
            MouseState mouse = Mouse.GetState();
            if (mouse != oldMouse && mouse.LeftButton == ButtonState.Pressed)
            {
                if (wizAttack.none == attackStates)
                {
                    attackStates = wizAttack.attack;
                    startTimer = true;
                }
                else if (wizAttack.heal == attackStates)
                {
                    attackStates = wizAttack.both;
                    timer = 7.0f;
                }
            }
            
            if (mouse != oldMouse && mouse.RightButton == ButtonState.Pressed)
            {
                if (wizAttack.none == attackStates)
                {
                    attackStates = wizAttack.heal;
                    startTimer = true;
                }
                else if (wizAttack.attack == attackStates)
                {
                    attackStates = wizAttack.both;
                    timer = 7.0f;
                }
            }
            if (true == startTimer)
            {
                float t = gameTime.ElapsedGameTime.Milliseconds;
                timer += t / 1000.0f;
            }
            if (timer > 3.0)
            {
                if (wizAttack.both == attackStates)
                {
                    result = new Vector3(2, 2, 1);
                }
                else if (wizAttack.attack == attackStates)
                {
                    result = new Vector3(4, 0, 1);
                }
                else if (wizAttack.heal == attackStates)
                {
                    result = new Vector3(0, 4, 1);
                }
                attackStates = wizAttack.none;
                startTimer = false;
                timer = 0;
            }
            return result;
        }

        public void Damage(int damage)
        {
            if (health > 0)
            {
                health -= damage;
            }
            else
            {
                health = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, Color.White);
            spriteBatch.DrawString(font, "Health: " + health, new Vector2(position.X, position.Y - 15), Color.Blue);
        }
    }
}
