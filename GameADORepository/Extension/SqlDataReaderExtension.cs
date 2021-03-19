using HeroVSMonster.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using HeroVSMonster.Core;

namespace Game.ADORepository.Extension
{
    public static class SqlDataReaderExtension
    {
        //data istanza di sql datareader me lo voncente in entità movie
        public static Player ToPlayer(this SqlDataReader reader)
        {
            //mappa tutte le proprietà di movie quando legge-> 
            return new Player()
            {
                ID = (int)reader["ID"], //oppure IDictionary= reader.GetInt32(0)
                name = reader["Nome"].ToString()
            };
        }
        public static Hero ToHero(this SqlDataReader reader)
        {
            //mappa tutte le proprietà di movie quando legge-> 
            return new Hero()
            {
                ID=(int)reader["ID"],
                name = reader["Nome"].ToString(), 
                classPerson = reader["Class"].ToString(),
                lifePoint= (int)reader["LifePoint"],
                level= (int)reader["Livello"],
                score=(int)reader["Score"]
            };
        }

        public static Weapon ToWeapon(this SqlDataReader reader)
        {
            //mappa tutte le proprietà di movie quando legge-> 
            return new Weapon()
            {
                name = reader["WeaponName"].ToString(), 
                damagePoint = (int)reader["DamagePoint"]
               
            };
        }
        public static Monster ToMonster(this SqlDataReader reader)
        {
            //mappa tutte le proprietà di movie quando legge-> 
            return new Monster()
            {
                level=(int)reader["Livello"],
                lifePoint = 20,
                classPerson = reader["Class"].ToString(),
                weapon = new Weapon
                {
                    name = reader["WeaponName"].ToString(),
                    damagePoint = (int)reader["DamagePoint"]
                }
            };
        }
        public static Livello ToLevel(this SqlDataReader reader)
        {
            //mappa tutte le proprietà di movie quando legge-> 
            return new Livello()
            {
                livello = (int)reader["Livello"],
                lifePoint = (int)reader["LifePoint"],
                score = (int)reader["Score"]
            };
        }
    } 
} 