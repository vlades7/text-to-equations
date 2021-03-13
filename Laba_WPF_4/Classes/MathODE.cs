using Laba_WPF_4.Interfaces;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.OdeSolvers;
using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba_WPF_4.Classes
{
    public class MathODE : IMathODE
    {
        public List<double[]> SolveODE(double[] y0, double startTime, double endTime, int n, Func<double, double[], double[]> func)
        {
            var y0Vector = Vector<double>.Build.DenseOfArray(y0);

            var result = RungeKutta.FourthOrder(y0Vector, startTime, endTime, n, (t, y) =>
            {
                var res = func(t, y.AsArray());
                return Vector<double>.Build.DenseOfArray(res);
            });
            var resultList = new List<double[]>();

            for (var i = 0; i < n; i++)
            {
                var vector = result[i];
                resultList.Add(vector.AsArray());
            }

            return resultList;
        }

        public bool Parse(string equation, out Func<double, double[], double> func, out string varName)
        {
            func = (t, y) => 0.0;
            varName = "";

            var equalsIdx = equation.IndexOf("=", StringComparison.Ordinal);
            if (equalsIdx == -1) return false;

            var leftSide = equation.Substring(0, equalsIdx).Trim();
            var rightSide = equation.Substring(equalsIdx + 1).Trim();

            var parsed = SymbolicExpression.Parse(rightSide);

            if (!leftSide.Contains("'"))
            {
                func = (t, y) => parsed.RealNumberValue;
            }
            else
            {
                var variables = parsed.CollectVariables();
                var variablesStrings = variables.Select(variable => variable.ToString()).ToList();
                var parsedFunc = parsed.Compile(variablesStrings.ToArray());

                func = (t, y) =>
                {
                    var args = new List<object>();

                    if (variablesStrings.Contains("t")) args.Add(t);
                    args.AddRange(y.Where((t1, i) => variablesStrings.Contains("y" + (i + 1))).Cast<object>());

                    return (double)parsedFunc.DynamicInvoke(args.ToArray());
                };
            }

            varName = leftSide;

            return true;
        }
    }
}
