using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPracticum
{
    class Error
    {
        private FunctionView m_method;
        private FunctionView m_exactSolution;

        private double[] errorValues;

        public Error(FunctionView method, FunctionView exactSolution)
        {
            m_method = method;
            m_exactSolution = exactSolution;
        }

        public FunctionView GetFunctionView()
        {
            return m_method;
        }

        public void Update()
        {
            double[] expected = m_exactSolution.GetYPoints();
            double[] computed = m_method.GetYPoints();

            errorValues = new double[expected.Length];

            for (int i = 0; i < expected.Length; i++)
            {
                errorValues[i] = Math.Abs(expected[i] - computed[i]);
            }
        }

        public double[] GetErrors()
        {
            return errorValues;
        }
    }
}
