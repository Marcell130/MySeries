using System;

namespace MySeries.Api.GlobalHandlers.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public int Id { get; }

        public EntityNotFoundException() : base("The requested entity could not be found.")
        {
            
        }

        public EntityNotFoundException(int id) : base($"The requested entity (id={id}) could not be found.")
        {
            Id = id;
        }

        public EntityNotFoundException(string message) : base(message)
        {
            
        }

    }
}