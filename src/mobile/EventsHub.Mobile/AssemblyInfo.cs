using Android.App;
using Xamarin.Forms.Xaml;

[assembly: Application(UsesCleartextTraffic = true)]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
