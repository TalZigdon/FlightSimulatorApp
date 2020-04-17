﻿using FlightSimulator.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FlightSimulator.ViewModels
{
	public class VMMap : INotifyPropertyChanged
	{
		readonly Models.Model model;
		private string msg;
		private bool stop = false;
		public event PropertyChangedEventHandler PropertyChanged;
		public VMMap(Models.Model model1)
		{
			model = model1;
			msg = "Plane is in bounds";
			model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM" + e.PropertyName);
			};
			VMLocation = model.Location;
			vmStatusOfServer = "server is connected";
		}

		public void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			if (name.Equals("VMLocation"))
				VMLocation = model.Location;
			if (name.Equals("VMStop"))
				VMStop = model.stop;
			if (name.Equals("VMMsg"))
				VMMsg = model.Msg;
			if (name.Equals("VMTimeout"))
			{
				VMStatusOfServer = "10 sec w/out response";
				VMStop = model.stop;
			}
		}

		//VM-massage property:
		//Send to the view massage.
		public string VMMsg
		{
			get { return msg; }
			set
			{
				if (value != msg)
				{
					msg = value;
					NotifyPropertyChanged("VMMsg");
				}
			}
		}

		//VMStop property.
		public Boolean VMStop
		{
			get
			{
				return model.stop;
			}
			set
			{
				stop = value;
				if (VMStatusOfServer != "10 sec w/out response" && value == true)
					VMStatusOfServer = "Server disconnected";
			}
		}
		private string vmStatusOfServer;

		//VMStatusOfServer property.
		public string VMStatusOfServer
		{
			get
			{
				return vmStatusOfServer;
			}
			set
			{
				if (value != vmStatusOfServer)
				{
					vmStatusOfServer = value;
					NotifyPropertyChanged("VMStatusOfServer");
				}
			}
		}

		//VMLocation property.
		public Location VMLocation
		{
			get
			{
				return model.Location;
			}
			set
			{
				model.Location = value;
			}
		}
	}
}