using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace March_27_library.Data
{
    public class PeopleManager
    {
        private string _connectionString;
        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;   
        }
        public List<Person> GetAllPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Person> people = new List<Person>();
            while (reader.Read())
            {
                people.Add(new Person{
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            connection.Close();
            return people;
        }
        public void AddListOfPeople(List<Person> people)
        {
            foreach(Person person in people)
            {
                AddPerson(person);
            }
        }
        public void AddPerson(Person p)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO People VALUES(@firstName, @lastName, @age)";
            command.Parameters.AddWithValue("@firstName", p.FirstName);
            command.Parameters.AddWithValue("@lastName", p.LastName);
            command.Parameters.AddWithValue("@age", p.Age);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
