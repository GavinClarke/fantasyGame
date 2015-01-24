using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fantasy
{
    class Camera
    {
        Viewport viewport;          // Player viewport
        Matrix transform;           // Transform Matrix
        Vector3 minCameraDistance;  // Minimum Distance before camera stop scrolling
        const float ZOOM = 1;       // Camera Zoom
        int width;                  // Width of Level
        int height;                 // Height of Level

        public Camera(Viewport newViewPort, int widthOfWorld, int heightOfWorld)
        {
            // Size of the World, does not effect sreensize
            width = widthOfWorld;
            height = heightOfWorld;

            // Viewport, values for screen height and width
            viewport = newViewPort;

            // Min Camera Distance before screen stops scrolling
            minCameraDistance = new Vector3((newViewPort.Width * 0.5f) / ZOOM, (newViewPort.Height * 0.5f) / ZOOM, 0);
        }


        public void Update(Vector2 playersCentre)
        {
            //Stops camera moving outside Width/Height Boundaries 
            //Far Left of the Map
            if (playersCentre.X <= minCameraDistance.X)
                playersCentre.X = minCameraDistance.X;

            // Far Right of the Map
            // Neg Min Distance + Viewports Width = Max Width Distance
            else if (playersCentre.X >= -minCameraDistance.X + width)
                playersCentre.X = -minCameraDistance.X + width;

            // Top of the Map
            if (playersCentre.Y <= minCameraDistance.Y)
                playersCentre.Y = minCameraDistance.Y;

            // Bottom of the Map
            // Neg Min Distance + Viewports Height = Max Height Distance
            else if (playersCentre.Y >= -minCameraDistance.Y + height)
                playersCentre.Y = -minCameraDistance.Y + height;

            // Matrix Multi: Player Center * Rotation * Scaler * Screen Centre
            transform = Matrix.CreateTranslation(new Vector3(-playersCentre.X, -playersCentre.Y, 0))
                      //* Matrix.CreateRotationZ((float)Math.PI) "Used for Rotations"
                      * Matrix.CreateScale(new Vector3(ZOOM, ZOOM, 0))
                      * Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
        }

        // Getters and Setters
        //////////////////////////
        public Matrix GetTranslation
        { get { return transform; } }
    }
}
