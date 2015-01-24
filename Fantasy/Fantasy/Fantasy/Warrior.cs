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
    class Warrior
    {
        int attackStr = 20;
        public int health = 30;
        Vector2 position;
        Texture2D image;
        SpriteFont font;

        public Warrior(ContentManager content)
        {
            font = content.Load<SpriteFont>("SpriteFont1");
            position = new Vector2(0, 300);
        }

        public int Attack()
        {
            Random num = new Random();

            int attack = num.Next(1, 20);

            return attack;
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
            //spriteBatch.Draw(image, position, Color.White);
            spriteBatch.DrawString(font, "Health: " + health, position, Color.Green);
        }
    }
}
