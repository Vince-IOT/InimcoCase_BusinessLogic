using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using BusinessLogic.Data;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

namespace BusinessLogic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }

        int AmountVowels = 0;
        int AmountConstants = 0;

        [HttpPost]
        public JsonResult CreateUser (UsersInformation Information)
        {
            // Make Full name from First and Last name
            string FullName = (Information.FirstName + " " + Information.LastName).ToLower();
            Console.WriteLine(FullName);

            // Check for every charachter in full name
            foreach (char c in FullName)
            {
                // Regex match lowercase a-z
                var regexItem = new Regex("^[a-z]*$");
                // Check if Regex is a match 
                if (!regexItem.IsMatch(c.ToString())) {
                    continue; // <-- If match = skip with continue
                }else
                {
                    // Vowel checker
                    bool isVowel = "aeiou".IndexOf(c) >= 0;
                    if (isVowel) // <-- If vowel
                    {
                        AmountVowels++;
                    }
                    else
                    {
                        AmountConstants++;
                    }
                }
            }

            Console.WriteLine("Amount of vowels: " + AmountVowels);
            Console.WriteLine("Amount of constants: " + AmountConstants);

            // Place full name in a array
            char[] charArray = FullName.ToCharArray();
            Array.Reverse(charArray); // Reverse array
            Console.WriteLine(charArray); // Print array

            if (Information.Id == 0)
            {
                _context.Users.Add(Information);
            } else
            {
                var UserInDb = _context.Users.Find(Information.Id);

                if (UserInDb == null)
                    return new JsonResult(NotFound());

                UserInDb = Information;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(Information));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Users.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Users.ToList();

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Users.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Users.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
