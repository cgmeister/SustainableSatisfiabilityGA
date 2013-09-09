using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Optimera;

class GAModel: IOptimisable
{
    public int NumberOfParameters()
    {
        return 4;
    }

    public double Fitness(double[] genes)
    {
        SetParams(genes[0], genes[1], genes[2], genes[3]);
        return -CalculateSomething();
    }

    public object DeepClone()
    {
        GAModel clone = new GAModel();
        clone.A = this.A;
        clone.B = this.B;
        clone.C = this.C;
        clone.D = this.D;
        clone.E = this.E;
        clone.F = this.F;
        return clone;
    }

    private Double A;
    private Double B;
    private Double C;
    private Double D;
    private Double E = 2;
    private Double F = 3;

    public GAModel()
    {
        E = 2.0;
        F = 3.0;
    }

    public void SetParams(Double a, Double b, Double c, Double d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public Double CalculateSomething()
    {
        for (int i = 0; i < 4e5; i++)
        {
            Int32 j = i - 25;
        }
        return(Math.Pow(0.433 - A, 2.0) +
            Math.Pow(0.533 - B, 2.0) +
            Math.Pow(0.633 - C, 2.0) +
            Math.Pow(0.733 - D, 2.0) + 
            E + F);
    }

}
