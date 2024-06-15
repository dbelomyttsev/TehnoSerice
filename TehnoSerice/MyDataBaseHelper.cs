using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Logging;
using Npgsql;

namespace TehnoSerice
{
    public class MyDataBaseHelper
    {
        string connectionstring = "host=localhost;username=postgres;password=123098;database=TehnoService";

        NpgsqlConnection connection;

        public MyDataBaseHelper() 
        { 
            connection = new NpgsqlConnection(connectionstring);
        }

        public User FindUser(string login)
        {
            try
            {
                connection.Open();

                User user = new User();

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"SELECT * FROM public.users where login = \'{login}\'", connection);

                NpgsqlDataReader reader = npgsqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    user = new User 
                    {
                        UserId = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Password = reader.GetString(2),
                        Role = (User.UserRole)reader.GetInt32(3)
                    };
                    break;
                }

                return user;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void SaveUser(User user)
        {
            try
            {
                connection.Open();

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand("INSERT INTO public.users(login, \npassword\n, role_id) VALUES (@login, @password, @role_id);", connection);

                npgsqlCommand.Parameters.AddWithValue("@login", user.Login);
                npgsqlCommand.Parameters.AddWithValue("@password", user.Password);
                npgsqlCommand.Parameters.AddWithValue("@role_id", (int)user.Role);

                npgsqlCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void SaveOrder(Order order)
        {
            try
            {
                connection.Open();

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand("INSERT INTO public.orders" +
                    "(add_date, end_date, problem_type_id, description, user_id, status) " +
                    "VALUES (@add_date, @end_date, @problem_type_id, @description, @user_id, @status);", connection);

                npgsqlCommand.Parameters.AddWithValue("@add_date", order.AddDate);
                npgsqlCommand.Parameters.AddWithValue("@end_date", order.EndDate);
                npgsqlCommand.Parameters.AddWithValue("@problem_type_id", order.ProblemId);
                npgsqlCommand.Parameters.AddWithValue("@description", order.Description);
                npgsqlCommand.Parameters.AddWithValue("@user_id", order.UserId);
                npgsqlCommand.Parameters.AddWithValue("@status", order.Status);
                //npgsqlCommand.Parameters.AddWithValue("@workers_id", order.WorkerId);

                npgsqlCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<ProblemType> GetAllProblem()
        {
            try
            {
                List<ProblemType> problemTypes = new List<ProblemType>();

                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.problem_types", connection);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    problemTypes.Add(new ProblemType
                    {
                        ProblemTypeId = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
                return problemTypes;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                List<Order> orders = new List<Order>();
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("select * from orders", connection);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id = reader.GetInt32(0),
                        AddDate = reader.GetDateTime(1),
                        EndDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                        ProblemId = reader.GetInt32(3),
                        Description = reader.GetString(4),
                        UserId = reader.GetInt32(5),
                        Status = reader.GetString(6),
                        WorkerId = reader.GetInt32(5),
                    });
                }
                return orders;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateOrder(Order order) 
        {
            try
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("UPDATE public.orders SET end_date=@end_date, status=@status WHERE order_id=@order_id;", connection);

                command.Parameters.AddWithValue("@end_date", order.EndDate);
                command.Parameters.AddWithValue("@order_id", order.Id);
                command.Parameters.AddWithValue("@status", order.Status);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                    
            }
        
        }
    }
}
