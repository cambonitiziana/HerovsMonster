using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Game.ADORepository.Extension;
using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;

namespace GameADORepository
{
    public class ADOPlayerRepository : IPlayerRepository
    {
        const string connectionString = @"Persist Security Info= false; Integrated Security = true; Initial Catalog = Game; Server = .\SQLEXPRESS";
        public void Create(Player p)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                string Nome = p.name;

                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM Player";

                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO Player VALUES (@Nome, 0)";

                insertCommand.Parameters.AddWithValue("@Nome", Nome);
               

                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;


                DataSet dataset = new DataSet();
                try
                {
                    connection.Open();
                    adapter.Fill(dataset, "Player");

                    DataRow player = dataset.Tables["Player"].NewRow();
                    dataset.Tables["Player"].Rows.Add(player);
                    adapter.Update(dataset, "Player");
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

        public bool Delete(Player obj)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetAll()
        {
            List<Player> playerList = new List<Player>();

            //ADO.NET
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //aprire connessione 
                connection.Open();

                //comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Player";

                //esecuzione : ho una tabella-> sql datareader

                SqlDataReader reader = command.ExecuteReader();

                //lettura della tabella e quello che leggaimo lo mettiamo nella lista
                //problema: quello che leggiamo dipenda dal nome delle colonne
                //--> metodo esterno (estensione sql) -> in modo da mappare quello che leggiamo --> lo mettiamo in adorepository

                while (reader.Read())
                {
                    playerList.Add(reader.ToPlayer());
                }

                //chiudere connessione

                reader.Close();
                connection.Close();

            }
            return playerList;
        }

        public Player GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Player obj)
        {
            throw new NotImplementedException();
        }

        
    }
}
