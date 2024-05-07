using System;

namespace TesteRemoteControl
{
    public interface ICameraService
    {
        event EventHandler<string> QrCodeTextChanged;

        void OpenCamera();
    }
}