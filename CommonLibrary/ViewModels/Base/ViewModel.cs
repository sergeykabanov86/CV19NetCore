﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CommonLibrary.ViewModels.Base
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool SetProperty<T>(ref T Field, T Value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(Field, Value)) return false;

            Field = Value;
            OnPropertyChanged(PropertyName);
            return false;
        }
    }
}