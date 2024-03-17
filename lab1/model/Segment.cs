using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab1.model
{
    public class Segment
    {
        private double X1 { set; get; }
        private double Y1 { set; get; }
        private double X2 { set; get; }
        private double Y2 { set; get; }
        private double crossingX { set; get; }
        private double crossingY { set; get; }

        public double CrossingX
        {
            get { return crossingX; }
        }

        public double CrossingY
        {
            get { return crossingY; }
        }

        public Segment(double x1, double y1, double x2, double y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public Segment() { }

        public bool AreCrossing(Segment other)
        {
            double dx1 = X2 - X1;
            double dy1 = Y2 - Y1;
            double dx2 = other.X2 - other.X1;
            double dy2 = other.Y2 - other.Y1;

            double cross1 = CrossProduct(dx1, dy1, other.X1 - X1, other.Y1 - Y1);
            double cross2 = CrossProduct(dx1, dy1, other.X2 - X1, other.Y2 - Y1);
            double cross3 = CrossProduct(dx2, dy2, X1 - other.X1, Y1 - other.Y1);
            double cross4 = CrossProduct(dx2, dy2, X2 - other.X1, Y2 - other.Y1);

            if (Math.Sign(cross1) == Math.Sign(cross2) || cross1 == 0 || cross2 == 0)
            {
                crossingX = 0;
                crossingY = 0;
                return false;
            }

            if (Math.Sign(cross3) == Math.Sign(cross4) || cross3 == 0 || cross4 == 0)
            {
                crossingX = 0;
                crossingY = 0;
                return false;
            }

            if (cross3 != 0 && cross4 != 0)
            {
                double ratio = Math.Abs(cross1) / Math.Abs(cross2 - cross1);
                crossingX = X1 + dx1 * ratio;
                crossingY = Y1 + dy1 * ratio;
            }
            else if (cross1 != 0 && cross2 != 0)
            {
                double ratio = Math.Abs(cross3) / Math.Abs(cross4 - cross3);
                crossingX = other.X1 + dx2 * ratio;
                crossingY = other.Y1 + dy2 * ratio;
            }
            else
            {
                crossingX = 0;
                crossingY = 0;
            }

            return true;
        }

        private double CrossProduct(double dx1, double dy1, double dx2, double dy2)
        {
            return dx1 * dy2 - dy1 * dx2;
        }
    }
}