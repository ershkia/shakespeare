using System;

namespace shakespeare.core.dto
{
    public class User
    {
        public User(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public Guid Id { private set; get; }
        public string UserName { private set; get; }
    }
}
