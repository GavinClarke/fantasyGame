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
        SpriteFont font;
        Vector2 position;
        string current = "Wizard attack";
        enum attacks
        {
            warrior,
            wizard,
            enemy,
        }
        float timer = 0.0f;
        attacks attackOrder;

        public Combat(ContentManager content)
        {
            attackOrder = attacks.wizard;
            wizard = new Wizard(content);
            enemy = new Enemy(content);
            warrior = new Warrior(content);
            font = content.Load<SpriteFont>("SpriteFont1");
            position = new Vector2(350,0);
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
                    warrior.health += (int)damage.Y;
                    wizard.health += (int)damage.Y;
                }

                current = "Wizard attack";

            }
            if (attacks.warrior == attackOrder)
            {
                float t = gameTime.ElapsedGameTime.Milliseconds;
                timer += t / 1000.0f;

                if (timer > 6)
                {
                    enemy.RoleDefence(warrior.Attack());
                    attackOrder = attacks.enemy;
                    timer = 0;
                }

                current = "Warrior attack";

            }
            if (attacks.enemy == attackOrder)
            {
                float t = gameTime.ElapsedGameTime.Milliseconds;
                timer += t / 1000.0f;

                if (timer > 6)
                {
                    Vector2 result = enemy.Attack();
                    warrior.Damage((int)result.X);
                    wizard.Damage((int)result.Y);
                    attackOrder = attacks.wizard;
                    
                }

                current = "Enemy attack";

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemy.Draw(spriteBatch);
            warrior.Draw(spriteBatch);
            wizard.Draw(spriteBatch);
            spriteBatch.DrawString(font, current, position, Color.Green);
        }
    }
}
