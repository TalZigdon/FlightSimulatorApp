﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    class Model : Notifier
    {
        public double XairPlaneLocation;
        public double YairPlaneLocation;
        public double [] valuesFromView = new double[4];
        public volatile bool stop = false;
        private double xPos;
        public double XPos
        {
            get
            {
                return xPos;
            }
            set
            {
                if (xPos != value)
                {
                    xPos = value;
                    this.NotifyPropertyChanged("XPos");
                }
            }
        }
        private double yPos;
        public double YPos
        {
            get
            {
                return yPos;
            }
            set
            {
                if (yPos != value)
                {
                    yPos = value;
                    this.NotifyPropertyChanged("YPos");
                }
            }
        }
        private double verticalSpeed;
        public double VerticalSpeed
        {
            get 
            {
                return verticalSpeed;
            }
            set
            {
                if (verticalSpeed != value)
                {
                    verticalSpeed = value;
                    this.NotifyPropertyChanged("VerticalSpeed");
                }
            }
        }
        private double headingDeg;
        public double HeadingDeg
        {
            get
            {
                return headingDeg;
            }
            set
            {
                if (headingDeg != value)
                {
                    headingDeg = value;
                    this.NotifyPropertyChanged("HeadingDeg");
                }
            }
        }
        private double groundSpeedKt;
        public double GroundSpeedKt
        {
            get
            { 
                return groundSpeedKt;
            }
            set
            {
                if (groundSpeedKt != value)
                {
                    groundSpeedKt = value;
                    this.NotifyPropertyChanged("GroundSpeedKt");
                }
            }
        }
        private double indicatedSpeedKt;
        public double IndicatedSpeedKt
        {
            get
            {
                return indicatedSpeedKt;
            }
            set
            {
                if (indicatedSpeedKt != value)
                {
                    indicatedSpeedKt = value;
                    this.NotifyPropertyChanged("IndicatedSpeedKt");
                }
            }
        }
        private double altitudeFt;
        public double AltitudeFt
        {
            get
            {
                return altitudeFt;
            }
            set
            {
                if (altitudeFt != value)
                {
                    altitudeFt = value;
                    this.NotifyPropertyChanged("AltitudeFt");
                }
            }
        }
    private double rollDeg;
    public double RollDeg
    {
        get
        {
            return rollDeg;
        }
        set
        {
                if (rollDeg != value)
                {
                    rollDeg = value;
                    this.NotifyPropertyChanged("RollDeg");
                }
        }
    }
        private double pitchDeg;
        public double PitchDeg
        {
            get
            {
                return pitchDeg;
            }
            set
            {
                if (pitchDeg != value)
                {
                    pitchDeg = value;
                    this.NotifyPropertyChanged("PitchDeg");
                }
            }
        }
        private double indicatedAltitudeFt;
        public double IndicatedAlitudeFt
        {
            get
            {
                return indicatedAltitudeFt;
            }
            set
            {
                if (indicatedAltitudeFt != value)
                {
                    indicatedAltitudeFt = value;
                    this.NotifyPropertyChanged("IndicatedAltitudeFt");
                }
            }
        }

        MyTcpClient myClient = new MyTcpClient();
        public void Connect(string ip, int port)
        {
            myClient.Connect(ip, port);
            this.Start();
        }
        public void Start()
        {
            Thread thread = new Thread(StartReadAndWrite);
            thread.Start();
        }
        private void StartReadAndWrite()
        {
            while (!stop)
            {
                //values from the server
                myClient.write("get /instrumentation/heading-indicator/indicated-heading-deg");
                HeadingDeg = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/gps/indicated-vertical-speed");
                VerticalSpeed = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/gps/indicated-ground-speed-kt");
                GroundSpeedKt = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt");
                IndicatedSpeedKt = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/gps/indicated-altitude-ft");
                AltitudeFt = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/attitude-indicator/indicated-roll-deg");
                RollDeg = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/attitude-indicator/indicated-pitch-deg");
                PitchDeg = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/altimeter/indicated-altitude-ft");
                IndicatedAlitudeFt = Double.Parse(myClient.read());
                myClient.write("get /position/latitude-deg");
                XPos = Double.Parse(myClient.read());
                myClient.write("get /position/longitude-deg");
                YPos = Double.Parse(myClient.read());

                //values from the view that we need to update
                myClient.write("set /controls/flight/rudder" + valuesFromView[0].ToString());
                myClient.write("set /controls/flight/elevator" + valuesFromView[1].ToString());
                myClient.write("set /controls/engines/current-engine/throttle" + valuesFromView[2].ToString());
                myClient.write("set /controls/flight/aileron" + valuesFromView[3].ToString());
                //location of the airplane

                
            }
        }
        public void UpdateValue(String info, double newVal)
        {
            if(info == "rudder")
            {
                valuesFromView[0] = UpdateRudder(newVal);
            }
            if(info == "elevator")
            {
                valuesFromView[1] = UpdateElevator(newVal);
            }
            if(info == "throttle")
            {
                valuesFromView[2] = UpdateThrottle(newVal);
            }
            if(info == "aileron")
            {
                valuesFromView[3] = UpdateAileron(newVal);
            }
        }

        private double UpdateRudder(double newVal)
        {
            if(newVal > 1)
            {
                return 1;
            }
            if(newVal < -1)
            {
                return -1;
            }
            return newVal;
        }

        private double UpdateElevator(double newVal)
        {
            {
                if (newVal > 1)
                {
                    return 1;
                }
                if (newVal < -1)
                {
                    return -1;
                }
                return newVal;
            }
        }

        private double UpdateThrottle(double newVal)
        {
            {
                if (newVal > 1)
                {
                    return 1;
                }
                if (newVal < -1)
                {
                    return -1;
                }
                return newVal;
            }
        }

        private double UpdateAileron(double newVal)
        {
            {
                if (newVal > 1)
                {
                    return 1;
                }
                if (newVal < 0)
                {
                    return 0;
                }
                return newVal;
            }
        }
    }
}
