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
    public class ADOStatisticsRepository : IStatisticsRepository
    {
        const string connectionString = @"Persist Security Info= false; Integrated Security = true; Initial Catalog = Game; Server = .\SQLEXPRESS";
        public void delete(Hero h)
        {
            
        }

        public List<MatchStatistics> getAll(Player p)
        {
            List<MatchStatistics> matchStatistics = new List<MatchStatistics>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                string query = "SELECT * FROM StatisticsMatch WHERE PlayerID=@id";

                //Comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;


                command.Parameters.AddWithValue("@id", p.ID);
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    matchStatistics.Add(reader.ToStatistics());
                }

                reader.Close();
                connection.Close();
         
            }
            return matchStatistics;
        }

        public void update(Hero h)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                //Comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "UPDATE StatisticsMatch SET MatchNumbers = @mn , Winnings = @w, MatchTime=@time WHERE HeroName = @name";

                //creo parametri 
                command.Parameters.AddWithValue("@mn", h.Statistcs.totalMatch);
                command.Parameters.AddWithValue("@w", h.Statistcs.winnings);
                command.Parameters.AddWithValue("@time", h.Statistcs.time);
                command.Parameters.AddWithValue("@name", h.name);

                //esecuzione 

                command.ExecuteNonQuery();
                connection.Close();

            }
           

        }

      
            //public void Create(Hero newHero)
            //{

            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        SqlDataAdapter adapter = new SqlDataAdapter();

            //        SqlCommand selectCommand = new SqlCommand();
            //        selectCommand.Connection = connection;
            //        selectCommand.CommandType = System.Data.CommandType.Text;
            //        selectCommand.CommandText = "SELECT * FROM StatisticsMatch";

            //        SqlCommand insertCommand = new SqlCommand();
            //        insertCommand.Connection = connection;
            //        insertCommand.CommandType = System.Data.CommandType.Text;
            //        insertCommand.CommandText = "INSERT INTO StatisticsMatch VALUES (@ID ,

            //        insertCommand.Parameters.AddWithValue("@ID", newHero.name);//avrei dovuto mettere dentro il player ID
                    
            //        adapter.SelectCommand = selectCommand;
            //        adapter.InsertCommand = insertCommand;


            //    }
            //}
    }
}
