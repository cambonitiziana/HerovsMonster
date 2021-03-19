using Game.ADORepository.Extension;
using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Game.ADORepository
{
    public class ADOMonsterRepository : IMonsterRepository
    {

        const string connectionString = @"Persist Security Info= false; Integrated Security = true; Initial Catalog = Game; Server = .\SQLEXPRESS";

        public void Create(Monster obj)
        {
            throw new NotImplementedException();
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
