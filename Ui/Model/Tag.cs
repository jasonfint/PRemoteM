﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using PRM.Service;
using Shawn.Utils;
using Shawn.Utils.Wpf.FileSystem;

namespace PRM.Model
{
    public class Tag : NotifyPropertyChangedBase
    {
        private readonly Action _saveOnPinnedChanged;
        public Tag(string name, bool isPinned, Action saveOnPinnedChanged)
        {
            _name = name;
            _isPinned = isPinned;
            this._saveOnPinnedChanged = saveOnPinnedChanged;
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set => SetAndNotifyIfChanged(ref _name, value);
        }


        private int _itemsCount = 0;
        public int ItemsCount
        {
            get => _itemsCount;
            set => SetAndNotifyIfChanged(ref _itemsCount, value);
        }

        private bool _isPinned = false;
        public bool IsPinned
        {
            get => _isPinned;
            set
            {
                SetAndNotifyIfChanged(ref _isPinned, value);
                _saveOnPinnedChanged.Invoke();
            }
        }

        private Visibility _objectVisibility = Visibility.Visible;
        [JsonIgnore]
        public Visibility ObjectVisibility
        {
            get => _objectVisibility;
            set => SetAndNotifyIfChanged(ref _objectVisibility, value);
        }
    }
}
