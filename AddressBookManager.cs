
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Xml;

namespace AddressBook_System
{
    public class AddressBookManager
    {
        List<AddressBook> Addresses;
        public AddressBookManager()
        {
            Addresses = new List<AddressBook>();
        }
        public void Open_AddressBook()
        {
            Console.WriteLine("Enter AddressBook Name:");
            string book_name = Console.ReadLine();
            AddressBook Address_book = null;
            foreach (var res in Addresses)
            {
                if (res.AddressBook_Name == book_name)
                {
                    Address_book = res;
                    AddressBook_operation(Address_book);
                    return;
                }
            }
            Console.WriteLine("there is no addressBook with entered name");
        }
        public void Create_AddressBook()
        {
            Console.WriteLine("Enter AddressBook Name:");
            string name = Console.ReadLine();
            Addresses.Add(new AddressBook(name));
            return;
        }
       
        public void AddressBook_operation(AddressBook addressBook)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Enter A to Add new contact\nEnter E to edit a contact\nEnter D to Delete a Contact\nEnter P to print the contacts\nEnter F to search the contacts\nEnter S to save the contacts\nEnter L to read the contacts");
                char ch = Console.ReadLine().ToUpper()[0];
                switch (ch)
                {
                    case 'A':
                       
                        if (addressBook.contacts.Count==0)
                        {
                            addressBook.AddToContact();
                        }
                        else
                        {
                            foreach (Contact c in addressBook.contacts)
                            {
                                Console.WriteLine("Enter Name of the person");
                                var name = Console.ReadLine();
                                while (c.FirstName.Equals(name))
                                {
                                    Console.WriteLine($"Entered {name} is already present");
                                    Console.WriteLine($"Entered different name to add contact");
                                    name = Console.ReadLine();
                                }
                            }
                                addressBook.AddToContact();
                        }
                        break;
                    case 'E':
                        addressBook.EditContact();
                        break;
                    case 'D':
                        addressBook.DeleteContact();
                        break;
                    case 'P':
                        addressBook.Display();
                        break;
                    case 'F':
                        addressBook.search();
                        break;
                    case 'S':
                        addressBook.Save();
                        break;
                    case 'L':
                        addressBook.Load();
                        break;
                    default:
                        break;
                }
                Thread.Sleep(5000);
                Console.Clear();
                Console.WriteLine("Enter 'BACK' to go back to AddressBookManagement  or 'No' to stay in current addressBook :");
                string input = Console.ReadLine().ToUpper();
                if (input.Equals("BACK"))
                {
                    flag = false;
                }
            }
        }


    }
}