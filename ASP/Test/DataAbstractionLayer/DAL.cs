using Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Test.DataAbstractionLayer
{
    public class DAL
    {
        private MySqlConnection connection;
        public DAL()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=web;";
            connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
        }

        ~DAL()
        {
            connection.Close();
        }
        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();

            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "select * from books";
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();
                    book.id = reader.GetInt32("id");
                    book.title = reader.GetString("title");
                    book.author = reader.GetString("author");
                    book.pages = reader.GetInt32("pages");
                    book.price = reader.GetInt32("price");
                    book.lender = reader.GetString("lender");
                    books.Add(book);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }

            return books;
        }

        public User Login(string username, string password)
        {
            User user = new User();
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "select * from users where username='" + username +"' and pass='" + password +"' ";
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user.username = reader.GetString("username");
                    user.password = reader.GetString("pass");
                }
                else
                {
                    user = null;
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return user;
        }

        public void Register(string username, string password)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "insert into users(username, pass) values('" + username + "', '" + password + "')";
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }
        }

        public void saveBook(Book book)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "insert into books(title, author, pages, price, lender) values('" + book.title + "', '" + book.author + "', '" + book.pages + "', '" + book.price + "', '" + "No" + "')";
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }
        }

        public void deleteBook(string title)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "delete from books where title='" + title +"' ";
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }
        }

        public int getBookId(string bookTitle)
        {
            int BookID = -1;
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "select id from books where title='" + bookTitle +"' ";
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                   BookID = reader.GetInt32("id");
                }
                reader.Close();
            }
            
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }

            return BookID;
        }

        public void updateBook(int id, Book book)
        {
            Console.WriteLine(id);
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "update books set title='" + book.title + "', author='" + book.author + "', pages='" + book.pages + "', price='" + book.price + "' where id='" + id + "' ";
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }
        }

        public void lend(int id, string lender)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "update books set lender='" + lender + "' where id='" + id + "' ";
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }
        }

        public List<Book> filter(string author)
        {
            List<Book> books = new List<Book>();
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "select * from books where author='" + author +"' ";
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();
                    book.id = reader.GetInt32("id");
                    book.title = reader.GetString("title");
                    book.author = reader.GetString("author");
                    book.pages = reader.GetInt32("pages");
                    book.price = reader.GetInt32("price");
                    book.lender = reader.GetString("lender");
                    books.Add(book);
                }
                reader.Close();
            }
            
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }

            return books;
        }
    }
}