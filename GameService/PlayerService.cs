using HeroVSMonster.Core.Entities;
using HeroVSMonster.Core.Interface;
using System;
using System.Collections.Generic;

namespace GameService
{
    public class PlayerService
    {
        private IPlayerRepository _repo;
        public PlayerService(IPlayerRepository repo)
        {
            _repo = repo;
        }

        public List<Player> GetAllPlayer()
        {
            return _repo.GetAll();
        }

        public Player GetPlayerByID(int n)
        {
            //logica di business

            if (n >0 )
            {
                return _repo.GetByID(n);
            }
            return null;
        }

        public Player CreatePlayer(Player p)
        {
            if (p != null)
            {
                _repo.Create(p);
                return p;
            }
            else
                return null;
        }

        public static Player Registration(string n)
        {
            Player p = new Player();
            p.name = n;
            return p;
        }
        public static (bool,int) IsRegistred(string player, List<Player> players)
        {
            bool isRegistred = false;
            int id=0;
            foreach (var p in players)
            {
                if (p.name == player)
                {
                    id = p.ID;
                    isRegistred = true;
                }
            }
            return (isRegistred, id);
        }

        //{    
        //    Console.WriteLine("Sei gia Iscritto?");

        //    var answer = Funz.CheckAnswer();
        //    return answer;
        //}

        //get ID of an existing player-> if not found-> id=0
        public static int GetExistingPlayerID(IEnumerable<Player> players)
        {

        InsertName:
            Console.WriteLine("Inserisci il tuo nome: ");
            Player p = new Player();
            p.name = Console.ReadLine();

            var id = GetPlayerID(players, p);

            if (id == 0)
            {
                Console.WriteLine("utente non trovato!");
                goto InsertName;
            }

            return id;

        }

        //get ID
        public static int GetPlayerID(IEnumerable<Player> players, Player p)
        {
            int id = 0;
            foreach (var player in players)
            {
                if (player.name == p.name)
                {
                    id = player.ID;
                    Console.WriteLine(" Il tuo ID è: {0}", player.ID);
                }
            }
            return id;
        }

        
    }
}
