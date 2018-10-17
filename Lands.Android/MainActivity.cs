using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;
using System.Configuration;
using Plugin.Permissions;
using System.Runtime.InteropServices;
using Mono.Data.Sqlite;

namespace Lands.Droid
{
    [Activity(
        Label = "Lands", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /*[DllImport("libsqlite.so")]
        public static extern int sqlite3_shutdown();

        [DllImport("libsqlite.so")]
        public static extern int sqlite3_initialize();*/

        protected override void OnCreate(Bundle bundle)
        {
            /*sqlite3_shutdown();
            SqliteConnection.SetConfig(SQLiteConfig.Serialized);
            sqlite3_initialize();*/

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init(true);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(
                requestCode,
               permissions,
               grantResults);
        }
    }
}

