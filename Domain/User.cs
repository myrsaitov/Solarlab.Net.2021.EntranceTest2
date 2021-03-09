using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public sealed class User : EntityMutable<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
}