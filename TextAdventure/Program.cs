using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextAdventure
{
    class Program
    {
        
        public static int Main(string[] args)
        {
            bool exit = false;
            int currentRoomId = 0;
            int oldRoomId = 0;
            
            Room currentRoom;

            exit = Menu();
            List<Room> allrooms = createRooms();
            currentRoom = allrooms.ElementAt(currentRoomId);
            Console.WriteLine(currentRoom.description);
            while (exit == false)
            {
                exit = processCommand(Console.ReadLine(),currentRoom,ref currentRoomId);
                if ( oldRoomId != currentRoomId)
                {
                    currentRoom = allrooms.ElementAt(currentRoomId);
                    Console.WriteLine(currentRoom.description);
                    oldRoomId = currentRoomId;
                }

            }
            return 1; //exit

        }

        private static List<Room> createRooms()
        {
//            <World>
                //<Room>
//                  <North></North>
//                  <South>You go through the door to the south.</South>
//                  <East></East>
//                  <West></West>
//                  <Description>This room is small and dirty. It looks like a hallway.</Description>
//                  <RoomIds>1,1,0,1</RoomIds>
//                </Room>
//            </World>
            Console.WriteLine("Please provide the full filepath of the XML world document");
            XDocument xml = XDocument.Load(Console.ReadLine());
            Console.Clear();
            List<Room> rooms = new List<Room>();
            foreach (XElement room in xml.Descendants("Room"))
            {
                string roomIds = room.Element("RoomIds").Value;
               Room newRoom = new Room {
                   gonorth = room.Element("North").Value.ToString(),
                   goeast  = room.Element("East").Value.ToString(),
                   gosouth = room.Element("South").Value.ToString(),
                   gowest  = room.Element("West").Value.ToString(),
                   description = room.Element("Description").Value.ToString(),
                   roomIds = roomIds.Split(',').Select(x => Int32.Parse(x)).ToArray<int>(),
                   
                   
               };
               rooms.Add(newRoom);
            }
            return rooms;
        }

        private static bool processCommand(string command,Room currentRoom,ref int roomId)
        {
            bool exitgame = false;
            string outputValue;
            switch(command.ToLower())
            {
                case "go north":
                case "north":
                case "n":
                    outputValue = currentRoom.gonorth;
                    roomId = currentRoom.roomIds[0];
                    break;
                case "go south":
                case "south":
                case "s":
                    outputValue = currentRoom.gosouth;
                    roomId = currentRoom.roomIds[2];
                    break;
                case "go east":
                case "east":
                case "e":
                    outputValue = currentRoom.goeast;
                    roomId = currentRoom.roomIds[1];
                    break;
                case "go west":
                case "west":
                case "w":
                    outputValue = currentRoom.gowest;
                    roomId = currentRoom.roomIds[3];
                    break;
                case "look":
                    outputValue = currentRoom.description;
                    break;
                case "exit":
                case "quit":
                    outputValue = "Exiting Game..";
                    exitgame = true;
                    break;
                default:
                    outputValue = "Command not recognised. Please try again";
                    break;
            }
            Console.WriteLine(outputValue);
            return exitgame;
        }

    
        private static bool Menu()
        {
            int commandRecognised = 0;
            Console.WriteLine("Welcome to Text Adventure. Choose and option:\nPlay!\nQuit!");
            while (commandRecognised == 0)
            {
                switch (Console.ReadLine())
                {
                    case "play":
                        Console.Clear();
                        commandRecognised = 1;
                        break;
                    case "quit":
                    case "exit":
                        commandRecognised = 2;
                        break;
                    default:
                        Console.WriteLine("Command Not Recognised. Please Try again.");
                        break;
                }
            }

            // Exit if we've not chosen to play
            if ( commandRecognised == 1)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
    }
}
