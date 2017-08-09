﻿using Erp.Notify;
using LbmScreenConfig;
using LittleBigMouse_Control.PluginLocation;
using LittleBigMouse_Control.Plugins.Location;

namespace LittleBigMouse_Control.Plugins.Size
{
    internal class SizePlugin : Plugin, IPluginButton, IPluginScreenControl
    {
        public override bool Init()
        {
            MainViewModel?.AddButton(this);
            return true;
        }

        public string Caption => "Size";

        public bool IsActivated
        {
            get => this.Get<bool>(); set
            {
                if (this.Set(value))
                {
                    if (value)
                    {
                        MainViewModel.Presenter.ScreenControlGetter = this;

                        _control = new LocationControlViewModel
                        {
                            Config = MainViewModel.Config,
                        };

                        MainViewModel.Control = _control;
                    }
                    else
                    {
                        if (MainViewModel.Presenter.ScreenControlGetter == this)
                            MainViewModel.Presenter.ScreenControlGetter = null;


                        if (MainViewModel.Control == _control)
                        {
                            _control = null;
                            MainViewModel.Control = _control;
                        }
                    }
                }
            }
        }

        private LocationControlViewModel _control = null;

        ScreenControlViewModel IPluginScreenControl.GetScreenControlViewModel(Screen screen) => new SizeViewModel { Screen = screen };
    }
}