// using System.ComponentModel;
// using Cardboard.Core.Interfaces;

// using IComponent = Cardboard.Core.Interfaces.IComponent;

// namespace Cardboard.Engine
// {
//     public class App : IApp
//     {
//         private readonly ITreeManager _treeManager;
//         private readonly IRenderer _renderer;
//         private IWindow? _window;
//         private IComponent? _rootComponent;

//         public App(ITreeManager treeManager, IRenderer renderer)
//         {
//             _treeManager = treeManager ?? throw new ArgumentNullException(nameof(treeManager));
//             _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
//         }

//         public void Run()
//         {

//         }

//         public void Exit()
//         {
//             _renderer.Dispose();
//             _window?.Close();
//         }
//     }
// }