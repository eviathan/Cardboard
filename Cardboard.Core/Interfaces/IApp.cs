using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cardboard.Core.Interfaces
{
    public interface IApp
    {
        // IReadOnlyList<IWindow> Windows { get; }
        // IWindow CreateWindow(string title, ITemplate? initialTemplate = null);
        void Run();
        void Exit();
    }
}