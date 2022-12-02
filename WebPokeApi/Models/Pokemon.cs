using Newtonsoft.Json;
using System;

namespace WebPokeApi.Models
{
    public class Pokemon
    {
        public string? name { get; set; }
        public string? url { get; set; }

        public Root? root;
    }

    public class PokemonList
    {
        public List<Pokemon>? results;        
    }

    public class Sprites
    {
        public Other? other { get; set; }
    }

    public class Other
    {
        public DreamWorld? dream_world { get; set; }
    }

    public class DreamWorld
    {
        public string? front_default { get; set; }        
    }

    public class Root
    {
        public Sprites? sprites { get; set; }
        public List<Type>? types { get; set; }
    }

    public class Type
    {        
        public Type2? type { get; set; }
    }

    public class Type2
    {
        public string? name { get; set; }
        public string? url { get; set; }
    }
}
