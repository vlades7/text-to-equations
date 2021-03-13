using Laba_WPF_4.Interfaces;
using System;
using System.Collections.Generic;

namespace Laba_WPF_4.Classes
{
    public class Calculator
    {
        public double T { get; private set; }
        public double Tmax { get; private set; }
        public double N { get; private set; }

        public List<double[]> Solve(string[] input)
        {
            IMathODE solver = new MathODE();

            var functions = new List<Func<double, double[], double>>();
            var names = new List<string>();
            var y0 = new List<double>();

            foreach (var equation in input)
            {
                solver.Parse(equation, out var func, out var name);

                functions.Add(func);
                names.Add(name);
            }

            for (var i = 0; i < names.Count; i++)
            {
                if (names[i] == "t")
                {
                    T = functions[i](0, new double[0]);
                }
                else if (names[i] == "tmax")
                {
                    Tmax = functions[i](0, new double[0]);
                }
                else if (names[i] == "n")
                {
                    N = functions[i](0, new double[0]) + 1;
                }
                else if (names[i].StartsWith("y") && names[i].Contains("_0"))
                {
                    y0.Add(functions[i](0, new double[0]));
                }
            }

            var result = solver.SolveODE(y0.ToArray(), T, Tmax, (int)N, (tt, y) =>
            {
                var fs = new List<double>();

                for (var i = 0; i < names.Count; i++)
                {
                    if (names[i].StartsWith("y") && names[i].Contains("'"))
                    {
                        fs.Add(functions[i](tt, y));
                    }
                }

                return fs.ToArray();
            });

            return result;
        }
    }
}
