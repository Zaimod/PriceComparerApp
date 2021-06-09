using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PriceComparerApp.Behaviors
{
    public class NumberValidationBehavior : Behavior<Entry>
    {
        /// <summary>
        /// Called when [attached to].
        /// </summary>
        /// <param name="entry">The entry.</param>
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        /// <summary>
        /// Called when [detaching from].
        /// </summary>
        /// <param name="entry">The entry.</param>
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        /// <summary>
        /// Called when [entry text changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            int result;

            bool isValid = int.TryParse(args.NewTextValue, out result);

            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
