using System;
using System.Collections.Generic;

namespace Laba_WPF_4.Interfaces
{
    interface IMathODE
    {
        List<double[]> SolveODE(double[] y0, double startTime, double endTime, int n, Func<double, double[], double[]> func);
        bool Parse(string equation, out Func<double, double[], double> func, out string varName);
    }
}
