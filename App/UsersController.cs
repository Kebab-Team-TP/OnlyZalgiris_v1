using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
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
            List<User> users = GetAll();
            users.Add(newUser);
            SaveUsers(users);
        }

        public static void Delete(string username)
        {
            List<User> users = GetAll();
            User userToDelete = users.FirstOrDefault(u => u.Username == username);
            if (userToDelete != null)
            {
                users.Remove(userToDelete);
                SaveUsers(users);
            }
        }

        public static void SaveUsers(List<User> users)
        {
            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText(fullPath, jsonString);
        }

        public static void UpdatePassword(string username, string newPassword)
        {
            List<User> users = GetAll();
            User userToUpdate = users.FirstOrDefault(u => u.Username == username);
            if (userToUpdate != null)
            {
                userToUpdate.Password = newPassword;
                SaveUsers(users);
            }
            else
            {
                // Handle the case where the user does not exist
                throw new Exception($"User with username '{username}' not found.");
            }
        }
    }
}