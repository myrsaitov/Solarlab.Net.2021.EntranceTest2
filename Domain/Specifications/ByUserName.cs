using System;
using System.Linq.Expressions;

namespace WidePictBoard.Domain.Specifications
{
    public static class ByUserName
    {
        public static Expression<Func<User, bool>> With(string userName) => u => u.Name == userName;
    }
}