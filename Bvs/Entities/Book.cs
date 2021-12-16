namespace Bvs.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Verlag { get; set; }
        public int Verfuegbar { get; set; }
        public int Anzahl { get; set; }
        public string B_foto { get; set; }
        public string Autor { get; set; }
        public string Kategorie { get; set; }
    }
}
