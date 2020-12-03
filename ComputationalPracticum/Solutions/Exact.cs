using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPracticum
{
    class Exact : FunctionView
    {
        protected override void FillValues()
        {
            double C = (m_y_0 + Math.Pow(m_x_0, 2) + 1) / Math.Pow(Math.E, Math.Pow(m_x_0, 2));
            for (int i = 0; i < m_N; i++)
            {
                m_view_y[i] = C * Math.Pow(Math.E, Math.Pow(m_view_x[i], 2)) - Math.Pow(m_view_x[i], 2) - 1;
            }
        }
    }
}
