// ****************************************************************************
// <copyright file="MultiTouchManipulationBehavior.cs" company="Davide Zordan">
// Copyright © Davide Zordan 2010
// </copyright>
// ****************************************************************************
// <author>Davide Zordan</author>
// <email>info@davidezordan.net</email>
// <date>10.07.2010</date>
// <project>MultiTouch.Behaviors.Silverlight4</project>
// <web>http://multitouch.codeplex.com/</web>
// <license>
// See http://multitouch.codeplex.com/license.
// </license>
// <LastBaseLevel>BL0001</LastBaseLevel>
// ****************************************************************************

using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Input;

namespace MultiTouch.Behaviors.WPF4
{
    /// <summary>
    /// Implements Multi-Touch Manipulation
    /// </summary>
    public partial class MultiTouchBehavior
    {
        private TranslateZoomRotateBehavior _translateZoomRotateBehavior;

        private void OnAttachedImpl()
        {
            _translateZoomRotateBehavior = new TranslateZoomRotateBehavior();
            _translateZoomRotateBehavior.Attach(AssociatedObject);
            _translateZoomRotateBehavior.SupportedGestures =
                    (IsTranslateXEnabled ? ManipulationModes.Translate : ManipulationModes.None)
                    | (IsRotateEnabled ? ManipulationModes.Rotate : ManipulationModes.None)
                    | (IsScaleEnabled ? ManipulationModes.Scale : ManipulationModes.None);
            _translateZoomRotateBehavior.MaximumScale = MaximumScale;
            _translateZoomRotateBehavior.MinimumScale = MinimumScale;
            _translateZoomRotateBehavior.ConstrainToParentBounds = true;
        }

        private void OnProcessorDelta(object sender, EventArgs e)
        {

        }

        private void OnDetachingImpl()
        {
            if (_translateZoomRotateBehavior != null)
            {
                _translateZoomRotateBehavior.Detach();
            }
        }

        private static void OnIsInertiaEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Not used yet
        }

        private static void OnIsScaleEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender is MultiTouchBehavior) && (e.NewValue != null) && (((MultiTouchBehavior)sender)._translateZoomRotateBehavior != null))
            {
                ((MultiTouchBehavior)sender)._translateZoomRotateBehavior.SupportedGestures = (bool)e.NewValue ? ManipulationModes.Scale : ManipulationModes.None;
            }
        }

        private static void OnIsRotateEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender is MultiTouchBehavior) && (e.NewValue != null) && (((MultiTouchBehavior)sender)._translateZoomRotateBehavior != null))
            {
                ((MultiTouchBehavior)sender)._translateZoomRotateBehavior.SupportedGestures = (bool)e.NewValue ? ManipulationModes.Rotate : ManipulationModes.None;
            }
        }

        private static void OnIsTranslateXEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender is MultiTouchBehavior) && (e.NewValue != null) && (((MultiTouchBehavior)sender)._translateZoomRotateBehavior != null))
            {
                ((MultiTouchBehavior)sender)._translateZoomRotateBehavior.SupportedGestures = (bool)e.NewValue ? ManipulationModes.TranslateX : ManipulationModes.None;
            }
        }

        private static void OnIsTranslateYEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender is MultiTouchBehavior) && (e.NewValue != null) && (((MultiTouchBehavior)sender)._translateZoomRotateBehavior != null))
            {
                ((MultiTouchBehavior)sender)._translateZoomRotateBehavior.SupportedGestures = (bool)e.NewValue ? ManipulationModes.TranslateY : ManipulationModes.None;
            }
        }

        private static void OnMinimumScaleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender is MultiTouchBehavior) && (e.NewValue != null) && (((MultiTouchBehavior)sender)._translateZoomRotateBehavior != null))
            {
                ((MultiTouchBehavior)sender)._translateZoomRotateBehavior.MinimumScale = (int)e.NewValue;
            }
        }

        private static void OnMaximumScaleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender is MultiTouchBehavior) && (e.NewValue != null) && (((MultiTouchBehavior)sender)._translateZoomRotateBehavior != null))
            {
                ((MultiTouchBehavior)sender)._translateZoomRotateBehavior.MaximumScale = (int)e.NewValue;
            }
        }

        private bool _isDebugModeActive;
        public bool IsDebugModeActive
        {
            get
            {
                return _isDebugModeActive;
            }
            set
            {
#if DEBUG
                _isDebugModeActive = value;
#endif
            }
        }

        private bool _areFingersVisible;
        public bool AreFingersVisible
        {
            get
            {
                return _areFingersVisible;
            }
            set
            {
#if DEBUG
                _areFingersVisible = value;
#endif
            }
        }

        void TouchFrameReported(object sender, TouchFrameEventArgs e)
        {

        }
    }
}

