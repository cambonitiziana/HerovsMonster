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
    public class ADOHeroRepository: IHeroRepository
    {
        const string connectionString = @"Persist Security Info= false; Integrated Security = true; Initial Catalog = Game; Server = .\SQLEXPRESS";
        public void Create(Hero newHero)
        {

            throw new NotImplementedException();
        }
        public  void Create(Hero newHero, int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM Hero";

                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO Hero VALUES (@ID ,@Nome, @Class, @Score, @LifePoint, @Livello)";

                insertCommand.Parameters.AddWithValue("@Nome", newHero.name);
                insertCommand.Parameters.AddWithValue("@ID", id);
                insertCommand.Parameters.AddWithValue("@Class", newHero.classPerson);
                insertCommand.Parameters.AddWithValue("@Score", newHero.score);
                insertCommand.Parameters.AddWithValue("@LifePoint", newHero.lifePoint);
                insertCommand.Parameters.AddWithValue("@Livello", newHero.level);

                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;


                DataSet dataset = new DataSet();
                try
                {
                    connection.Open();
                    adapter.Fill(dataset, "Hero");
                    DataRow hero = dataset.Tables["Hero"].NewRow();
                    dataset.Tables["Hero"].Rows.Add(hero);
                    adapter.Update(dataset, "Hero");

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
        public bool Delete(Hero h)
        { 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand deleteCommand = new SqlCommand();
                deleteCommand.Connection = connection;
                deleteCommand.CommandType = System.Data.CommandType.Text;
                deleteCommand.CommandText = "DELETE FROM Hero WHERE [ID]=@id";

                deleteCommand.Parameters.AddWithValue("@id", h.ID);
                
                adapter.DeleteCommand = deleteCommand;

                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e)
                {
                    return false;
                   Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public List<Hero> GetAll()
        {
            List<Hero> AllHeros = new List<Hero>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                //Comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Hero";


                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AllHeros.Add(reader.ToHero());
                }

                reader.Close();
                connection.Close();

            }
            return AllHeros;
        }
        public Hero GetByID(int ID)
        {
           throw new NotImplementedException();
        }
        public List<Hero> GetHerossByID(int n)
        {
            List<Hero> HerosByIDList = new List<Hero>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                //Comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Hero WHERE [PlayerID] = @ID";

                //creo parametro id
                SqlParameter idPar = new SqlParameter();
                idPar.ParameterName = "@ID";
                idPar.Value = n;

                //esecuzione 
                command.Parameters.Add(idPar);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    HerosByIDList.Add(reader.ToHero());
                }

                reader.Close();
                connection.Close();

            }
            return HerosByIDList;
        }

        public bool Update(Hero h)
         {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                //Comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "UPDATE HERO SET Score = @score , LifePoint = @lifepoint, Livello = @livello WHERE ID = @id";

                //creo parametri 
                command.Parameters.AddWithValue("@score", h.score);
                command.Parameters.AddWithValue("@lifepoint", h.lifePoint);
                command.Parameters.AddWithValue("@livello", h.level);
                command.Parameters.AddWithValue("@id", h.ID);

                //esecuzione 

                command.ExecuteNonQuery();
                connection.Close();

            }
            return true;
            
         }
    }
           
}
