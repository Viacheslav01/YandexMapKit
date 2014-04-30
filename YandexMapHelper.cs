using System.Windows;
using Yandex.Maps;

namespace YandexMapKit
{
	public static class YandexMapHelper
	{
		public static readonly DependencyProperty FixManipulationProperty = DependencyProperty.RegisterAttached(
			"FixManipulation", typeof(bool), typeof(YandexMapHelper), new PropertyMetadata(OnFixManipulationChanged));

		public static void SetFixManipulation(DependencyObject element, bool value)
		{
			element.SetValue(FixManipulationProperty, value);
		}

		public static bool GetFixManipulation(DependencyObject element)
		{
			return (bool) element.GetValue(FixManipulationProperty);
		}

		private static void OnFixManipulationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var map = d as Map;
			if (map == null)
			{
				return;
			}

			var fixManipulation = (bool?) e.NewValue;

			if (fixManipulation == true)
			{
				map.UseOptimizedManipulationRouting = false;
				
				map.ManipulationStarted += MapManipulationStarted;
				map.ManipulationCompleted += MapManipulationCompleted;
				map.ManipulationDelta += MapManipulationDelta;

				return;
			}

			fixManipulation = (bool?)e.OldValue;

			if (fixManipulation == true)
			{
				map.UseOptimizedManipulationRouting = true;

				map.ManipulationStarted -= MapManipulationStarted;
				map.ManipulationCompleted -= MapManipulationCompleted;
				map.ManipulationDelta -= MapManipulationDelta;
			}
		}

		private static void MapManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
		{
			e.Handled = true;
		}

		private static void MapManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
		{
			e.Handled = true;
		}

		private static void MapManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
		{
			e.Handled = true;
		}
	}
}
