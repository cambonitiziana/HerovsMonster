using Game.ADORepository;
using GameADORepository;
using GameService;
using HeroVSMonster.Core.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroVSMonster
{
    public class DIConfiguration
    {
        public static ServiceProvider ConfigurazionePlayer() //classe appena scaricata
        {
            return new ServiceCollection()

                    //aggiunta in due modi: addTransient e dipende da dove viene messo il servizio, si può cancellare o no
                    //AddSingleton: aggiunge implementazione unica e va bene per sempre

                    .AddTransient<PlayerService>() //primo servizio che aggiungiamo è quello scritto da noi
                                                  //servizio che mappa astrazione con implementazione

                    .AddTransient<IPlayerRepository, ADOPlayerRepository>() //servizio che mappa l'astrazione con l'implementazione
                    .BuildServiceProvider();

        }

        public static ServiceProvider ConfigurazioneHero() //classe appena scaricata
        {
            return new ServiceCollection()

                    .AddTransient<HeroService>() //primo servizio che aggiungiamo è quello scritto da noi
                                                   //servizio che mappa astrazione con implementazione

                    .AddTransient<IHeroRepository, ADOHeroRepository>() //servizio che mappa l'astrazione con l'implementazione
                    .BuildServiceProvider();

        }

        public static ServiceProvider ConfigurazioneWeapon() //classe appena scaricata
        {
            return new ServiceCollection()

                  
                    .AddScoped<WeaponService>() 

                    .AddScoped<IWeaponRepository, ADOWeaponsRepository>() //servizio che mappa l'astrazione con l'implementazione
                    .BuildServiceProvider();

        }

        public static ServiceProvider ConfigurazioneMonster () //classe appena scaricata
        {
            return new ServiceCollection()

                    .AddScoped<MonsterService>()

                    .AddScoped<IMonsterRepository, ADOMonsterRepository>() 
                    .BuildServiceProvider();
        }
        public static ServiceProvider ConfigurazioneLevel() //classe appena scaricata
        {
            return new ServiceCollection()

                    .AddScoped<LevelService>()

                    .AddScoped<ILevelRepository, ADOLevelRepository>()
                    .BuildServiceProvider();
        }
        public static ServiceProvider ConfigurazioneStatistics() //classe appena scaricata
        {
            return new ServiceCollection()


                    .AddScoped<StatisticsService>()

                    .AddScoped<IStatisticsRepository, ADOStatisticsRepository>() //servizio che mappa l'astrazione con l'implementazione
                    .BuildServiceProvider();

        }
    }
}
