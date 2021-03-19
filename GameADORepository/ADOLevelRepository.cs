using Game.ADORepository.Extension;
using HeroVSMonster.Core;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Game.ADORepository
{
    public class ADOLevelRepository: ILevelRepository
    {
        const string connectionString = @"Persist Security Info= false; Integrated Security = true; Initial Catalog = Game; Server = .\SQLEXPRESS";

        public void Create(Livello obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Livello obj)
        {
            throw new NotImplementedException();
        }

        public List<Livello> GetAll()
        {
            List<Livello> Levels = new List<Livello>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                string query = "SELECT * FROM LevelLifePointScore";

                //Comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Levels.Add(reader.ToLevel());
                }

                
                reader.Close();
                connection.Close();

            }
            return Levels;
        }

        public Livello GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Livello obj)
        {
            throw new NotImplementedException();
        }
    }
}
