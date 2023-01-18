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
        public IActionResult Simulate(SimulationRequest request)
        {
            Random random = new Random();
            int wins = 0;
            int losses = 0;

          
            for (int i = 0; i < request.SimulationCount; i++)
            {

                bool result = MontyHallPick(random.Next(3), request.SwitchDoor, random.Next(3), random.Next(1));

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

            
            int leftGoat = 0;
            int rightGoat = 2;
            switch (pickedDoor)
            {
                case 0: leftGoat = 1; rightGoat = 2; break;
                case 1: leftGoat = 0; rightGoat = 2; break;
                case 2: leftGoat = 0; rightGoat = 1; break;
            }

            int keepGoat = goatDoorToRemove == 0 ? rightGoat : leftGoat;

           
            if (changeDoor == 0)
            {
                
                win = carDoor == pickedDoor;
            }
            else
            {
                
                win = carDoor != keepGoat;
            }

            return win;
        }

    }

}

