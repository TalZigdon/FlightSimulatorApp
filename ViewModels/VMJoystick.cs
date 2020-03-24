﻿using FlightSimulator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.ViewModels
{
    class VMJoystick : Notifier
    {

        public Models.Model model;
        private double _rudder;
        private double _elevator;
        private double _aileron;
        private double _throttle;
        public VMJoystick(Model model1)
        {
            this.model = model1;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM" + e.PropertyName);
            };
        }
        // insert both sliders and their property
        // create constructors
        // bind
        // where to send messages via model to simulator?

        public double VMRudder
        {
            get { return _rudder; }
            set
            {
                if (value != _rudder)
                {
                    Console.WriteLine("changed rudder in view model from" + _rudder.ToString() + "to" + value.ToString());
                    _rudder = value;
                    this.NotifyPropertyChanged("rudder");
                    model.UpdateValue("rudder", value);
                }
            }
        }

        public double VMElevator
        {
            get { return _elevator; }
            set
            {
                if (value != _elevator)
                {
                    Console.WriteLine("changed elevator in view model from" + _elevator.ToString() + "to" + value.ToString());
                    _elevator = value;
                    NotifyPropertyChanged("elevator");
                    model.UpdateValue("elevator", value);

                }
            }
        }

        public double VMAileron
        {
            get { return _aileron; }
            set
            {
                if (value != _aileron)
                {
                    Console.WriteLine("changed elevator in view model from" + _aileron.ToString() + "to" + value.ToString());
                    _aileron = value;
                    NotifyPropertyChanged("aileron");
                    model.UpdateValue("aileron", value);
                }
            }
        }

        public double VMThrottle
        {
            get { return _throttle; }
            set
            {
                if (value != _throttle)
                {
                    Console.WriteLine("changed elevator in view model from" + _throttle.ToString() + "to" + value.ToString());
                    _throttle = value;
                    NotifyPropertyChanged("throttle");
                    model.UpdateValue("throttle", value);
                }
            }
        }
    }
}
