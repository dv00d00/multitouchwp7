using System;
using System.Windows.Input;

namespace CatMania
{
    public class DelegateCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> canExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute)
            : this(execute, () => true)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canexecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            action = execute;
            canExecute = canexecute;
        }

        public bool CanExecute(object p)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object p)
        {
            if (CanExecute(null))
            {
                action();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
    
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> action;
        private readonly Func<T, bool> canExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute)
            : this(execute, it => true)
        {
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canexecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            action = execute;
            canExecute = canexecute;
        }

        public bool CanExecute(object p)
        {
            return canExecute != null && canExecute((T) p);
        }

        public void Execute(object p)
        {
            if (CanExecute(null))
            {
                if (p is T)
                {
                    action((T) p);
                }
                else
                {
                    throw new ArgumentException("Can execute method has wrong input parameter type");
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}