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
    class Doors
    {
        Button DoorOne;
        Button DoorTwo;
        Button DoorThree;

        Random randNum;
        SpriteFont font;
        int newRoom = 0;

        public Doors(ContentManager content)
        {
            DoorOne = new Button(new Rectangle(175, 0, 100, 300),"DoorOne",content);
            DoorTwo = new Button(new Rectangle(325, 0, 100, 300), "DoorTwo", content);
            DoorThree = new Button(new Rectangle(475, 0, 100, 300), "DoorThree", content);
            randNum = new Random();
            font = content.Load<SpriteFont>("SpriteFont1");
        }

        public int Update()
        {
            newRoom = 0;
            if (DoorOne.CheckMouseClick())
            {
                newRoom = randNum.Next(1, 10);
            }
            else if (DoorTwo.CheckMouseClick())
            {
                newRoom = randNum.Next(1, 10);
            }
            else if (DoorThree.CheckMouseClick())
            {
                newRoom = randNum.Next(1, 10);
            }
            return newRoom;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            DoorOne.Draw(spriteBatch);
            DoorTwo.Draw(spriteBatch);
            DoorThree.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Room: " + newRoom, new Vector2(350, 400), Color.Red);
        }


    }
}
