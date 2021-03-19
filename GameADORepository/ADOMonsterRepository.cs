using Game.ADORepository.Extension;
using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Game.ADORepository
{
    public class ADOMonsterRepository : IMonsterRepository
    {

        const string connectionString = @"Persist Security Info= false; Integrated Security = true; Initial Catalog = Game; Server = .\SQLEXPRESS";

        public void Create(Monster m)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM RoleClasses";

                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO RoleClasses VALUES ('Mostro', @Class, @livello, @weaponName, @damagePoint)";

                insertCommand.Parameters.AddWithValue("@Class",m.classPerson );
                insertCommand.Parameters.AddWithValue("@livello", m.level);
                insertCommand.Parameters.AddWithValue("@weaponName", m.weapon.name);
                insertCommand.Parameters.AddWithValue("@damagePoint", m.weapon.damagePoint);

                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;


                DataSet dataset = new DataSet();
                try
                {
                    connection.Open();
                    adapter.Fill(dataset, "RoleClasses");

                    DataRow monster = dataset.Tables["RoleClasses"].NewRow();
                    dataset.Tables["RoleClasses"].Rows.Add(monster);
                    adapter.Update(dataset, "RoleClasses");
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool Delete(Monster obj)
        {
            throw new NotImplementedException();
        }

        public List<Monster> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<Monster> GetAll(Hero h)
        {
            List<Monster> MonsterByLevel = new List<Monster>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                string query = "SELECT * FROM RoleClasses WHERE [Livello] <= @level AND [Role] = 'Mostro'";

                //Comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query; 


                command.Parameters.AddWithValue("@level", h.level);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MonsterByLevel.Add(reader.ToMonster());
                }

                reader.Close();
                connection.Close();

            }
            return MonsterByLevel;
        }
 

        public Monster GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Monster obj)
        {
            throw new NotImplementedException();
        }
    }
}
