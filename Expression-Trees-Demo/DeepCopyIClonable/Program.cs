using System;

namespace DeepCopyICloneable
{

    public class Shoe : ICloneable
    {
        public string Color;
        public object Clone()
        {
            Shoe newShoe = new Shoe();
            newShoe.Color = Color.Clone() as string;
            return newShoe;
        }
    }

    public class Dude
    {
        public string Name;
        public Shoe RightShoe;
        public Shoe LeftShoe;
        
        public Dude CopyDude()
        {
            Dude newPerson = new Dude();
            newPerson.Name = Name;
            newPerson.LeftShoe = LeftShoe.Clone() as Shoe;
            newPerson.RightShoe = RightShoe.Clone() as Shoe;
            return newPerson;
        }

        public override string ToString()
        {
            return (Name + " : Dude!, I have a " + RightShoe.Color + " shoe on my right foot, and a " + LeftShoe.Color + " on my left foot.");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Dude Bill = new Dude();
            Bill.Name = "Bill";
            Bill.LeftShoe = new Shoe();
            Bill.RightShoe = new Shoe();
            Bill.LeftShoe.Color = Bill.RightShoe.Color = "Blue";
            Dude Ted = Bill.CopyDude() as Dude;
            Ted.Name = "Ted";
            Ted.LeftShoe.Color = Ted.RightShoe.Color = "Red";
            Console.WriteLine(Bill.ToString());
            Console.WriteLine(Ted.ToString());
        }
    }
}
