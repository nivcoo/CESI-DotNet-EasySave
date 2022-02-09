﻿using MainApplication.Objects.Enums;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace GuiApplication.Converter;

public class StateConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		State state = (State)Enum.Parse(typeof(State), value.ToString());
		string param = parameter.ToString();
		switch (state)
		{
			case State.Active:
				if(param == "pause")
					return Visibility.Visible;
				else
					return Visibility.Collapsed;
			case State.Pause:
				if (param == "resume" || param == "play")
					return Visibility.Visible;
				else
					return Visibility.Collapsed;
			case State.End:
				if (param == "pause" || param == "resume")
					return Visibility.Collapsed;
				else
					return Visibility.Visible;
		}
		return Visibility.Collapsed;
	}

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
		State state = (State)Enum.Parse(typeof(State), value.ToString());

		if (state == State.Active)
			return "Active";
		else if (state == State.Pause)
			return "Pause";
		else
			return "End";
	}
}

