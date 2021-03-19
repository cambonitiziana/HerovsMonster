using Game.ADORepository.Extension;
using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Game.ADORepository
{
   public class ADOWeaponsRepository : IWeaponRepository
   {

        const string connectionString = @"Persist Security Info= false; Integrated Security = true; Initial Catalog = Game; Server = .\SQLEXPRESS";

        public void Create(Weapon obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Weapon obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Weapon> GetAll()
        {
            throw new NotImplementedException();
        }

        public Weapon GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Weapon obj)
        {
            throw new NotImplementedException();
        }
        public List<Weapon> GetAll(Hero h)
        {
            List<Weapon> WeaponsByClasses = new List<Weapon>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                string query= "SELECT * FROM RoleClasses WHERE [Livello] = @level AND [Class]=@class";
                
                //Comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;
                
                command.Parameters.AddWithValue("@level", h.level);
                command.Parameters.AddWithValue("@class", h.classPerson);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    WeaponsByClasses.Add(reader.ToWeapon());
                }

                reader.Close();
                connection.Close();

            }
            return WeaponsByClasses;
        }

        List<Weapon> IRepository<Weapon>.GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
