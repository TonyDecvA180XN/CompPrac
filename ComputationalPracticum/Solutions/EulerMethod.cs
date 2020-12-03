using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPracticum
{
    class EulerMethod : FunctionView
    {
        protected override void FillValues() 
        {
            m_view_y[0] = m_y_0;
            for (int i = 1; i < m_N; i++)
            {
                double x = m_view_x[i - 1];
                double y = m_view_y[i - 1];
                m_view_y[i] = y + m_step * f(x, y);
            }
        }
    }
}
