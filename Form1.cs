using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ch7Pg340_HideAndSeek
{
    public partial class Form1 : Form
    {
        private Location currentLocation;

        RoomWithHidingPlace diningRoom;
        RoomWithDoor livingRoom;
        RoomWithDoor kitchen;
        OutsideWithHidingPlace garden;
        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;

        Room stairs;
        RoomWithHidingPlace upstairsHallway;
        RoomWithHidingPlace masterBedroom;
        RoomWithHidingPlace secondBedroom;
        RoomWithHidingPlace bathroom;
        OutsideWithHidingPlace driveway;

        Opponent opponent;
        int numberOfMoves;


        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            MoveToANewLocation(livingRoom);
            ResetGame();
        }

        private void CreateObjects()
        {
            diningRoom = new RoomWithHidingPlace("Dining Room", "a crystal chandelier", "in the tall armoire");
            livingRoom = new RoomWithDoor("Living Room", "an antique carpet", "inside the closet", "an oak door with a brass knob");
            kitchen = new RoomWithDoor("Kitchen", "steel appliances", "in the cabinet", "a screen door");
            garden = new OutsideWithHidingPlace("Garden", false, "in the shed");
            frontYard = new OutsideWithDoor("Front Yard", false, "an oak door with a brass knob");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");

            stairs = new Room("Stairs", "a wooden banister");
            upstairsHallway = new RoomWithHidingPlace("Upstairs Hallway", "a picture of a dog", "a closet");
            masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed", "under the bed");
            secondBedroom = new RoomWithHidingPlace("Second Bedroom", "a small bed", "under the bed");
            bathroom = new RoomWithHidingPlace("Bathroom", "a sink and toilet", "in the shower");
            driveway = new OutsideWithHidingPlace("Driveway", false, "the garage");

            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom, stairs };
            kitchen.Exits = new Location[] { diningRoom };
            garden.Exits = new Location[] { frontYard, backYard };
            frontYard.Exits = new Location[] { backYard, garden, driveway };
            backYard.Exits = new Location[] { frontYard, garden, driveway };

            stairs.Exits = new Location[] { livingRoom, upstairsHallway };
            upstairsHallway.Exits = new Location[] { stairs, masterBedroom, secondBedroom, bathroom};
            masterBedroom.Exits = new Location[] { upstairsHallway };
            secondBedroom.Exits = new Location[] { upstairsHallway };
            bathroom.Exits = new Location[] { upstairsHallway };
            driveway.Exits = new Location[] { frontYard, backYard };

            livingRoom.DoorLocation = frontYard;
            kitchen.DoorLocation = backYard;
            frontYard.DoorLocation = livingRoom;
            backYard.DoorLocation = kitchen;
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            if (currentLocation is IHasExteriorDoor) //not necessary but added to be double safe
            {
                IHasExteriorDoor locationWithDoor = currentLocation as IHasExteriorDoor;
                MoveToANewLocation(locationWithDoor.DoorLocation);
            }
        }

        private void check_Click(object sender, EventArgs e)
        {
            numberOfMoves += 1;
            if (opponent.Check(currentLocation))
            {
                MessageBox.Show("You found me in " + numberOfMoves + " move(s)!");
                IHidingPlace locationWithHidingPlace = currentLocation as IHidingPlace;
                description.Text = "You found your opponent in the " + locationWithHidingPlace.HidingPlace + ". It took you " + numberOfMoves + " moves to find your opponent.";
                ResetGame();
            }
            else
            {
                MessageBox.Show("There's nothing here.");
            }
        }

        private void hide_Click(object sender, EventArgs e)
        {
            numberOfMoves = 0;
            for (int i = 1; i <= 10; i++)
            {
                opponent.Move();
                description.Text = i.ToString() + "...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            description.Text = "Ready or not, here I come!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            MoveToANewLocation(livingRoom);
            RedrawForm();
        }

        private void MoveToANewLocation(Location location)
        {
            currentLocation = location;
            numberOfMoves += 1;
            RedrawForm();
        }

        private void RedrawForm()
        {
            goHere.Visible = true;
            exits.Visible = true;
            hide.Visible = false;
            if (currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Visible = true;
            else
                goThroughTheDoor.Visible = false;

            exits.Items.Clear();
            foreach (Location exit in currentLocation.Exits)
                exits.Items.Add(exit.Name);
            exits.SelectedIndex = 0;
            description.Text = currentLocation.Description;

            VerifyCheckButtonVisibility();
        }

        private void VerifyCheckButtonVisibility()
        {
            if (currentLocation is IHidingPlace)
            {
                IHidingPlace locationWithHidingPlace = currentLocation as IHidingPlace;
                check.Visible = true;
                check.Text = "Check " + locationWithHidingPlace.HidingPlace;
            }
            else
                check.Visible = false;
        }

        private void ResetGame()
        {
            opponent = new Opponent(livingRoom);
            numberOfMoves = 0;
            goHere.Visible = false;
            exits.Visible = false;
            goThroughTheDoor.Visible = false;
            check.Visible = false;
            hide.Visible = true;
        }

        private void description_TextChanged(object sender, EventArgs e)
        {
            //should remove since it isn't needed
        }

        private void exits_SelectedIndexChanged(object sender, EventArgs e)
        {
            //should remove since it isn't needed
        }

    }
}
