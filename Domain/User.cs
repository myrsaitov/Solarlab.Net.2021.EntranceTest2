using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public sealed class User : EntityMutable<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string UserName { get; set; }
    }
}