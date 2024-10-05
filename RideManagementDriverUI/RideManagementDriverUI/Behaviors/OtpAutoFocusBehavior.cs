using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideManagementDriverUI.Behaviors
{
    public class OtpAutoFocusBehavior : Behavior<Entry>
    {
        public Entry NextEntry { get; set; }
        public Entry PreviousEntry { get; set; }

        private string _previousText = string.Empty;

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnTextChanged;
            bindable.Focused += OnFocused;  // Track focus when user taps into an Entry
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnTextChanged;
            bindable.Focused -= OnFocused;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;

            // Detect backspace press
            if (_previousText != null && e.NewTextValue.Length < _previousText.Length && string.IsNullOrEmpty(e.NewTextValue))
            {
                if (PreviousEntry != null)
                {
                    PreviousEntry.Focus();
                }
            }
            else if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length == 1)
            {
                // Move to next entry if text length is 1
                NextEntry?.Focus();
            }

            _previousText = e.NewTextValue;
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            _previousText = entry.Text;  // Store the current text when entry is focused
        }
    }
}
