using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch7Pg340_HideAndSeek
{
    class Opponent
    {
        private Location myLocation;
        private Random random;

        public Opponent(Location myLocation)
        {
            this.myLocation = myLocation;
            this.random = new Random();
        }

        public void Move()
        {
            if (myLocation is IHasExteriorDoor)
            {
                IHasExteriorDoor myLocationWithDoor = myLocation as IHasExteriorDoor;
                if (random.Next(2) == 1)
                    myLocation = (Location) myLocationWithDoor.DoorLocation;
                else
                    myLocation = myLocation.Exits[random.Next(myLocation.Exits.Length)];
            }
            else
                myLocation = myLocation.Exits[random.Next(myLocation.Exits.Length)];

            while (myLocation is IHidingPlace == false)
                myLocation = myLocation.Exits[random.Next(myLocation.Exits.Length)];
        }

        public bool Check(Location myLocaiton)
        {
            if (this.myLocation == myLocaiton)
                return true;
            else
                return false;
        }

    }
}
