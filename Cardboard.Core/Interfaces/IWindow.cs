using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cardboard.Core.Models;
using Cardboard.Core.Models.Events;

namespace Cardboard.Core.Interfaces
{
    public interface IWindow
    {
        Guid Id { get; }

        // string Title { get; set; }
        // Size Size { get; set; }

        // IntPtr NativeHandle { get; }

        // event EventHandler<InputEventArgs> InputReceived;
        // event EventHandler Resized;
        IntPtr NativeHandle { get; }
        void SetRootComponent(IComponent component);
        void Show();
        void Close();
    }
}