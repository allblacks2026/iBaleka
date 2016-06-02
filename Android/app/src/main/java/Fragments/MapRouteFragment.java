package Fragments;


import android.Manifest;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Build;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.*;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.LocationSource;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.MapsInitializer;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.LatLng;

import java.util.ArrayList;
import java.util.List;

import allblacks.com.ibaleka_android_prototype.R;
public class MapRouteFragment extends Fragment implements GoogleApiClient
        .OnConnectionFailedListener, GoogleApiClient.ConnectionCallbacks, LocationSource
        .OnLocationChangedListener, View.OnClickListener, OnMapReadyCallback, GoogleMap.OnMapLongClickListener {

    private GoogleApiClient googleApiObject;
    private Location locationInformation;
    //These hold the permission status
    private int hasFineLocationPermission;
    private int hasCourseLocationPermission;
    private List<String> permissionsList;
    private boolean grantedFineLocation = false;
    private boolean grantedCourseLocation = false;
    private static final int PERMISSIONS_REQUEST_CODE = 30;
    private boolean[] permissionsArray = new boolean[2];
    private LocationRequest currentLocationRequest;
    private MapView mapRouteMapView;
    private GoogleMap mapObject;


    public MapRouteFragment() {
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View myView = inflater.inflate(R.layout.fragment_map_route, container, false);
        checkSystemPermissions();
        initializeMap(savedInstanceState, myView);
        return myView;
    }

    public void initializeMap(Bundle savedInstanceState, View myView)
    {
        googleApiObject = new GoogleApiClient.Builder(getContext()).addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this).addApi(LocationServices.API).build();
        mapRouteMapView = (MapView) myView.findViewById(R.id.mapRouteMapView);
        MapsInitializer.initialize(this.getContext());
        mapRouteMapView.onCreate(savedInstanceState);
        mapRouteMapView.getMapAsync(this);
    }
    public void checkSystemPermissions()
    {
        permissionsList = new ArrayList<>();
        //Start by checking for permission
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            hasFineLocationPermission = getActivity().checkSelfPermission(Manifest.permission
                    .ACCESS_FINE_LOCATION);

            hasCourseLocationPermission = getActivity().checkSelfPermission(Manifest.permission
                    .ACCESS_COARSE_LOCATION);
            //Now we are going to check if PERMISSION_GRANTED was the result. If this isn't the case,
            // we will add the permission to the list - we will ask for this permission.
            if (hasFineLocationPermission != PackageManager.PERMISSION_GRANTED) {
                permissionsList.add(Manifest.permission.ACCESS_FINE_LOCATION);
            } else {
                grantedFineLocation = true;
            }
            if (hasCourseLocationPermission != PackageManager.PERMISSION_GRANTED) {
                permissionsList.add(Manifest.permission.ACCESS_COARSE_LOCATION);
            } else {
                grantedCourseLocation = true;
            }
            //Now we check the permissions array, if it isn't empty, we will ask for those permissions
            if (!permissionsList.isEmpty()) {
                getActivity().requestPermissions(permissionsList.toArray(new String[permissionsList
                        .size()]), PERMISSIONS_REQUEST_CODE);
            }
        }
    }
    //This method handles the result of the permission request
    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        //We check the request code that was passed into this method. If it is equal to our
        // request code, this means we can continue with the application
        switch (requestCode) {
            case PERMISSIONS_REQUEST_CODE:
                //The loop will check if we have been granted permissions
                for (int a = 0; a < permissions.length; a++) {
                    if (grantResults[a] == PackageManager.PERMISSION_GRANTED) {
                        permissionsArray[a] = true;
                        //Experimental: Just to show we have permission
                        Toast.makeText(getContext(), "Permission Granted: " + permissions[a],
                                Toast.LENGTH_LONG).show();
                    } else if (grantResults[a] == PackageManager.PERMISSION_DENIED) {
                        Toast.makeText(getContext(), "You have denied access to: " +
                                "" + permissions[a] + ", app wont work", Toast.LENGTH_LONG).show();
                        permissionsArray[a] = false;
                    }
                }
                if (permissionsArray[0] == false || permissionsArray[1] == false ||
                        grantedCourseLocation != true && grantedFineLocation != true) {
                    getFragmentManager().popBackStackImmediate();
                }
                break;
            default:
                super.onRequestPermissionsResult(requestCode, permissions, grantResults);
                break;
        }
    }

    @Override
    public void onStart() {
        googleApiObject.connect();
        super.onStart();
    }

    @Override
    public void onStop() {
        googleApiObject.disconnect();
        super.onStop();
    }

    @Override
    public void onPause() {
        if (mapRouteMapView != null) {
            mapRouteMapView.onPause();
        }
        super.onPause();
    }

    @Override
    public void onResume() {
        super.onResume();
        if (mapRouteMapView != null) {
            mapRouteMapView.onResume();
        }
    }

    @Override
    public void onDestroy() {
        if (mapRouteMapView != null) {
            try {
                mapRouteMapView.onDestroy();
            } catch (Exception error) {
                displayMessage("Error Killing map", error.getMessage());
            }
        }
        super.onDestroy();
    }

    @Override
    public void onLowMemory() {
        super.onLowMemory();
        if (mapRouteMapView != null) {
            mapRouteMapView.onLowMemory();
        }
    }

    @Override
    public void onConnectionFailed(ConnectionResult connectionResult) {
        displayMessage("Error Connecting to Google Location Service", connectionResult.getErrorMessage());
    }

    @Override
    public void onConnected(Bundle bundle) {
        locationInformation = LocationServices.FusedLocationApi.getLastLocation(googleApiObject);

    }

    @Override
    public void onConnectionSuspended(int i) {

    }

    @Override
    public void onClick(View v) {
    }

    @Override
    public void onLocationChanged(Location location) {
        locationInformation = location;
    }

    public void createLocationUpdater()
    {

    }


    @Override
    public void onMapReady(GoogleMap googleMap) {
        mapObject = googleMap;
        mapObject.setMapType(GoogleMap.MAP_TYPE_TERRAIN);
    }

    public void displayMessage(String title, String message) {
        AlertDialog.Builder messageBox = new AlertDialog.Builder(getContext());
        messageBox.setTitle(title);
        messageBox.setMessage(message);
        messageBox.setPositiveButton("Got it", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

            }
        });
        messageBox.show();
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        if (mapRouteMapView != null) {
            mapRouteMapView.onSaveInstanceState(outState);
        }
    }

    @Override
    public void onMapLongClick(LatLng latLng) {

    }
}