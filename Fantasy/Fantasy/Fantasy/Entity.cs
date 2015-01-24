using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fantasy
{
    class Entity
    {
        Vector2 position = new Vector2(61,271);
        Vector2 centre;
        int size = 26;
        int speed = 1;
        const byte UP = 0, DOWN = 1, LEFT = 2, RIGHT = 3;
        byte direction = LEFT;


        const byte WIZARD = 0, BARBARIAN = 1;
        byte character = WIZARD;
        bool moveUp = true;
        bool moveDown = true;
        bool moveLeft = true;
        bool moveRight = true;
        public void Update(KeyboardState keyPress, KeyboardState oldKeyPress, int tileSize)
        {
            // Center of Player needs to be constantly updated for camera to follow the player
            centre = new Vector2((int)position.X + size * 0.5f, (int)position.Y + size * 0.5f);
            // Get Tile Position of Player


            // Swap Characters
            if (keyPress.IsKeyDown(Keys.Z) && oldKeyPress != keyPress)
                SwapCharacters();
            // Move Character
            Move(keyPress);
        }
        public void Draw(SpriteBatch spritebatch, Texture2D playerTex)
        {
            if (character == BARBARIAN)
                spritebatch.Draw(playerTex, new Rectangle((int)position.X, (int)position.Y, size, size), new Rectangle(direction * 128, 0, 128, 128), Color.White);
            else
                spritebatch.Draw(playerTex, new Rectangle((int)position.X, (int)position.Y, size, size), new Rectangle(direction * 128, 128, 128, 128), Color.White);
        }

        public void SwapCharacters()
        {
            if (character == WIZARD)
                character = BARBARIAN;
            else
                character = WIZARD;
        }

        public void Move(KeyboardState keyPress)
        {
            if (keyPress.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
                direction = UP;
            }
            else if (keyPress.IsKeyDown(Keys.D))
            {
                position.X += speed;
                direction = RIGHT;
            }
            else if (keyPress.IsKeyDown(Keys.A))
            {
                position.X -= speed;
                direction = LEFT;
            }
            else if (keyPress.IsKeyDown(Keys.S))
            {
                position.Y += speed;
                direction = DOWN;
            }
        }
        public void ReloadEnitity()
        {
            position = new Vector2(61, 271);
            direction = RIGHT;
        }

        public Vector2 GetCentre
        { get { return centre; } }
        public Vector2 GetPosition
        { get { return position; } set { position = value; } }
        public byte GetDirection
        { get { return direction; } }
        public int GetSize
        { get { return size; } }

    }
}
