using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPracticum
{
    class RungeKutta : FunctionView
    {
        protected override void FillValues() 
        {
            m_view_y[0] = m_y_0;
            for (int i = 1; i < m_N; i++)
            {
                double x = m_view_x[i - 1];
                double y = m_view_y[i - 1];
                double k1 = f(x, y);
                double k2 = f(x + m_step / 2, y + m_step * k1 / 2);
                double k3 = f(x + m_step / 2, y + m_step * k2 / 2);
                double k4 = f(x + m_step, y + m_step * k3);

                m_view_y[i] = y + m_step * (k1 + 2 * k2 + 2 * k3 + k4) / 6;
            }
        }
    }
}
