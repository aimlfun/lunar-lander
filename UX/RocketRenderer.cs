using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketAI.Vehicle;
using RocketAI.Utils;
using System.Security.Cryptography;
using RocketAI.World;
using RocketAI.AI;

namespace RocketAI.Vehicle.UX
{
    internal class RocketRenderer
    {      
        /// <summary>
        /// Used to label the rockets.
        /// </summary>
        private readonly static Font s_labelFont = new("Arial", 7);

        /// <summary>
        /// These points are little more than a "square", rotated based on the angle of the ship.
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <returns></returns>
        private static PointF[] OutlineOfRocketPoints(float centerX, float centerY, float angle)
        {
            return new PointF[] { MathUtils.RotatePointAboutOrigin(new PointF(centerX - 10, centerY - 10), new PointF(centerX,centerY), angle),
                                  MathUtils.RotatePointAboutOrigin(new PointF(centerX + 10, centerY - 10), new PointF(centerX,centerY), angle),
                                  MathUtils.RotatePointAboutOrigin(new PointF(centerX + 10, centerY + 10), new PointF(centerX,centerY), angle),
                                  MathUtils.RotatePointAboutOrigin(new PointF(centerX - 10, centerY + 10), new PointF(centerX,centerY), angle),
                                  MathUtils.RotatePointAboutOrigin(new PointF(centerX - 10, centerY - 10), new PointF(centerX,centerY), angle)
                                };
        }

 
        /// <summary>
        /// Draws a bar on the lhs, for "fuel" indication. 
        /// Colour goes black > orange > red (<25%).
        /// </summary>
        /// <param name="g"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        private static void DrawFuelGauge(Rocket rocket, Graphics g, float centerX, float centerY)
        {
            PointF[] fuel = { MathUtils.RotatePointAboutOrigin(new PointF(centerX - 8, centerY + 10 - (float) rocket.PctFuelUsed*18), new PointF(centerX,centerY), rocket.AnglePointingDegrees),
                              MathUtils.RotatePointAboutOrigin(new PointF(centerX - 8, centerY + 10), new PointF(centerX,centerY), rocket.AnglePointingDegrees)
            };

            // colour of gauge changes as level drops.
            using Pen gauge = new(rocket.PctFuelUsed < 0.25F ? Color.FromArgb(200, 255, 0, 0) : (rocket.PctFuelUsed < 0.5F ? Color.FromArgb(200, 255, 165, 0) : Color.FromArgb(200, 0, 0, 0)), 2);

            g.DrawLine(gauge, fuel[0], fuel[1]);
        }

        /// <summary>
        /// Draws an eliminated ship. 
        /// A rectangle, with label.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="best"></param>
        /// <param name="shipRotated"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="label"></param>
        private static void DrawEliminatedShip(Rocket rocket, Graphics g, List<int>? best, out PointF[] shipRotated, float centerX, float centerY, string label)
        {
            using Pen penR = new(Color.FromArgb((rocket.EliminationCount).Clamp(0, 255), 200, 200, 200));
            using SolidBrush brushLabel = new(Color.FromArgb((rocket.EliminationCount + 100).Clamp(0, 255), 200, 200, 200));

            if (NeuralNetwork.s_networks[rocket.Id].Fitness < -10000) label = "up";

            shipRotated = OutlineOfRocketPoints(centerX, centerY, (float) rocket.AnglePointingDegrees);
            penR.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            g.DrawLines(penR, shipRotated);

            SizeF size = g.MeasureString(label, s_labelFont);
            g.DrawString(label, s_labelFont, brushLabel, new PointF(centerX - size.Width / 2, centerY - size.Height / 2));

            if (best is not null && best.Contains(rocket.Id))
            {
                int ix = best.IndexOf(rocket.Id);

                g.DrawEllipse(new Pen((Color.FromArgb(ix * 20 + 150, 20, 20, 255)), 6 - ix), centerX - 15, centerY - 15, 30, 30);
            }
        }

