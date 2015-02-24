using System.Collections.Generic;
using System.Linq;
using DictionaryRepository;

namespace filmography
{
    public class Filmography
    {
        public Filmography() {
            _filmographyStorage = new DictionaryStorage<IEnumerable<string>>();
        }

        public IEnumerable<string> GetActors() {
            return _filmographyStorage.GetKeys();
        }

        public IEnumerable<string> GetActorFilms(string actorName) {
            return _filmographyStorage.Get(actorName);
        }

        public void AddActorFilm(string actorName, string movie) {
            if (_filmographyStorage.Exists(actorName)) {
                var movies = _filmographyStorage.Get(actorName).ToList();
                movies.Add(movie);
                _filmographyStorage.Set(actorName, movies);
            }
            else {
                var movies = new List<string> { movie };
                _filmographyStorage.Set(actorName, movies);
            }
        }

        private readonly DictionaryStorage<IEnumerable<string>> _filmographyStorage;
    }
}
