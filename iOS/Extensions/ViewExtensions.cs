using System;
using System.Linq;
using UIKit;

namespace MySpectrumCodingTest.iOS.Extensions
{
    public static class ViewExtensions
    {
        public static UIView FindFirstResponder(this UIView view)
        {
            if (view.IsFirstResponder)
            {
                return view;
            }

            return view.Subviews.Select(subView => subView.FindFirstResponder()).FirstOrDefault(firstResponder => firstResponder != null);
        }
        public static UIView FindSuperviewOfType(this UIView view, UIView stopAt, Type type)
        {
            if (view.Superview != null)
            {
                if (type.IsInstanceOfType(view.Superview))
                {
                    return view.Superview;
                }

                if (!Equals(view.Superview, stopAt))
                {
                    return view.Superview.FindSuperviewOfType(stopAt, type);
                }
            }

            return null;
        }
    }
}
