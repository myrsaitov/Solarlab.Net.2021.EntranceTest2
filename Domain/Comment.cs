using WidePictBoard.Domain;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public sealed class Comment : EntityMutable<int>
    {
        public int AuthorId { get; set; }
        
        public User Author { get; set; }
        
        public int AdId { get; set; }
        
        public string Text { get; set; }
    }
}