using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public sealed class User : EntityMutable<int>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}