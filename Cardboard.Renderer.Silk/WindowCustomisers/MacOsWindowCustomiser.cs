using System.Runtime.InteropServices;

namespace Cardboard.Renderer.Silk.WindowCustomisers
{
    public static class MacOsWindowCustomiser
    {
        private const string ObjCLib = "/usr/lib/libobjc.A.dylib";

        [DllImport(ObjCLib, EntryPoint = "objc_msgSend")]
        private static extern void void_objc_msgSend_IntPtr(nint receiver, nint selector, nint arg1);

        [DllImport(ObjCLib, EntryPoint = "objc_msgSend")]
        private static extern nint nint_objc_msgSend(nint receiver, nint selector);

        [DllImport(ObjCLib, EntryPoint = "objc_msgSend")]
        private static extern void void_objc_msgSend_Bool(nint receiver, nint selector, bool arg1);

        [DllImport(ObjCLib, EntryPoint = "sel_registerName")]
        private static extern nint sel_registerName(string selectorName);

        public static void EnableNativeDragAndTransparency(nint nsWindow)
        {
            if (!OperatingSystem.IsMacOS())
                throw new PlatformNotSupportedException("This function is only supported on macOS.");

            // Enable titlebar transparency
            var selSetTitlebarAppearsTransparent = sel_registerName("setTitlebarAppearsTransparent:");
            void_objc_msgSend_Bool(nsWindow, selSetTitlebarAppearsTransparent, true);

            // Get current styleMask
            var selStyleMask = sel_registerName("styleMask");
            var styleMask = nint_objc_msgSend(nsWindow, selStyleMask);

            // NSWindowStyleMaskFullSizeContentView = 1 << 15 = 0x00008000
            const nint NSWindowStyleMaskFullSizeContentView = 0x00008000;
            styleMask |= NSWindowStyleMaskFullSizeContentView;

            // Set new styleMask
            var selSetStyleMask = sel_registerName("setStyleMask:");
            void_objc_msgSend_IntPtr(nsWindow, selSetStyleMask, styleMask);

            // Allow moving the window by dragging the background
            var selSetMovableByBackground = sel_registerName("setMovableByWindowBackground:");
            void_objc_msgSend_Bool(nsWindow, selSetMovableByBackground, true);
        }
    }
}