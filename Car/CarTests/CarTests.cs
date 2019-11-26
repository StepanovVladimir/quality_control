using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Car.Tests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void TurnOnEngine_WhenEngineIsOff_EngineHasTurnedOn()
        {
            Car car = new Car();

            car.TurnOnEngine();

            Assert.AreEqual(car.EngineIsOn(), true);
        }

        [TestMethod]
        public void SetGear_ReverseWhenEngineIsOff_ThrowsException()
        {
            Car car = new Car();

            Action action = () => car.Gear = -1;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_PositiveWhenEngineIsOff_ThrowsException()
        {
            Car car = new Car();

            Action action = () => car.Gear = 1;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_ReverseOnSpeedEqualZero_GearHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            int reverseGear = -1;

            car.Gear = reverseGear;

            Assert.AreEqual(car.Gear, reverseGear);
        }

        [TestMethod]
        public void SetSpeed_OutOfReverseGearMax_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            int outOfMaxSpeed = 21;

            Action action = () => car.Speed = outOfMaxSpeed;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetSpeed_MaxOfReverseGear_SpeedHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            int maxSpeed = 20;

            car.Speed = maxSpeed;

            Assert.AreEqual(car.Speed, maxSpeed);
        }

        [TestMethod]
        public void SetSpeed_OnReverseGear_CarMovesBack()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;

            car.Speed = 1;

            Assert.AreEqual(car.DirOfMove, DirOfMove.Back);
        }

        [TestMethod]
        public void SetSpeed_Negative_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;

            Action action = () => car.Speed = -1;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_OutOfMinGear_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            car.Speed = 20;
            int outOfMinGear = -2;

            Action action = () => car.Gear = outOfMinGear;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_NeutralWhenCarMovesBack_GearHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            car.Speed = 1;
            int neutralGear = 0;

            car.Gear = neutralGear;

            Assert.AreEqual(car.Gear, neutralGear);
        }

        [TestMethod]
        public void SetGear_PositiveWhenCarMovesBack_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            car.Speed = 1;
            car.Gear = 0;

            Action action = () => car.Gear = 1;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_ReverseWhenSpeedNotZero_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            car.Speed = 1;
            car.Gear = 0;

            Action action = () => car.Gear = -1;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetSpeed_LessThanCurrentOnNeutralGearWhenCarMovesBack_CarStillMoveBack()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            car.Speed = 20;
            car.Gear = 0;
            int speedLessThanCurrent = 1;

            car.Speed = speedLessThanCurrent;

            Assert.AreEqual(car.DirOfMove, DirOfMove.Back);
        }

        [TestMethod]
        public void SetSpeed_MoreThanCurrentOnNeutralGear_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            car.Speed = 1;
            car.Gear = 0;
            int speedMoreThanCurrent = 2;

            Action action = () => car.Speed = speedMoreThanCurrent;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetSpeed_ZeroWhenCarMovesBack_CarHasStopped()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = -1;
            car.Speed = 1;

            car.Speed = 0;

            Assert.AreEqual(car.DirOfMove, DirOfMove.Stop);
        }

        [TestMethod]
        public void SetGear_FirstOnSpeedEqualZero_GearHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            int firstGear = 1;

            car.Gear = firstGear;

            Assert.AreEqual(car.Gear, firstGear);
        }

        [TestMethod]
        public void SetSpeed_OutOfFirstGearMax_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            int outOfMaxSpeed = 31;

            Action action = () => car.Speed = outOfMaxSpeed;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetSpeed_MaxOfFirstGear_SpeedHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            int maxSpeed = 30;

            car.Speed = maxSpeed;

            Assert.AreEqual(car.Speed, maxSpeed);
        }

        [TestMethod]
        public void SetSpeed_OnPositiveGear_CarMovesForward()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;

            car.Speed = 1;

            Assert.AreEqual(car.DirOfMove, DirOfMove.Forward);
        }

        [TestMethod]
        public void SetSpeed_LessThanCurrentOnNeutralGearWhenCarMovesForward_CarStillMoveForward()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 0;
            int speedLessThanCurrent = 1;

            car.Speed = speedLessThanCurrent;

            Assert.AreEqual(car.DirOfMove, DirOfMove.Forward);
        }

        [TestMethod]
        public void SetSpeed_ZeroWhenCarMovesForward_CarHasStopped()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 1;

            car.Speed = 0;

            Assert.AreEqual(car.DirOfMove, DirOfMove.Stop);
        }

        [TestMethod]
        public void SetGear_SecondOnSpeedOutOfMin_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 19;

            Action action = () => car.Gear = 2;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_SecondOnMinSpeed_GearHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 20;
            int secondGear = 2;

            car.Gear = secondGear;

            Assert.AreEqual(car.Gear, secondGear);
        }

        [TestMethod]
        public void SetSpeed_OutOfSecondGearMax_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            int outOfMaxSpeed = 51;

            Action action = () => car.Speed = outOfMaxSpeed;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetSpeed_MaxOfSecondGear_SpeedHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            int maxSpeed = 50;

            car.Speed = maxSpeed;

            Assert.AreEqual(car.Speed, maxSpeed);
        }

        [TestMethod]
        public void SetGear_ThirdOnSpeedOutOfMin_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 29;

            Action action = () => car.Gear = 3;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_ThirdOnMinSpeed_GearHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            int thirdGear = 3;

            car.Gear = thirdGear;

            Assert.AreEqual(car.Gear, thirdGear);
        }

        [TestMethod]
        public void SetSpeed_OutOfThirdGearMax_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 3;
            int outOfMaxSpeed = 61;

            Action action = () => car.Speed = outOfMaxSpeed;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetSpeed_MaxOfThirdGear_SpeedHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 3;
            int maxSpeed = 60;

            car.Speed = maxSpeed;

            Assert.AreEqual(car.Speed, maxSpeed);
        }

        [TestMethod]
        public void SetGear_FourthOnSpeedOutOfMin_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            car.Speed = 39;

            Action action = () => car.Gear = 4;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_FourthOnMinSpeed_GearHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            car.Speed = 40;
            int fourthGear = 4;

            car.Gear = fourthGear;

            Assert.AreEqual(car.Gear, fourthGear);
        }

        [TestMethod]
        public void SetSpeed_OutOfFourthGearMax_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            car.Speed = 40;
            car.Gear = 4;
            int outOfMaxSpeed = 91;

            Action action = () => car.Speed = outOfMaxSpeed;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetSpeed_MaxOfFourthGear_SpeedHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            car.Speed = 40;
            car.Gear = 4;
            int maxSpeed = 90;

            car.Speed = maxSpeed;

            Assert.AreEqual(car.Speed, maxSpeed);
        }

        [TestMethod]
        public void SetGear_FifthOnSpeedOutOfMin_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            car.Speed = 49;

            Action action = () => car.Gear = 5;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetGear_FifthOnMinSpeed_GearHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            car.Speed = 50;
            int fifthGear = 5;

            car.Gear = fifthGear;

            Assert.AreEqual(car.Gear, fifthGear);
        }

        [TestMethod]
        public void SetSpeed_OutOfFifthGearMax_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            car.Speed = 50;
            car.Gear = 5;
            int outOfMaxSpeed = 151;

            Action action = () => car.Speed = outOfMaxSpeed;

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void SetSpeed_MaxOfFifthGear_SpeedHasSet()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 30;
            car.Gear = 2;
            car.Speed = 50;
            car.Gear = 5;
            int maxSpeed = 150;

            car.Speed = maxSpeed;

            Assert.AreEqual(car.Speed, maxSpeed);
        }

        [TestMethod]
        public void TurnOffEngine_WhenGearNotNeutral_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;

            Action action = () => car.TurnOffEngine();

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void TurnOffEngine_WhenSpeedNotZero_ThrowsException()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.Gear = 1;
            car.Speed = 1;
            car.Gear = 0;

            Action action = () => car.TurnOffEngine();

            Assert.ThrowsException<Exception>(action);
        }

        [TestMethod]
        public void TurnOffEngine_WhenGearIsNeutralAndSpeedIsZero_EngineHasTurnedOff()
        {
            Car car = new Car();
            car.TurnOnEngine();

            car.TurnOffEngine();

            Assert.AreEqual(car.EngineIsOn(), false);
        }
    }
}