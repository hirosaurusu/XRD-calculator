using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRD_ver2
{
    internal class CalcEq
    {
        /// <summary>
        /// XRDの回折角を計算する(立方晶・六方晶)
        /// </summary>
        /// <param name="CryType"></param>
        /// <param name="h"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static double calcXRD(int CryType, double h, double k, double l, double a, double b, double c, double lambda)
        {
            double theta;
            if (CryType==3)
            {
                theta = 2 * 180 / Math.PI * Math.Asin(lambda / a / 2 * Math.Sqrt(h * h + k * k + l * l));
                return theta;
            }
            else if (CryType==5)
            {
                theta = 2 * 180 / Math.PI * Math.Asin(lambda / 2 * Math.Sqrt( (h * h + h*k + k * k)/a/a*4/3 + l*l/c/c ));
                return theta;
            }
            else if (CryType==6)
            {
                theta = 2 * 180 / Math.PI * Math.Asin(lambda / 2 * Math.Sqrt((h * h + k * k) / a / a + l * l / c / c));
                return theta;
            }
            else
            {
                return 100.00;
            }
            
        }


        public static double calcAngle(int CryType, double h1, double k1, double l1, double h2, double k2, double l2, double a, double b, double c)
        {
            double phi;
            double d1, d2;

            if (CryType == 3)
            {
                phi = 180 / Math.PI * Math.Acos( ( h1*h2 + k1 * k2 + l1 * l2 ) / ( Math.Sqrt(h1*h1+k1*k1+l1*l1) * Math.Sqrt(h2 * h2 + k2 * k2 + l2 * l2)) );
            }
            else if (CryType == 5)
            {
                d1 = 1 / Math.Sqrt((h1 * h1 + k1 * k1 + h1 * k1) * 4 / (3 * a * a) + l1 * l1 / (c * c));
                d2 = 1 / Math.Sqrt((h2 * h2 + k2 * k2 + h2 * k2) * 4 / (3 * a * a) + l2 * l2 / (c * c));
                phi = 180 / Math.PI * Math.Acos( d1 * d2 * ( ( h1 * h2 + k1 * k2 + 1 / 2 * ( h1 * k2 + k1 * h2 ) ) * 4 / (3 * a * a) + l1 * l2 / (c * c) ) );
            }
            else if (CryType == 6)
            {
                d1 = 1 / Math.Sqrt((h1 * h1 + k1 * k1 + h1 * k1) / (a * a) + l1 * l1 / (c * c));
                d2 = 1 / Math.Sqrt((h2 * h2 + k2 * k2 + h2 * k2) / (a * a) + l2 * l2 / (c * c));
                phi = 180 / Math.PI * Math.Acos( d1 * d2 * ( ( h1 * h2 + k1 * k2 ) / (a*a) + (l1*l2) / (c*c) ));
            }
            else
            {
                phi = 1000.00;//if else 構文を使うとelseを指定しないとダメなので、とりあえずテキトーに値を入れておいた。
            }

            return phi;
        }


        public static double calcDistance(int CryType, double h, double k, double l, double a, double b, double c)
        {
            double d;
            if (CryType == 3)
            {
                d = a / Math.Sqrt( h*h + k*k + l*l );
            }
            else if (CryType == 5)
            {
                d = 1 / Math.Sqrt((h * h + k * k + h * k) * 4 / (3 * a * a) + l * l / (c * c));
            }
            else if (CryType == 6)
            {
                d = 1 / Math.Sqrt((h * h + k * k + h * k) / (a * a) + l * l / (c * c));
            }
            else
            {
                d = 1000.00;//if else 構文を使うとelseを指定しないとダメなので、とりあえずテキトーに値を入れておいた。
            }
            return d;
        }
    }
}
