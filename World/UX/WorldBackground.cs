using RocketAI.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RocketAI.UX
{
    /// <summary>
    /// Draws and caches our space themed "background".
    /// </summary>
    internal static class WorldBackground
    {
        /// <summary>
        /// Store a pre-cached copy of our "scenery" comprised of grass & stars.
        /// </summary>
        private static Bitmap? preCachedSceneryImage = null;

        /// <summary>
        /// Returns a pre-drawn background.
        /// </summary>
        /// <returns></returns>
        internal static Bitmap? GetScenery()
        {
            return preCachedSceneryImage;
        }

        /// <summary>
        /// Add a little style, by drawing our background (grass + stars).
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        internal static void Draw(int width, int height)
        {
            preCachedSceneryImage = new(width, height);

            using Graphics gbackground = Graphics.FromImage(preCachedSceneryImage);

            // quality graphics requires setting these (avoid pixellation)
            gbackground.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gbackground.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            gbackground.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            gbackground.Clear(Color.Black); // space is dark, very dark

            int landHeight = 60;

            DrawStars(width, height, gbackground, landHeight);

            DrawLinesDelimitingSpaceWithAir(width, gbackground);

            DrawGround(width, height, gbackground, landHeight);

            DrawBaseTargetsAsBullseyes(height, gbackground);

            gbackground.Flush();

            // preCachedSceneryImage has "space" and "ground" drawn on it.
        }

        /// <summary>
        /// Draw a bullseye for each target base. 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="gbackground"></param>
        private static void DrawBaseTargetsAsBullseyes(int height, Graphics gbackground)
        {            
            // draw the BULLSEYE bases
            if (AISettings.s_fixedBases)
            {
                float floorY = height - 22;

                using Pen bullseyeOuter = new(Color.FromArgb(150, 0, 0, 255), 2);
                using SolidBrush bullseyeWhite = new(Color.FromArgb(180, 255, 255, 255));
                using SolidBrush bullseye = new(Color.FromArgb(220, 255, 0, 0));

                // r-x/y are ellipse radius. Full circles look silly given the ground goes into the horizon, so we scale "y" to make a squashed circle.
                float rox = 28;
                float roy = 0.5F * rox;
                float rbx = 10;
                float rby = 0.4F * rbx;

                for (int baseIndex = 0; baseIndex <= 5; baseIndex++)
                {
                    // white ellipse
                    gbackground.FillEllipse(bullseyeWhite, GetBaseOffset(baseIndex) - rox / 2, floorY - roy / 2, rox, roy);
                    
                    // blue outer circle
                    gbackground.DrawEllipse(bullseyeOuter, GetBaseOffset(baseIndex) - rox / 2, floorY - roy / 2, rox, roy);
                    
                    // red center dot
                    gbackground.FillEllipse(bullseye, GetBaseOffset(baseIndex) - rbx / 2, floorY - rby / 2, rbx, rby);
                }
            }
        }

        /// <summary>
        /// We need to detect when rockets have gone rogued and decided to fly into outerspace.
        /// To make it obvious in the UI, we draw an arbitrary line.
        /// </summary>
        /// <param name="widthOfCanvas"></param>
        /// <param name="gbackground"></param>
        private static void DrawLinesDelimitingSpaceWithAir(int widthOfCanvas, Graphics gbackground)
        {
            // draw - - - - - - indicating the point at which rockets are deemed to be escaping the atmosphere
            using Pen ceilingPen = new(Color.FromArgb(10, 255, 255, 255));
            ceilingPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            gbackground.DrawLine(ceilingPen, 0, 50, widthOfCanvas, 50);
        }

        /// <summary>
        /// Ground is a rectangle with some horizontal lines, and lines receding into the horizon. Nothing fancy. 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="gbackground"></param>
        /// <param name="landHeight"></param>
        private static void DrawGround(int width, int height, Graphics gbackground, int landHeight)
        {
            using SolidBrush grassbrush = new(Color.FromArgb(130, 150, 150, 155));

            using Pen penGrassLine = new(Color.FromArgb(80, 200, 200, 200));
            penGrassLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            gbackground.FillRectangle(grassbrush, 0, height - landHeight, width, height);

            // vertical diagonal grass lines
            for (int diagonalLineX = -25; diagonalLineX < 25; diagonalLineX++)
            {
                gbackground.DrawLine(penGrassLine, width / 2 + diagonalLineX * 40, height - landHeight, width / 2 + diagonalLineX * 100, height);
            }

            int t = 10;

            // horizontal grass lines
            for (int horizontalLineY = 0; horizontalLineY < 11; horizontalLineY++)
            {
                gbackground.DrawLine(penGrassLine, 0, height - landHeight + horizontalLineY * t, width, height - landHeight + horizontalLineY * t);
                t += 2;
            }
        }

        /// <summary>
        /// Draw stars of varying size and brightness on the background.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="gbackground"></param>
        /// <param name="landHeight"></param>
        private static void DrawStars(int width, int height, Graphics gbackground, int landHeight)
        {
            // add some stars in the form of "blobs" varying in size
            for (int stars = 0; stars < 800; stars++)
            {
                int diameterOfStar = RandomNumberGenerator.GetInt32(1, 5);
                Point positionOfStarInSky = new(RandomNumberGenerator.GetInt32(0, width), RandomNumberGenerator.GetInt32(0, height - landHeight - 70));

                gbackground.FillEllipse(new SolidBrush(Color.FromArgb(RandomNumberGenerator.GetInt32(2, 120) + 5, 255, 255, 255)),
                                        positionOfStarInSky.X, positionOfStarInSky.Y,
                                        diameterOfStar, diameterOfStar);
            }
        }

        /// <summary>
        /// Returns the offset for a base from its' index.
        /// </summary>
        /// <param name="baseIndex"></param>
        /// <returns></returns>
        internal static float GetBaseOffset(int baseIndex)
        {
            return baseIndex * 300 + 200;
        }
    }
}