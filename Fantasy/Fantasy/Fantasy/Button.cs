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
    class Button
    {
        Texture2D m_button;
        Rectangle m_buttonRect;
        String m_buttonName;
        MouseState oldMouse;
        public Button(Rectangle rect,String name,ContentManager content)
        {
            m_buttonRect = rect;
            m_buttonName = name;
            oldMouse = Mouse.GetState();
            m_button = content.Load<Texture2D>(m_buttonName);
        }

        public bool CheckMouseClick()
        {
            MouseState mouse = Mouse.GetState();

            if (mouse.X > m_buttonRect.X &&
               mouse.X < m_buttonRect.X + m_buttonRect.Width &&
               mouse.Y > m_buttonRect.Y &&
               mouse.Y < m_buttonRect.Y + m_buttonRect.Height &&
               mouse.LeftButton == ButtonState.Released &&
               oldMouse.LeftButton == ButtonState.Pressed)
            {
                oldMouse = mouse;
                return true;
            }
            oldMouse = mouse;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_button, m_buttonRect, Color.White);
        }

    }
}
