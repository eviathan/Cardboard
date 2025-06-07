using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cardboard.Core.Models;
using Cardboard.Core.Models.Events;

namespace Cardboard.Core.Interfaces
{
    public interface IWindow
    {
        // string Title { get; set; }
        // Size Size { get; set; }

        // IntPtr NativeHandle { get; }

        // event EventHandler<InputEventArgs> InputReceived;
        // event EventHandler Resized;

        void SetRootComponent(IComponent component);
        void Show();
        void Close();
    }
}