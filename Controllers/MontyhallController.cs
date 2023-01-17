using Microsoft.AspNetCore.Mvc;
using MontyhallBackend.Models;
using System;

namespace MontyhallBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontyhallController : ControllerBase
    {

        [HttpPost("Simulate")]
        public IActionResult Simulate(Montyhall montyhall)
        {
            Random random = new Random();
            int wins = 0;
            int losses = 0;

            // iterate our MontyHall routine
            for (int i = 0; i < montyhall.SimulationCount; i++)
            {

                bool result = MontyHallPick(random.Next(3), montyhall.SwitchDoor, random.Next(3), random.Next(1));

                if (result)
                {
                    wins++;
                }
                else
                {
                    losses++;
                }
            }

            return Ok(new { wins });
        }

        public static bool MontyHallPick(int pickedDoor, int changeDoor, int carDoor, int goatDoorToRemove)
        {
            bool win = false;

            // randomly remove one of the *goat* doors,
            // but not the "contestants picked" ONE!
            int leftGoat = 0;
            int rightGoat = 2;
            switch (pickedDoor)
            {
                case 0: leftGoat = 1; rightGoat = 2; break;
                case 1: leftGoat = 0; rightGoat = 2; break;
                case 2: leftGoat = 0; rightGoat = 1; break;
            }

            int keepGoat = goatDoorToRemove == 0 ? rightGoat : leftGoat;

            // would the contestant win with the switch or the stay?
            if (changeDoor == 0)
            {
                // not changing the initially picked door
                win = carDoor == pickedDoor;
            }
            else
            {
                // changing picked door to the other door remaining
                win = carDoor != keepGoat;
            }

            return win;
        }

    }

}

