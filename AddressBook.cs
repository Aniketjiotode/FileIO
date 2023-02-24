using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace AddressBook_System
{
    public class AddressBook
    {
        public List<Contact> contacts;
        public string AddressBook_Name;
        Dictionary<string, string> citys;
        Dictionary<string, string> states;
        public AddressBook(string addressbookName)
        {
            contacts = new List<Contact>();
            citys = new Dictionary<string, string>();
            states = new Dictionary<string, string>();
            AddressBook_Name = addressbookName;
        }
        public Contact AddToContact()
        {
            Contact contact = new Contact();
            Console.WriteLine("Enter FirstName:");
            var name = Console.ReadLine();
            contact.FirstName = name;
            Console.WriteLine("Enter LastName:");
            var lastname = Console.ReadLine();
            contact.LastName = lastname;
            Console.WriteLine("Enter Address");
            var address = Console.ReadLine();
            contact.Address = address;
            Console.WriteLine("Enter City");
            var city = Console.ReadLine();
            contact.City = city;
            Console.WriteLine("Enter State");
            var state = Console.ReadLine();
            contact.State = state;
            Console.WriteLine("Enter Email Id");
            var email = Console.ReadLine();
            contact.Email = email;
            Console.WriteLine("Enter Zipcode");
            contact.ZipCode = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Phone Number");
            contact.PhoneNumber = int.Parse(Console.ReadLine());
            contacts.Add(contact);
            citys.Add(name, city);
            states.Add(name, state); ;
            return contact;
        }
        public void Display()
        {
            Console.WriteLine("Enter 1 to print all data \nEnter 2 to view persons by state or city\nEnter 3 to get number persons by state or city\nEnter 4 to print entry in alphabetical order\nEnter 5 to sort entries by city,state and zipcode");
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    if (contacts.Count <= 0)
                    {
                        Console.WriteLine("No contacts available");
                    }
                    else
                    {
                        foreach (var contact in contacts)
                        {
                            Console.WriteLine("AddressBook Name: " + AddressBook_Name + "\n FirstName: " + contact.FirstName + "\n LastName:" + contact.LastName + "\n Address: " + contact.Address + "\n City: " + contact.City + "\n State: " + contact.State + "\n Email Id" + contact.Email + "\n ZipCode: " + contact.ZipCode + "\n Phone number: " + contact.PhoneNumber);
                        }
                    }
                    return;
                    break;
                    
                case 2:
                    Console.WriteLine("Enter 1 to view person by city\nEnter 2 to view persons by state");
                    int input2 = int.Parse(Console.ReadLine());
                    switch (input2)
                    {
                        case 1:
                            if (citys.Count <= 0)
                            {
                                Console.WriteLine("No contacts available");
                            }
                            else
                            {
                                foreach (var c in citys)
                                {
                                    Console.WriteLine(c);
                                    return;
                                }
                            }
                            break;
                        case 2:
                            if (states.Count <= 0)
                            {
                                Console.WriteLine("No contacts available");
                            }
                            else
                            {
                                foreach (var s in states)
                                {
                                    Console.WriteLine(s);
                                    return;
                                }
                            }
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter 1 to get number of person by city\nEnter 2 to get number of persons by state");
                    int input3 = int.Parse(Console.ReadLine());
                    switch (input3)
                    {
                        case 1:
                            Console.WriteLine("Enter the city name");
                            var city = Console.ReadLine();
                            int count = 0;
                            foreach (var c in citys)
                            {
                                if (c.Value.Equals(city))
                                {
                                    count++;
                                }
                            }
                            if (count == 0)
                            {
                                Console.WriteLine($"No person of city {city} is present");
                                return;
                            }
                            else
                            {
                                Console.WriteLine($"{count} person of city is present in contact");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter the state name");
                            var state = Console.ReadLine();
                            int count2 = 0;
                            foreach (var s in states)
                            {
                                if (s.Value.Equals(state))
                                {
                                    count2++;
                                }
                            }
                            if (count2 == 0)
                            {
                                Console.WriteLine($"No person of city {state} is present");
                            }
                            else
                            {
                                Console.WriteLine($"{count2} person of city is present in contact");
                                return;
                            }
                            break;
                    }
                    break;
                case 4:
                    if (contacts.Count <= 0)
                    {
                        Console.WriteLine("No contacts available");
                    }
                    else
                    {
                         contacts.Sort((x,y)=>x.FirstName.CompareTo(y.FirstName));
                        foreach (var contact in contacts)
                        {
                            Console.WriteLine("AddressBook Name: " + AddressBook_Name + "\n FirstName: " + contact.FirstName + "\n LastName:" + contact.LastName + "\n Address: " + contact.Address + "\n City: " + contact.City + "\n State: " + contact.State + "\n Email Id" + contact.Email + "\n ZipCode: " + contact.ZipCode + "\n Phone number: " + contact.PhoneNumber);
                        }
                    }
                    break;
                case 5:
                    if (contacts.Count <= 0)
                    {
                        Console.WriteLine("No contacts available");
                        return;
                    }
                    Console.WriteLine("Enter city,state and zipcode to sort entries");
                    Console.WriteLine("Enter city name");
                    var Citys = Console.ReadLine();
                    Console.WriteLine("Enter state name");
                    var States = Console.ReadLine();
                    Console.WriteLine("Enter Zipcode name");
                    var Zipcodes= int.Parse(Console.ReadLine());
                    foreach (var contact in contacts)
                    {
                        if(Citys==contact.City && States==contact.State && Zipcodes==contact.ZipCode )
                        {
                            Console.WriteLine("AddressBook Name: " + AddressBook_Name + "\n FirstName: " + contact.FirstName + "\n LastName:" + contact.LastName + "\n Address: " + contact.Address + "\n City: " + contact.City + "\n State: " + contact.State + "\n Email Id" + contact.Email + "\n ZipCode: " + contact.ZipCode + "\n Phone number: " + contact.PhoneNumber);
                        }
                    }
                    break;
                    
            }
        }
        public void Save()
        {
            string path = @"C:\json\Address.json";
            var jsonInventory = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(path, jsonInventory);
        }
        public void Load()
        {
            string path = @"C:\json\Address.json";
            var json = File.ReadAllText(path);
            contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            Display();
        }
        public void EditContact()
        {
            Console.WriteLine("Enter FirstName of conatc to edit that contact");
            string name = Console.ReadLine();
            Contact contact = null;
            foreach (var res in contacts)
            {
                if (res.FirstName == name)
                {
                    contact = res;
                }
            }
            if (contact == null)
            {
                Console.WriteLine("No contact present with given name");
            }
            contact = AddToContact();
            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].FirstName == name)
                {
                    contacts[i] = contact; break;
                }
            }

            Console.WriteLine("Contact Edited successfully");

        }
        public void search()
        {
            Console.WriteLine("Enter 1 to search by city \nEnter 2 to search by state");
            int input = int.Parse(Console.ReadLine());
            string res = "";
            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter city name");
                    var city = Console.ReadLine();
                    res = city;
                    break;
                case 2:
                    Console.WriteLine("Enter state name");
                    var state = Console.ReadLine();
                    res = state;
                    break;
            }
            foreach (var contact in contacts)
            {
                if (contact.City.Equals(res) || contact.State.Equals(res))
                    Console.WriteLine("AddressBook Name: " + AddressBook_Name + "\n FirstName: " + contact.FirstName + "\n LastName:" + contact.LastName + "\n Address: " + contact.Address + "\n City: " + contact.City + "\n State: " + contact.State + "\n Email Id" + contact.Email + "\n ZipCode: " + contact.ZipCode + "\n Phone number: " + contact.PhoneNumber);
                else Console.WriteLine("person search by city or state is not present in contact");
            }
            Console.ReadLine();
        }

        public void DeleteContact()
        {
            Console.WriteLine("Enter the FirstName of the contact to Delete");
            string dname = Console.ReadLine();
            bool flag = true;

            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].FirstName == dname)
                {
                    contacts.RemoveAt(i);
                    Console.WriteLine("Contact deleted Successfully");
                    flag = false;

                }
            }
            if (flag)
            {
                Console.WriteLine($"No contact present of {dname} name");
            }

        }
    }
}
