using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace GeoV
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void TestB_Clicked(object sender, EventArgs e)
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                status = results[Permission.Location];

                if (status != PermissionStatus.Granted)
                {
                    string msg = "No tenemos acceso a tu posicion";
                    await DisplayAlert("Alerta", msg, "ok");
                    return;
                }
            }




            Plugin.Geolocator.Abstractions.IGeolocator locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var position = await locator.GetPositionAsync();
            Time.Text = "Time: " + position.Timestamp.LocalDateTime.ToString();
            Lat.Text = "Latitude: " + position.Latitude.ToString();
            Long.Text = "Longitude: " + position.Longitude.ToString();
        }
    }
}
