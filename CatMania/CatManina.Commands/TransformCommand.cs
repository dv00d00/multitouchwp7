using System;

namespace CatManina.Commands
{
    public interface ICommand
    {
        void Undo();
        void Do();
    }

    public class CommandBase : ICommand
    {
        public virtual void Undo()
        {
        }

        public virtual void Do()
        {
        }
    }

    public class TransformCommand : CommandBase
    {
        private readonly Guid guid;

        public TransformCommand(Guid guid)
        {
            this.guid = guid;
        }

        public override void Undo()
        {
            base.Undo();
        }

        public override void Do()
        {
            base.Do();
        }
    }


}