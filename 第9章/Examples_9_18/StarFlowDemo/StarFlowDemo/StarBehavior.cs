using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StarFlowDemo
{
    public static class StarBehavior
    {
        const int CAPACITY = 75;

        private static bool beginning = false;

        private static List<StarEntity> _starflakes = new List<StarEntity>(CAPACITY);

        public static DependencyProperty AttachStarFlakeProperty = DependencyProperty.RegisterAttached(
            "AttachStar",
            typeof(bool),
            typeof(StarBehavior),
            new PropertyMetadata(false, new PropertyChangedCallback(_Attach)));

        public static bool GetAttachStarFlake(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachStarFlakeProperty);
        }

        public static void SetAttachStarFlake(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachStarFlakeProperty, value);
        }

        public static void _Attach(object sender, DependencyPropertyChangedEventArgs args)
        {
            Canvas canvas = sender as Canvas;

            if (canvas != null && args.NewValue != null && args.NewValue.GetType().Equals(typeof(bool)))
            {

                if ((bool)args.NewValue)
                {

                    if (canvas.Children.Count > 0)
                    {
                        return;
                    }

                    beginning = true;
                    for (int x = 0; x < _starflakes.Capacity; x++)
                    {
                        StarEntity starflake = new StarEntity((o) => canvas.Children.Add(o));
                        starflake.StarflakeDied += new EventHandler(Starflake_StarflakeDied);
                        _starflakes.Add(starflake);
                    }
                }
                else
                {

                    beginning = false;
                }
            }
        }

        static void Starflake_StarflakeDied(object sender, EventArgs e)
        {
            StarEntity starflake = sender as StarEntity;
            Canvas canvas = starflake.Surface;
            _starflakes.Remove(starflake);

            if (beginning)
            {
                StarEntity newFlake = new StarEntity((o) => canvas.Children.Add(o), true);
                newFlake.StarflakeDied += Starflake_StarflakeDied;
                _starflakes.Add(newFlake);
            }
        }
    }
}
