using System.Runtime.CompilerServices;

namespace ThemeSelector.Controls
{
    internal sealed class RadioCheck : GraphicsView
    {
        class CircleDrawable : IDrawable
        {
            public const string DefaultTag = "None";
            public float Width;
            public float Height;
            public float StrokeThickness;
            public Color StrokeColor;
            public Color BackgroundColor;
            public object Tag = DefaultTag;

            public const float DefaultThichkess = 1.0f;

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                if (BackgroundColor != Colors.Transparent)
                {
                    App.Trace(this, nameof(BackgroundColor), BackgroundColor.Name());
                    canvas.FillColor = BackgroundColor;
                    canvas.FillRectangle(dirtyRect);
                }

                if (StrokeColor != Colors.Transparent && StrokeThickness > 0)
                {
                    App.Trace(this, nameof(Draw), "Id:{0} Color: {1} Thickness:{2}", Tag, StrokeColor.Name(), StrokeThickness);

                    double radius = (Math.Min(Width, Height) - StrokeThickness) / 2;
                    PointF center = new Point(Width / 2, Height / 2);

                    App.Trace(this, nameof(Draw), "Id:{0} {1}x{2} Radius:{3}", Tag, center.X, center.Y, radius);                        
                    canvas.StrokeColor = StrokeColor;
                    canvas.StrokeSize = StrokeThickness;
                    canvas.DrawCircle(center, radius);
                }
            }
        }

        readonly CircleDrawable _drawable;

        public RadioCheck()
        {
            _drawable = new CircleDrawable()
            {
                StrokeColor = StrokeColor,
                BackgroundColor = Colors.Transparent,
                StrokeThickness = StrokeThickness,
                Tag = "NoId"
            };
            Drawable = _drawable;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            _drawable.Width = (float) width;
            _drawable.Height = (float) height;
            base.OnSizeAllocated(width, height);
        }

        #region Properties

        void SetProperty<T>(string memberName, ref T target, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(target, value))
            {
                if (value is Color color)
                {
                    App.Trace(this, memberName, color.Name());
                }
                else if (value != null)
                {
                    App.Trace(this, memberName, value);
                }
                else
                {
                    App.Trace(this, memberName, "[NULL]");
                }
                target = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color to use to fill the drawn area.
        /// </summary>
        public Color StrokeColor
        {
            get => (Color)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }

        /// <summary>
        /// Provides the <see cref="BindableProperty"/> backing store for <see cref="StrokeColor"/>.
        /// </summary>
        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create
        (
            nameof(StrokeColor),
            typeof(Color),
            typeof(RadioCheck),
            propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (bindableObject is RadioCheck circle)
                {
                    circle.OnStrokeColorChanged((Color)newValue);
                }
            }
        );

        void OnStrokeColorChanged(Color value)
        {
            SetProperty(nameof(StrokeColor), ref _drawable.StrokeColor, value);
        }

        /// <summary>
        /// Gets or sets the color to use to fill the drawn area.
        /// </summary>
        public float StrokeThickness
        {
            get => (float)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        /// <summary>
        /// Provides the <see cref="BindableProperty"/> backing store for <see cref="StrokeThickness"/>.
        /// </summary>
        public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create
        (
            nameof(StrokeThickness),
            typeof(float),
            typeof(RadioCheck),
            CircleDrawable.DefaultThichkess,
             propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (bindableObject is RadioCheck circle)
                {
                    circle.OnStrokeThicknessChanged();
                }
            });

        void OnStrokeThicknessChanged()
        {
            SetProperty(nameof(StrokeColor), ref _drawable.StrokeThickness, StrokeThickness);
        }

        /// <summary>
        /// Gets or sets the color to use to fill the drawn area.
        /// </summary>
        public object Tag
        {
            get => (object)GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }

        /// <summary>
        /// Provides the <see cref="BindableProperty"/> backing store for <see cref="Tag"/>.
        /// </summary>
        public static readonly BindableProperty TagProperty = BindableProperty.Create
        (
            nameof(Tag),
            typeof(object),
            typeof(RadioCheck),
            CircleDrawable.DefaultThichkess,
             propertyChanged: (bindableObject, oldValue, newValue) =>
             {
                 if (bindableObject is RadioCheck circle)
                 {
                     circle._drawable.Tag = newValue ?? CircleDrawable.DefaultTag;
                 }
             });


        #endregion Properties

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (StringComparer.Ordinal.Compare(propertyName, BackgroundColorProperty.PropertyName) == 0)
            {
                SetProperty(nameof(BackgroundColor), ref _drawable.BackgroundColor, BackgroundColor);
            }
            base.OnPropertyChanged(propertyName);
        }
    }
}
