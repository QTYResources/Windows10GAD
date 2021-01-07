using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace StarFlowDemo
{
    public class StarEntity
    {
        const double LEFT = 480;

        const double TOP = 800;

        const double GONE = 480;

        private double _affinity;

        private Guid _identifier = Guid.NewGuid();

        private static Random _random = new Random();

        private Canvas _surface;

        public Canvas Surface
        {
            get { return _surface; }
        }

        private double x, y, velocity;

        private Path _starflake;

        public Guid Identifier
        {
            get { return _identifier; }
        }

        public StarEntity(Action<Path> insert)
            : this(insert, true)
        {
        }

        public StarEntity(Action<Path> insert, bool fromTop)
        {
            _starflake = StarFactory.Create();

            _affinity = _random.NextDouble();

            velocity = _random.NextDouble() * 2;
            x = _random.NextDouble() * LEFT;
            y = fromTop ? 0 : _random.NextDouble() * TOP;

            _starflake.SetValue(Canvas.LeftProperty, x);
            _starflake.SetValue(Canvas.TopProperty, y);

            insert(_starflake);

            _surface = _starflake.Parent as Canvas;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        void CompositionTarget_Rendering(object sender, object e)
        {
            _Frame();
        }

        private void _Frame()
        {
            y = y + velocity + 3.0 * _random.NextDouble() - 1.0;

            if (y > GONE)
            {
                CompositionTarget.Rendering -= CompositionTarget_Rendering;

                _surface.Children.Remove(_starflake);

                EventHandler handler = StarflakeDied;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
            else
            {

                double xFactor = 10.0 * _affinity;
                if (_affinity < 0.5) xFactor *= -1.0;
                x = x + _random.NextDouble() * xFactor;

                if (x < 0)
                {
                    x = 0;
                    _affinity = 1.0 - _affinity;
                }

                if (x > LEFT)
                {
                    x = LEFT;
                    _affinity = 1.0 - _affinity;
                }

                _starflake.SetValue(Canvas.LeftProperty, x);
                _starflake.SetValue(Canvas.TopProperty, y);
            }

            RotateTransform rotate = (RotateTransform)_starflake.GetValue(Path.RenderTransformProperty);
            rotate.Angle += _random.NextDouble() * 4.0 * _affinity;
        }

        public event EventHandler StarflakeDied;

        #region Overrides

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is StarEntity && ((StarEntity)obj).Identifier.Equals(Identifier);
        }
        #endregion
    }
}