        /// <summary>
        /// Draws a shadow as the altitude goes low enough.
        /// It's basically an ellipse that gets larger and darker the nearer the ground.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="floorY"></param>
        /// <param name="centerX"></param>
        /// <returns></returns>
        private static void DrawShadowBeneathRocket(Rocket rocket, Graphics g, float floorY, float centerX)
        {
            if (rocket.Altitude < 255 && rocket.Altitude >= 0)
            {
                float shadowWidth = 11 * (255 - rocket.Altitude) / 255;
                float shadowHeight = 6 * (255 - rocket.Altitude) / 255;

                int alpha = (129 - (int)rocket.Altitude / 2) + 100;

                g.FillEllipse(new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)), centerX - shadowWidth, floorY - shadowHeight, shadowWidth * 2, shadowHeight * 2);
            }
        }

        /// <summary>
        /// Draws a rudimentary flame.
        /// It reverts to "default" to make the edge of the flame jaggedy.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="floorY"></param>
        private static void DrawFlame(Rocket rocket, Graphics g, float centerX, float centerY, float floorY)
        {
            var flameColour = RandomNumberGenerator.GetInt32(0, 2) switch
            {
                0 => Color.FromArgb(150, 253, 207, 88),
                1 => Color.FromArgb(150, 242, 125, 12),
                _ => Color.FromArgb(150, 240, 127, 19),
            };

            using SolidBrush brushFlame = new(flameColour);

            float flameHeight = 20F * (float)(rocket.BurnForce / (rocket.TotalMass * WorldSettings.s_gravity * AISettings.s_AIburnAmplifier));
            float flameWidth = flameHeight / 4;

            // flame would look dumb if it wasn't in the direction of the rocket. We are also thrust vectoring,
            // and therefore need to rotate the flame.
            double thrustAngle = rocket.AnglePointingDegrees - rocket.OffsetAngleOfThrustInDegrees * 10;

            /*
             *    [  ]
             *     /\   } points
             *     \/   } 
             */

            PointF[] points = { MathUtils.RotatePointAboutOrigin(new PointF(centerX, centerY + 10), new PointF(centerX, centerY), thrustAngle ),
                                MathUtils.RotatePointAboutOrigin(new PointF(centerX + flameWidth, centerY + 10 + flameHeight / 3F), new PointF(centerX, centerY), thrustAngle ),
                                MathUtils.RotatePointAboutOrigin(new PointF(centerX, centerY + 10 + flameHeight), new PointF(centerX,centerY), thrustAngle ),
                                MathUtils.RotatePointAboutOrigin(new PointF(centerX - flameWidth, centerY + 10 + flameHeight / 3F), new PointF(centerX, centerY), thrustAngle ),
                                MathUtils.RotatePointAboutOrigin(new PointF(centerX, centerY + 10), new PointF(centerX, centerY), thrustAngle )
                              };

            // if the rocket is next to the floor, we can't have the bottom pointy bit, it'll look dumb. 
            // so we flatten

            /*
             *      [  ]
             *       /\
             *   ########## <- no \/
             *   ##########
             *   
             */

            for (int point = 0; point < points.Length; point++)
            {
                if (points[point].Y > floorY)
                {
                    points[1].X += (points[2].Y - floorY) / 4; // flare the blast
                    points[3].X -= (points[2].Y - floorY) / 4;

                    // ensure it doesn't go thru the floor
                    points[point].Y = floorY;
                }
            }

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            g.FillPolygon(brushFlame, points);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // draws a line showing centre line
            // PointF p = MathUtils.RotatePointAboutOrigin(new PointF(centerX, centerY + 30), new PointF(centerX, centerY), anglePointingDegrees);
            // g.DrawLine(Pens.Red, new PointF(centerX, centerY), p);
        }

        /// <summary>
        /// Draws the rocket along with velocity lines.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="height"></param>
        internal static void Draw(Rocket rocket, Graphics g, int height, List<int>? best)
        {
            PointF[] shipRotated;

            float yScale = (height - 70) / (float)RocketSettings.s_maxHeight;
            float centerX = rocket.PositionX;
            float centerY = height - 30 - rocket.Altitude * yScale;

            string label = rocket.EliminatedBecauseOf;
            if (NeuralNetwork.s_networks[rocket.Id].Fitness < -10000) label = "up";

            if (rocket.IsEliminated)
            {
                if (--rocket.EliminationCount < 0) return;
                DrawEliminatedShip(rocket, g, best, out _, centerX, centerY, label);

                return;
            }

            float floorY = height - 22;

            // draw a dotted line to it's intended base
            if (AISettings.s_fixedBases && rocket.Altitude < 1800 && rocket.Altitude>800)
            {
                using Pen pBase = new(Color.FromArgb((int)((1 - rocket.Altitude / RocketSettings.s_maxHeight) * 40), 255, 255, 255));
                pBase.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                pBase.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                g.DrawLine(pBase, centerX, centerY, rocket.targetBase, floorY);
            }

            using Pen penVelocity = new(Color.FromArgb(100, 255, 255, 255));
            penVelocity.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            rocket.UpdateFitness();

            shipRotated = OutlineOfRocketPoints(centerX, centerY, (float) rocket.AnglePointingDegrees);

            DrawShadowBeneathRocket(rocket, g, floorY, centerX);

            // rudimentary rectangle for space ship
            g.FillPolygon(rocket.ColouredBrushToPaintShip, shipRotated);

            DrawFuelGauge(rocket, g, centerX, centerY);

            // if coming in hot, draw red border to box
            if (rocket.ComingInHot()) g.DrawLines(Pens.Red, shipRotated); else g.DrawLines(Pens.Gray, shipRotated);

            // cross showing landed.
            if (rocket.HasLandedSafely)
            {
                g.DrawLine(Pens.White, shipRotated[0], shipRotated[2]);
                g.DrawLine(Pens.White, shipRotated[1], shipRotated[3]);
            }
            else
            {
                DrawFlame(rocket, g, centerX, centerY, floorY);
            }

            // draw upwards velocity
            g.DrawLine(penVelocity,
                       new PointF(centerX, centerY),
                       MathUtils.RotatePointAboutOrigin(new PointF(centerX, centerY + (float)rocket.VerticalVelocity), new PointF(centerX, centerY), rocket.AnglePointingDegrees+ 180));

            g.DrawLine(penVelocity,
                       new PointF(centerX, centerY),
                       MathUtils.RotatePointAboutOrigin(new PointF(centerX + (float)rocket.LateralVelocity, centerY), new PointF(centerX, centerY), rocket.AnglePointingDegrees));

            // draw a circle around the best rockets
            if (best is not null && best.Contains(rocket.Id))
            {
                int ix = best.IndexOf(rocket.Id);
                g.DrawEllipse(new Pen(Color.Red, 5 - ix), centerX - 15, centerY - 15, 30, 30);
            }
        }

    }
}
