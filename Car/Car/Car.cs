using System;

namespace Car
{
    public enum DirOfMove
    {
        Forward,
        Back,
        Stop
    }

    public class Car
    {
        public bool EngineIsOn()
        {
            return engineIsOn;
        }

        public void TurnOnEngine()
        {
            engineIsOn = true;
        }

        public void TurnOffEngine()
        {
            if (gear != 0 || speed != 0)
            {
                throw new Exception("You cannot turn off the engine. Gear and speed must be zero");
            }
            engineIsOn = false;
        }

        public int Gear
        {
            get
            {
                return gear;
            }

            set
            {
                switch (value)
                {
                    case -1:
                        if (gear != value)
                        {
                            if (!EngineIsOn())
                            {
                                throw new Exception("You cannot set this gear because the engine is off");
                            }
                            if (Speed != 0)
                            {
                                throw new Exception("You cannot set reverse gear because the speed is not zero");
                            }
                        }
                        gear = value;
                        break;

                    case 0:
                        gear = value;
                        break;

                    case 1:
                        SetPositiveGear(value, 0, 30);
                        break;

                    case 2:
                        SetPositiveGear(value, 20, 50);
                        break;

                    case 3:
                        SetPositiveGear(value, 30, 60);
                        break;

                    case 4:
                        SetPositiveGear(value, 40, 90);
                        break;

                    case 5:
                        SetPositiveGear(value, 50, 150);
                        break;

                    default:
                        throw new Exception("No such gear");
                }
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }

            set
            {
                switch (Gear)
                {
                    case -1:
                        SetSpeed(value, 0, 20, DirOfMove.Back);
                        break;

                    case 0:
                        SetSpeed(value, 0, Speed, DirOfMove);
                        break;

                    case 1:
                        SetSpeed(value, 0, 30, DirOfMove.Forward);
                        break;

                    case 2:
                        SetSpeed(value, 20, 50, DirOfMove.Forward);
                        break;

                    case 3:
                        SetSpeed(value, 30, 60, DirOfMove.Forward);
                        break;

                    case 4:
                        SetSpeed(value, 40, 90, DirOfMove.Forward);
                        break;

                    case 5:
                        SetSpeed(value, 50, 150, DirOfMove.Forward);
                        break;
                }
            }
        }

        public DirOfMove DirOfMove { get; private set; } = DirOfMove.Stop;

        private bool engineIsOn = false;
        private int gear = 0;
        private int speed = 0;

        private void SetPositiveGear(int gear, int lowerBound, int upperBound)
        {
            if (!EngineIsOn())
            {
                throw new Exception("You cannot set this gear because the engine is off");
            }
            if (DirOfMove == DirOfMove.Back)
            {
                throw new Exception("You cannot set positive gear because the car moves back");
            }
            if (Speed < lowerBound || Speed > upperBound)
            {
                throw new Exception("You cannot set this gear because car speed outside the speed range for this gear");
            }
            this.gear = gear;
        }

        private void SetSpeed(int speed, int lowerBound, int upperBound, DirOfMove dir)
        {
            if (speed < lowerBound || speed > upperBound)
            {
                throw new Exception("You cannot set this speed because it outside the speed range for this gear");
            }
            this.speed = speed;
            if (speed == 0)
            {
                DirOfMove = DirOfMove.Stop;
            }
            else
            {
                DirOfMove = dir;
            }
        }
    }
}