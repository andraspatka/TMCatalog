using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace TMCatalog.Common.Behaviors
{
    public class TreeViewSelectedItemBehavior : Behavior<TreeView>
    {
        public static readonly DependencyProperty TreeViewSelectedItemProperty =
            DependencyProperty.RegisterAttached("TreeViewSelectedItem",
                typeof(object),
                typeof(TreeViewSelectedItemBehavior),
                null);

        public object TreeViewSelectedItem
        {
            get
            {
                return GetValue(TreeViewSelectedItemProperty);
            }

            set
            {
                SetValue(TreeViewSelectedItemProperty, value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectedItemChanged += AssociatedObject_SelectedItemChanged;

        }

        private void AssociatedObject_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.TreeViewSelectedItem = e.NewValue;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectedItemChanged -= AssociatedObject_SelectedItemChanged;
            base.OnDetaching();
        }
    }
}
