using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text.Json;
using Zalgiris.Models;

namespace Zalgiris.App
{
    public class UsersController
    {

        static string relativePath = "App_Data/Users.json";
        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string fullPath = Path.Combine(currentDirectory, relativePath);
        public static List<User> GetAll()
        {
            try
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    return JsonSerializer.Deserialize<List<User>>(reader.ReadToEnd());

                }
            }
            catch
            {
                return new List<User>();
            }
        }
        public static void Add(User newUser)
        {
            List<User> users = new List<User>();
            users = GetAll();
            users.Add(newUser);
            string jsonString = JsonSerializer.Serialize(users);
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(jsonString);
            }
            
        }
    }
}