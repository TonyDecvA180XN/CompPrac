using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputationalPracticum
{
    class GlobalError
    {
        private Error[] m_errors;

        public GlobalError(Error[] errors)
        {
            m_errors = errors;
        }

        public double[][] Compute(decimal n_0, decimal N, NumericUpDown n_field)
        {
            decimal oldN = n_field.Value;
            double[][] result = new double[m_errors.Length][];
            for (int i = 0; i < m_errors.Length; i++)
            {
                result[i] = new double[(int)(N - n_0 + 1)];
            }
            for (int i = (int)n_0; i <= N; i++)
            {
                n_field.Value = i;
                for (int j = 0; j < m_errors.Length; j++)
                {
                    m_errors[j].Update();
                    result[j][(int)(i - n_0)] = m_errors[j].GetErrors().Max();
                }
            }
            n_field.Value = oldN;
            return result;
        }
    }
}
