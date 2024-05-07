using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using System;
using System.IO;
using System.Linq;
using ZXing;
using ZXing.QrCode;
namespace TesteRemoteControl.Android
{
    [Activity(
        Label = "TesteRemoteControl.Android",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : AvaloniaMainActivity<App>
    {
        public byte[] bytes { get; set; }
        private const int REQUEST_CAMERA_PERMISSION = 1;
        private const int REQUEST_PERMISSIONS_CODE = 1;


        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            return base.CustomizeAppBuilder(builder)
                .WithInterFont()
                .UseReactiveUI();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            GlobalServices.CameraService = new CameraService(this);
            base.OnCreate(savedInstanceState);
            CheckAndRequestPermissions();

        }
        protected override void OnActivityResult(int requestCode, global::Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == REQUEST_CAMERA_PERMISSION && resultCode == global::Android.App.Result.Ok)
            {
                // Obter o bitmap da imagem
                Bundle extras = data.Extras;
                Bitmap imageBitmap = (Bitmap)extras.Get("data");

                if (imageBitmap != null)
                {
                    int width = imageBitmap.Width;
                    int height = imageBitmap.Height;
                    int[] pixels = new int[width * height];
                    imageBitmap.GetPixels(pixels, 0, width, 0, 0, width, height);

                    // Converte os pixels para o array de bytes necessário
                    byte[] pixelsByte = new byte[pixels.Length * 3];
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        int pixel = pixels[i];
                        pixelsByte[i * 3] = (byte)((pixel >> 16) & 0xFF); // Vermelho
                        pixelsByte[i * 3 + 1] = (byte)((pixel >> 8) & 0xFF); // Verde
                        pixelsByte[i * 3 + 2] = (byte)(pixel & 0xFF); // Azul
                    }
                    
                    // Cria um source de luminância compatível com ZXing a partir do array de bytes
                    LuminanceSource source = new RGBLuminanceSource(pixelsByte, width, height);
                    BarcodeReaderGeneric reader = new();
                    var result = reader.Decode(source);

                    if (result != null)
                        {
                        ((CameraService)GlobalServices.CameraService).HandleQrCodeDecoded(result.Text);
                    }
                    
                }
            }
        }
        private void CheckAndRequestPermissions()
        {
            string[] permissionsNeeded = {
        Manifest.Permission.Camera,
        Manifest.Permission.WriteExternalStorage
    };

            var permissionsToRequest = permissionsNeeded.Where(permission => ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted).ToList();

            if (permissionsToRequest.Any())
            {
                ActivityCompat.RequestPermissions(this, permissionsToRequest.ToArray(), REQUEST_PERMISSIONS_CODE);
            }
            else
            {
                // Todas as permissões já estão concedidas. Faça o que precisa ser feito.
            }
        }
    }
    public class CameraService : ICameraService
    {
        private readonly Activity _activity;
        public event EventHandler<string> QrCodeTextChanged;

        private string _qrCodeText;

        public string QrCodeText
        {
            get { return _qrCodeText; }
            private set
            {
                if (_qrCodeText != value)
                {
                    _qrCodeText = value;
                    QrCodeTextChanged?.Invoke(this, value);
                }
            } 
        }
        public CameraService(Activity activity)
        {
            _activity = activity;
        }
        public void OpenCamera()
        {
            StartCamera();
        }
        public async void StartCamera()
        {
            
            Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);

            // Verifica se existe uma activity de câmera para lidar com a intent
            if (takePictureIntent.ResolveActivity(_activity.PackageManager) != null)
            {
                _activity.StartActivityForResult(takePictureIntent, 1);
            }
        }
        public void HandleQrCodeDecoded(string qrCodeText)
        {
            QrCodeText = qrCodeText;
        }
    }
}
