using System;

namespace DomainName.PL.Commands
{
    public interface ICommand
    {
        string Id { get; }
    }

    public abstract class Command : ICommand
    {
        public string Id { get; set; }
        public string IssuedBy { get; set; }
        public DateTime TimeIssued { get; set; }
    }
}