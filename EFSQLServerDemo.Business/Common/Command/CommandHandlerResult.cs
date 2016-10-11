using System.Collections.Generic;

namespace EFSQLServerDemo.Business.Common.Command
{
    public class CommandHandlerResult
    {
        public bool IsValid { get; set; }
        public int? Id { get; set; }
        public ICollection<CommandHandlerValidation> CommandHandlerValidations { get; set; }
    }
}
