using Xamarin.Forms;

namespace CustomControls.Extensions
{
    public static class VisualElementExtensions
    {
        private const uint AnimationLength = 300;

        public static void AnimateOpacityTo(this VisualElement visualElement, bool isFaded = false)
        {
            visualElement.IsVisible = true;
            var animation = isFaded
                ? new Animation(opacity => visualElement.Opacity = opacity, 1, 0)
                : new Animation(opacity => visualElement.Opacity = opacity, 0, 1);

            animation.Commit(visualElement, nameof(VisualElement.OpacityProperty), length: AnimationLength, finished: FinishFade);

            void FinishFade(double finalValue, bool cancelled)
                => visualElement.IsVisible = !isFaded;
        }

        public static void AnimateWidthTo(
            this VisualElement visualElement,
            double startWidth,
            double endWidth)
        {
            var animation = new Animation(width => visualElement.WidthRequest = width, startWidth, endWidth);
            animation.Commit(visualElement, nameof(VisualElement.WidthProperty), length: AnimationLength);
        }

        public static void AnimateHeightTo(
            this VisualElement visualElement,
            double startHeight,
            double endHeight)
        {
            var animation = new Animation(height => visualElement.HeightRequest = height, startHeight, endHeight);
            animation.Commit(visualElement, nameof(VisualElement.HeightProperty), length: AnimationLength);
        }
    }
}
