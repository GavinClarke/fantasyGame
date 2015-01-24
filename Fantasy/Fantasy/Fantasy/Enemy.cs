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
    class Enemy
    {
        int attackStr = 12;
        int defenceStr = 12;
        int health = 40;
        Vector2 position;
        Texture2D image;
        SpriteFont font;

        public Enemy(ContentManager content)
        {
            font = content.Load<SpriteFont>("SpriteFont1");
            position = new Vector2(600, 300);
        }

        public Vector2  Attack()
        {
            Random num = new Random();
            Vector2 result = new Vector2(0, 0);
            int attack = num.Next(1, attackStr);
            if ((attack % 2) == 0)
            {
                result = new Vector2(attack / 2, attack / 2);
            }
            else
            {
                result = new Vector2((float)(Math.Floor((float)(attack / 2)) + 1), (float)(Math.Floor((float)(attack / 2))));
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

        public void RoleDefence(int damage)
        {
            Random num = new Random();

            int defence = num.Next(1, defenceStr);

            if (damage > defence)
            {
                Damage(damage - defence);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(image, position, Color.White);
            spriteBatch.DrawString(font, "Health: " + health, position, Color.Red);
        }


    }
}
