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

        public Enemy()
        {
        }

        public int Attack()
        {
            Random num = new Random();

            int attack = num.Next(1, attackStr);

            return attack;
        }

        public void Damage(int damage)
        {
            health -= damage;
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
            spriteBatch.Draw(image, position, Color.White);
        }


    }
}
