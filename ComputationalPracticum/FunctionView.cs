using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPracticum
{

    public struct BorderRegion
    {
        // public double x_min, x_max;
        public double y_min, y_max;

        public static BorderRegion Extend(BorderRegion a, BorderRegion b)
        {
            BorderRegion r;
            r.y_min = Math.Min(a.y_min, b.y_min);
            r.y_max = Math.Max(a.y_max, b.y_max);
            return r;
        }
    }

    public class FunctionView
    {
        private readonly double LIMIT = 1000000000000000000000000.0;

        protected double m_x_0;
        protected double m_y_0;
        protected double m_X;
        protected int m_N;
        
        protected double[] m_view_x;
        protected double[] m_view_y;
        protected double m_step; 

        public FunctionView()
        {
            this.Update(0.0, 0.0, 1.0, 10);
        }

        ~FunctionView()
        {
            m_view_x = m_view_y = null;
        }

        public double[] GetXPoints()
        {
            return m_view_x;
        }

        public double[] GetYPoints()
        {
            return m_view_y;
        }

        public BorderRegion Update(double x_0, double y_0, double X, int N)
        {
            // resize array first
            if (m_N != (N+1))
            {
                m_view_x = new double[N+1];
                m_view_y = new double[N+1];
            }

            // fill x-axis
            m_step = (X - x_0) / N;
            for (int i = 0; i < N+1; i++)
            {
                m_view_x[i] = x_0 + i * m_step;
            }

            // assigning new values
            m_x_0 = x_0;
            m_y_0 = y_0;
            m_X = X;
            m_N = N+1;

            this.FillValues();

            // clamp max values and record borders
            BorderRegion borderRegion = new BorderRegion();
            for (int i = 0; i < m_N; i++)
            {
                m_view_x[i] = Math.Min(Math.Max(m_view_x[i], -LIMIT), LIMIT);
                m_view_y[i] = Math.Min(Math.Max(m_view_y[i], -LIMIT), LIMIT);

                borderRegion.y_min = Math.Min(borderRegion.y_min, m_view_y[i]);
                borderRegion.y_max = Math.Max(borderRegion.y_max, m_view_y[i]);
            }

            return borderRegion;
        }

        protected virtual void FillValues()
        {
            // For the base class we have simple line function y = x.
            for (int i = 0; i < m_N; i++)
            {
                m_view_y[i] = m_view_x[i];
            }
        }

        // f(x, y)
        public double f(double x, double y)
        {
            return 2 * x * (x * x + y);
        }
    }
}
