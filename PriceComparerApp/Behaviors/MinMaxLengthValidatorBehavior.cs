using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PriceComparerApp.Behaviors
{
    public class MinMaxLengthValidatorBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MinMaxLengthValidatorBehavior), 0);
        public static readonly BindableProperty MinLengthProperty = BindableProperty.Create("MinLength", typeof(int), typeof(MinMaxLengthValidatorBehavior), 0);

        public bool isValidate = false;
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public int MinLength
        {
            get { return (int)GetValue(MinLengthProperty); }
            set { SetValue(MinLengthProperty, value); }
        }

        /// <summary>
        /// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(`0)" /> method on this object.
        /// </summary>
        /// <param name="bindable">The bindable object to which the behavior was attached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += bindable_TextChanged;
        }

        /// <summary>
        /// Handles the TextChanged event of the bindable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= MaxLength)
            {
                ((Entry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
                isValidate = false;
            }

            if (e.NewTextValue.Length < MinLength)
            {
                ((Entry)sender).TextColor = Color.Red;
                isValidate = false;
            }
            else
            {
                ((Entry)sender).TextColor = Color.Default;
                isValidate = true;
            }
        }

        /// <summary>
        /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(`0)" /> method and then detaches from the superclass.
        /// </summary>
        /// <param name="bindable">The bindable object from which the behavior was detached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= bindable_TextChanged;

        }
    }
}
