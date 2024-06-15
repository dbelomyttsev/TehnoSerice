using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BooksShop
{
    public class DataBaseHelper
    {
        string stringconnection = "host=localhost;username=postgres;password=123098;database=BooksShop";
        NpgsqlConnection connection;
        public DataBaseHelper() { 
            connection = new NpgsqlConnection(stringconnection);
        }



        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            connection.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.books", connection);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book
                {
                    BookId = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Autor = reader.GetString(2),
                    Price = reader.GetInt32(3)
                });

            }

            connection.Close();

            return books;
        }
    
        public int GetBookCount(Book book)
        {
            int count = 0;

            connection.Open();

            NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM public.storage where bookid = {book.BookId}", connection);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                count = reader.GetInt32(2);
            }

            connection.Close();

            return count;

        }

        public void SaveOrders(int userid, DateTime date, int totalprice)
        {
            connection.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.orders(userid, date, totalprice) VALUES (@userid, @date, @totalprice);", connection);

            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@totalprice", totalprice);

            cmd.ExecuteNonQuery();

            connection.Close();     

        }

        


        public int GetIdLastOrder()
        {
            int orderid = 0;
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.orders\r\nORDER BY orderid DESC", connection);
            
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orderid = reader.GetInt32(0);
                break;
            }

            return orderid;
        
        }

        public void SaveBookOrder(Book book, int count)
        {
            connection.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("", connection);


        }

        public void AddOrder(Dictionary<Book, int> map, int userid, DateTime date, int totalprice)
        {
            using (var connection = new NpgsqlConnection(stringconnection))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sqlcmd = "INSERT INTO public.orders(userid, date, totalprice) VALUES (@userid, @date, @totalprice);";
                        // Добавляем заказ в таблицу "orders"
                        using (var command = new NpgsqlCommand(sqlcmd, connection))
                        {
                            command.Parameters.AddWithValue("@userid", userid);
                            command.Parameters.AddWithValue("@date", date);
                            command.Parameters.AddWithValue("@totalprice", totalprice);
                            command.ExecuteNonQuery();
                        }

                        int lastorderid = 0;
                        string sqlcmd1 = "SELECT * FROM public.orders\r\nORDER BY orderid DESC";
                        using (var command = new NpgsqlCommand(sqlcmd1, connection))
                        {
                            lastorderid = (int)command.ExecuteScalar();
                        }

                        string sqlcmd2 = "INSERT INTO public.bookorder(bookid, orderid, count) VALUES (@bookid, @orderid, @count);";

                        foreach (var item in map)
                        {
                            using (var command = new NpgsqlCommand(sqlcmd2, connection))
                            {
                                command.Parameters.AddWithValue("@bookid", item.Key.BookId);
                                command.Parameters.AddWithValue("@orderid", lastorderid);
                                command.Parameters.AddWithValue("@count", item.Value);
                                command.ExecuteNonQuery();
                            }
                        }

                        string sqlcmd3 = "UPDATE public.storage SET \"count\"= \"count\" - @count WHERE bookid = @bookid;";

                        foreach (var item in map)
                        {
                            using (var command = new NpgsqlCommand(sqlcmd3, connection))
                            {
                                command.Parameters.AddWithValue("@bookid", item.Key.BookId);
                                command.Parameters.AddWithValue("@count", item.Value);
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Ошибка при добавлении заказа: " + ex.Message);
                    }
                }
            }
        }
    }
}
