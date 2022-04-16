using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.BL
{
    public interface ICanFly
    {
        public  void Fly();
    }

    public enum Colors
    {
        Red,
        Orange,
        Black,
        Yellow
    }
   public abstract class Bird
    {
        public int NumberOfWings { get; set; } = 0;
        public Colors ColorOfBeak { get; set; }
        public abstract void Eat();
       
       
    }

    public class ostrich : Bird
    {
        public override void Eat()
        {
            // implement
        }

       
    }

    public class Eagle : Bird, ICanFly
    {
        public override void Eat()
        {
            throw new NotImplementedException();
        }

        public void Fly()
        {
            throw new NotImplementedException();
        }
    }


    public interface IEngine
    {
        public abstract string TurnOffEngine();
        public abstract string TurnOnEngine();
    }
    // is a realtion -> abstract or normal inhertiance
    // has a relationship -> Interface
    
    public abstract class Vehicle
    {
        public  int NoOfWheels { get; set; }
        public abstract string Accelarate();

        public void Blast()
        {
            // Customer 
            var x = 19;
        }

       
    }

    public class Bicycle : Vehicle
    {
      

        public override string Accelarate()
        {
            return "Bicycle Accelarting";
        }

     
    }

    public class Bike : Vehicle, IEngine
    {
        public override string Accelarate()
        {
            return "Bicycle Accelarting";
        }

        public string TurnOffEngine()
        {
            throw new NotImplementedException();
        }

        public string TurnOnEngine()
        {
            throw new NotImplementedException();
        }
    }

}
