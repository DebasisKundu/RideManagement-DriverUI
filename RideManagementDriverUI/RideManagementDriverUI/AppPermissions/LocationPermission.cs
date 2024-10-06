using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideManagementDriverUI.AppPermissions
{
    public static class LocationPermission
    {
        public static async Task<bool> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            return status == PermissionStatus.Granted;
        }

        public static async Task<bool> CheckAndConfirmCurrentLocation()
        {
            //todo:: logic - API Call required
            return false;
        }
    }
}
