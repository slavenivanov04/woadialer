﻿using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Calls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WoADialer.UI.Pages
{
    public sealed partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //_CurrentPhoneLine = await PhoneLine.FromIdAsync(await App.Current.CallStore.GetDefaultLineAsync());
            }
            catch
            {

            }
            lv_CallHistory.Items.Clear();
            IReadOnlyList<PhoneCallHistoryEntry> _entries = await App.Current.CallSystem.CallHistoryStore.GetEntryReader().ReadBatchAsync();
            List<PhoneCallHistoryEntry> entries = _entries.ToList();
            entries.Sort((x, y) => y.StartTime.CompareTo(x.StartTime));
            foreach (PhoneCallHistoryEntry entry in entries)
            {
                lv_CallHistory.Items.Add(new TextBlock() { Text = $"{entry.StartTime}: {entry.Address.DisplayName} {entry.Address.RawAddress} {(entry.IsMissed ? "Missed" : "")}" });
            }
        }
    }
}
