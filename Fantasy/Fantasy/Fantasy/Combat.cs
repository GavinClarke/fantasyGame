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
    class Combat
    {
        Warrior warrior;
        Wizard wizard;
        Enemy enemy;

        enum attacks
        {
            warrior,
            wizard,
            enemy,
        }

        attacks attackOrder;

        public Combat(ContentManager content)
        {
            attackOrder = attacks.wizard;
            wizard = new Wizard();
            enemy = new Enemy();
            warrior = new Warrior();
        }

        public void Update(GameTime gameTime)
        {
            if (attacks.wizard == attackOrder)
            {
                Vector3 damage = wizard.Attack(gameTime);
                if (1.0f == damage.Z)
                {
                    attackOrder = attacks.warrior;
                    enemy.Damage((int)(damage.X));
                }
                
            }
            if (attacks.warrior == attackOrder)
            {
                enemy.RoleDefence(warrior.Attack());
                attackOrder = attacks.enemy;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
