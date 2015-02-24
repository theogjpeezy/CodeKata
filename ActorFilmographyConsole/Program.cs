using System;
using filmography;

namespace ActorFilmographyConsole {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Welcome to the Actor Filmography Reference!");
            Console.Write("Please Enter an Actor's Name: ");
            var input = Console.ReadLine();
            var filmography = new Filmography();
            PopulateStorage(filmography);

            Console.Clear();
            Console.WriteLine("{0} was featured in:", input);
            foreach(var movie in filmography.GetActorFilms(input))
            {
                Console.WriteLine(movie);
            }

            Console.Read();
        }

        private static void PopulateStorage(Filmography filmography) {
            filmography.AddActorFilm("Paul Walker", "The Fast and Furious");
            filmography.AddActorFilm("Paul Walker", "Running Scared");
            filmography.AddActorFilm("Al Pacino", "Scarface");
            filmography.AddActorFilm("Al Pacino", "Heat");
            filmography.AddActorFilm("Kevin Spacey", "Se7en");
            filmography.AddActorFilm("Kevin Spacey", "K-Pax");
        }
    }
}
