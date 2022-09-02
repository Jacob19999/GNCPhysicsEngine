// Rocket engine simulator test program for  By Jacob Tang

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fluids;


class engine_sim
{
    static void Main(string[] args)
    {

        var lox_1 = new lox(-173.5);
        var ch4_1 = new lMethane(-92.7);

        Console.WriteLine("LOX Density : " + lox_1.getDensity());
        Console.WriteLine("CH4 Density : " + ch4_1.getDensity());
        Console.ReadLine();

    }
}


namespace Fluids
{
    class lox
    {

        private double t_k;

        public lox(double temperature)
        {
            t_k = temperature;

        }

        public double get_kinematicViscosity()
        {

            // Lookup table 2D array Temp to Vapour Pressur. Kelvin / Kg/m^3
            double[,] tkVpArry = new double[,] { {-200, 1230.1 },
                                                 {-190, 1184.7 },
                                                 {-180, 1137.7 },
                                                 {-170, 1087.9 }};


            var interpolateDensity = new Interpolator(tkVpArry, t_k);
            return interpolateDensity.mathInterpolate();
        }

        public double get_vapourPressure()
        {

            // Lookup table 2D array Temp to Vapour Pressur. Kelvin / Kg/m^3
            double[,] tkVpArry = new double[,] { {-200, 1230.1 },
                                                 {-190, 1184.7 },
                                                 {-180, 1137.7 },
                                                 {-170, 1087.9 }};


            var interpolateDensity = new Interpolator(tkVpArry, t_k);
            return interpolateDensity.mathInterpolate();
        }


        public double getDensity()
        {

            // Lookup table 2D array Temp to Density. Kelvin / Kg/m^3
            double[,] tkRpArry = new double[,] { {-200, 1230.1 },
                                                 {-190, 1184.7 },
                                                 {-180, 1137.7 },
                                                 {-170, 1087.9 }};

            var interpolateDensity = new Interpolator(tkRpArry , t_k);
           
            return interpolateDensity.mathInterpolate();
        }

    }

    class lMethane
    {

        private double t_k;

        public lMethane(double temperature)
        {
            t_k = temperature;
        }

        public double getDensity()
        {
            // Lookup table 2D array Temp to Density. Kelvin / Kg/m^3 @ 50 Bar

            double[,] tkRpArry = new double[,] { {-110,  1087.9 },
                                                 {-100,  231.48 },
                                                 {-90,   281.67 },
                                                 {-82.6, 234.97 }};

            var interpolateDensity = new Interpolator(tkRpArry, t_k);

            return interpolateDensity.mathInterpolate();
        }
     }
}

// RNG for combustion variations.
class rngGenerator
{
    // Time based RNG as seed.
    public rngGenerator() 
    {
        Random rnd = new Random();
        int num = rnd.Next();
    }

    public double getRngFuelFlowVariation()
    {
        return 0;
    }

    public double getRngCombustionVaiations()
    {
        return 0;
    }
}


class Interpolator
{

    double[,] arry = new double[,] { { 0, 0 } };

    private double tVal;

    public Interpolator(double[,] input2dArry, double targetVal)
    {

        arry = input2dArry;
        tVal = targetVal;

    }

    public double mathInterpolate()
    {

        int i = 0;
        double Col1Val1 = 0;
        double Col1Val2 = 0;
        double Col2Val1 = 0;
        double Col2Val2 = 0;
        double gradient = 0;

        // Look for closest target in the arry,use 2 closest values to interpolate.
        while (i < (arry.GetLength(0) - 1))
        {

            Col1Val1 = arry[i, 0];
            Col1Val2 = arry[i + 1, 0];

            //Limitiation the array first column must be in decending order.
            if ((tVal > Col1Val1) && (tVal < Col1Val2))
            {

                Col2Val1 = arry[i, 1];
                Col2Val2 = arry[i + 1, 1];
                break;
            }
            i += 1;
        }
        //Interpolate by using the target temperature and the 2 set points . Using the gradient equation (m=y2-y1/x2-x1)
        gradient = (Col2Val2 - Col2Val1) / (Col1Val2 - Col1Val1);

        //Find new Y value assuming comstant gradient. y2=m(x2-x1)+y1
        Col2Val2 = (gradient * (tVal - Col1Val1)) + Col2Val1;

        return Col2Val2;
    }
}


class engine
{

    private double isp = 0;
    private double oxdizerFF = 0;
    private double fuelFF = 0;
    private double cStar = 0;
    private double throatDia = 0;
    private double nozzleDia;
    private double expensionRatio = 0;
    private double exhaustVelocity = 0;
    private double mach = 0;
    private double combustionTemp = 0;
    private double kGasConstant = 1.380694E-23;
    private double rGasConstant = 0.092057;
    private double throttle = 0;
 

    public engine()
    {
        

    }

    public double absoluteStagationTemp()
    {
        // T0 = T + v^2 / (2CpJ)
        return 0;
    }
    public double getGasVelocity()
    {
        return 0;
    }

    public double getMach()
    {
        // Mach = v/(kRT)^0.5
        mach = getGasVelocity() / Math.Pow(kGasConstant * rGasConstant * combustionTemp, 0.5);

        return mach;
    }

    public double expensionRation()
    {
        return 0;
    }

    public double getCStar()
    {
        return 0;
    }
}



