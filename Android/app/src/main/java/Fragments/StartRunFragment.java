package Fragments;

import android.Manifest;
import android.app.AlertDialog;
import android.app.Fragment;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Bundle;
import android.renderscript.Element;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.fitness.Fitness;
import com.google.android.gms.fitness.FitnessActivities;
import com.google.android.gms.fitness.data.DataPoint;
import com.google.android.gms.fitness.data.DataSource;
import com.google.android.gms.fitness.data.DataType;
import com.google.android.gms.fitness.data.Field;
import com.google.android.gms.fitness.data.Session;
import com.google.android.gms.fitness.data.Value;
import com.google.android.gms.fitness.request.DataSourcesRequest;
import com.google.android.gms.fitness.request.OnDataPointListener;
import com.google.android.gms.fitness.request.SensorRequest;
import com.google.android.gms.fitness.result.DataSourcesResult;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.location.LocationSettingsRequest;
import com.google.android.gms.location.LocationSettingsResult;
import com.google.android.gms.location.LocationSettingsStatusCodes;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.LatLng;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.concurrent.TimeUnit;

import allblacks.com.Activities.R;

public class StartRunFragment extends Fragment implements OnMapReadyCallback, GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener, View.OnClickListener {

    private MapView startRunMapView;
    private Button startRunButton;
    private Button pauseRunButton;
    private Button endRunButton;
    private TextView elapsedTimeTextView;
    private TextView distanceCoveredTextView;
    private TextView caloriesBurntTextView;
    private TextView averageSpeedTextView;

    private GoogleApiClient googleApiClient;
    private LocationRequest locationRequest;
    private Location currentLocation;

    private int fineLocationRequestCode = 200;
    private boolean canContinue = false;
    final static int GPS_CHECK_SETTING = 100;

    //Our data point listeners
    private OnDataPointListener caloriesListener;
    private OnDataPointListener speedListener;
    private OnDataPointListener cummulativeDistanceListener;
    private List<Float> cummulativeDistance;
    private List<Float> speedList;
    private List<Float> caloriesList = new ArrayList<>();

    private GoogleMap startRunMap;
    //Fitness Session
    private Session fitnessSession;
    private final static int REQUEST_FIT_AUTHORIZATION = 1;
    private final static String REQUEST_FIT_IN_PROGRESS = "auth_state_pending";
    private boolean googleFitAuthorizationInProgress = false;


    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View currentView = inflater.inflate(R.layout.fragment_start_run, container, false);
        initializeComponents(currentView, savedInstanceState);
        cummulativeDistance = new ArrayList<Float>();
        speedList = new ArrayList<>();
        return currentView;
    }

    private void initializeComponents(View currentView, Bundle savedInstanceState) {
        startRunMapView = (MapView) currentView.findViewById(R.id.startRunMapLayout);
        startRunMapView.onCreate(savedInstanceState);
        startRunMapView.getMapAsync(this);
        startRunButton = (Button) currentView.findViewById(R.id.startRunButton);
        startRunButton.setOnClickListener(this);
        endRunButton = (Button) currentView.findViewById(R.id.endRunButton);
        endRunButton.setOnClickListener(this);
        pauseRunButton = (Button) currentView.findViewById(R.id.pauseRunButton);
        pauseRunButton.setOnClickListener(this);
        elapsedTimeTextView = (TextView) currentView.findViewById(R.id.elapsedTimeText);
        distanceCoveredTextView = (TextView) currentView.findViewById(R.id.distanceCoveredTextView);
        caloriesBurntTextView = (TextView) currentView.findViewById(R.id.totalCaloriesBurntTextView);
        averageSpeedTextView = (TextView) currentView.findViewById(R.id.runnerSpeedTextView);

        if (savedInstanceState != null) {
            googleFitAuthorizationInProgress = savedInstanceState.getBoolean(REQUEST_FIT_IN_PROGRESS);
        }

        buildLocationRequest();
        buildGoogleApi();
        buildLocationSettings();
        receiveLocationUpdates();

    }

    private void buildGoogleApi() {
        googleApiClient = new GoogleApiClient.Builder(this.getActivity())
                .addApi(Fitness.SENSORS_API)
                .addApi(Fitness.HISTORY_API)
                .addApi(Fitness.RECORDING_API)
                .addApi(LocationServices.API)
                .addScope(new Scope(Scopes.FITNESS_ACTIVITY_READ_WRITE))
                .addScope(new Scope(Scopes.FITNESS_BODY_READ_WRITE))
                .addScope(new Scope(Scopes.FITNESS_LOCATION_READ_WRITE))
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .build();
        googleApiClient.connect();

    }

    private void buildLocationRequest() {
        locationRequest = new LocationRequest();
        locationRequest.setInterval(3000);
        locationRequest.setFastestInterval(1000);
        locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
    }

    @Override
    public void onStart() {
        super.onStart();
        googleApiClient.connect();
    }

    private void buildLocationSettings() {
        LocationSettingsRequest.Builder locationSettings = new LocationSettingsRequest.Builder();
        locationSettings.addLocationRequest(locationRequest);
        PendingResult<LocationSettingsResult> locationResult = LocationServices.SettingsApi.checkLocationSettings(googleApiClient, locationSettings.build());
        locationResult.setResultCallback(new ResultCallback<LocationSettingsResult>() {
            @Override
            public void onResult(@NonNull LocationSettingsResult locationSettingsResult) {
                final Status locationResult = locationSettingsResult.getStatus();
                switch (locationResult.getStatusCode()) {
                    case LocationSettingsStatusCodes.SUCCESS:
                        canContinue = true;
                        break;
                    case LocationSettingsStatusCodes.RESOLUTION_REQUIRED:
                        displayMessage("GPS Required", "Please check your GPS Settings");
                        canContinue = false;
                        try {
                            locationResult.startResolutionForResult(getActivity(), GPS_CHECK_SETTING);
                        } catch (Exception error) {
                            displayMessage("Error Resolving Request", "An error occurred while resolving the GPS Request");
                            canContinue = false;
                        }
                        break;
                    case LocationSettingsStatusCodes.SETTINGS_CHANGE_UNAVAILABLE:
                        displayMessage("Settings Unavailable", "GPS Settings are unavailable");
                        canContinue = false;
                        break;
                }
            }
        });
    }

    private void receiveLocationUpdates() {
        if (canContinue) {
            LocationListener locationListener = new LocationListener() {
                @Override
                public void onLocationChanged(Location location) {
                    currentLocation = location;
                }
            };
            checkPermissions();
            LocationServices.FusedLocationApi.requestLocationUpdates(googleApiClient, locationRequest, locationListener);
        }

    }

    private void checkPermissions()
    {
        if (ActivityCompat.checkSelfPermission(getActivity(), Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(getActivity(), new String[] {Manifest.permission.ACCESS_FINE_LOCATION}, 200);
            return;
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        switch (requestCode) {
            case 200:
                for (int a = 0; a < permissions.length; a++) {
                    if (grantResults[a] == PackageManager.PERMISSION_GRANTED) {
                        canContinue = true;
                    } else {
                        canContinue = false;
                    }
                }
                break;
            default:
                super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        startRunMap = googleMap;

        if (canContinue) {
            //This permission check somehow works with Android 6.
            checkPermissions();
            startRunMap.setMyLocationEnabled(true);
            startRunMap.setMapType(GoogleMap.MAP_TYPE_HYBRID);
            startRunMap.animateCamera(CameraUpdateFactory.newLatLngZoom(new LatLng(currentLocation.getLatitude(), currentLocation.getLatitude()), 18));
        }
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putBoolean(REQUEST_FIT_IN_PROGRESS, googleFitAuthorizationInProgress);
    }

    @Override
    public void onPause() {
        startRunMapView.onPause();
        super.onPause();
    }

    @Override
    public void onResume() {
        super.onResume();
        googleApiClient.connect();
        startRunMapView.onResume();
    }

    @Override
    public void onDestroy() {
        startRunMapView.onDestroy();
        super.onDestroy();
    }

    @Override
    public void onConnected(@Nullable Bundle bundle) {

        getAvailableDataSources();

    }

    @Override
    public void onConnectionSuspended(int i) {

    }

    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        try {
            googleFitAuthorizationInProgress = true;
            connectionResult.startResolutionForResult(getActivity(), REQUEST_FIT_AUTHORIZATION);
        } catch (Exception error) {

        }
    }



    private void displayMessage(String title, String message) {
        AlertDialog.Builder alertBox = new AlertDialog.Builder(getActivity());
        alertBox.setTitle(title);
        alertBox.setMessage(message);
        alertBox.setPositiveButton("Got It", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

            }
        });
        alertBox.show();
    }
    private void registerFitnessDataListener(final DataType dataType, DataSource source) {
        SensorRequest request = new SensorRequest.Builder()
                .setDataSource(source)
                .setDataType(dataType)
                .setSamplingRate(1, TimeUnit.SECONDS)
                .build();

        if (dataType.equals(DataType.TYPE_SPEED)) {
            speedListener = new OnDataPointListener() {
                @Override
                public void onDataPoint(DataPoint dataPoint) {
                    for (final Field dataField : dataPoint.getDataType().getFields()) {
                        final Value speedValue = dataPoint.getValue(dataField);
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                speedList.add(speedValue.asFloat());
                                displayToast("Current Speed: "+speedValue.asFloat());
                            }
                        });
                    }
                }
            };
            Fitness.SensorsApi.add(googleApiClient, request, speedListener)
                    .setResultCallback(new ResultCallback<Status>() {
                        @Override
                        public void onResult(@NonNull Status status) {
                            if (status.isSuccess()) {
                                displayToast("Sensor successfully connected");
                            }
                        }
                    });
        }

        if (dataType.equals(DataType.TYPE_DISTANCE_CUMULATIVE)) {
            cummulativeDistanceListener = new OnDataPointListener() {
                @Override
                public void onDataPoint(DataPoint dataPoint) {
                    for (final Field dataField : dataPoint.getDataType().getFields()) {
                        final Value distanceValue = dataPoint.getValue(dataField);
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                cummulativeDistance.add(distanceValue.asFloat());
                                displayToast("Total Distance: "+distanceValue.asFloat());
                            }
                        });
                    }
                }
            };

            Fitness.SensorsApi.add(googleApiClient, request, cummulativeDistanceListener)
                    .setResultCallback(new ResultCallback<Status>() {
                        @Override
                        public void onResult(@NonNull Status status) {
                            if (status.isSuccess()) {
                                displayToast("Speed Sensor Successfully Connected!");
                            }
                        }
                    });
        }

        if (dataType.equals(DataType.TYPE_CALORIES_EXPENDED)) {
            caloriesListener = new OnDataPointListener() {
                @Override
                public void onDataPoint(DataPoint dataPoint) {
                    for (final Field dataField : dataPoint.getDataType().getFields()) {
                        final Value caloriesValue = dataPoint.getValue(dataField);
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {

                                caloriesList.add(caloriesValue.asFloat());
                                displayToast("Calories: "+caloriesValue.asFloat());
                            }
                        });
                    }
                }
            };

            Fitness.SensorsApi.add(googleApiClient, request, cummulativeDistanceListener)
                    .setResultCallback(new ResultCallback<Status>() {
                        @Override
                        public void onResult(@NonNull Status status) {
                            if (status.isSuccess()) {
                                displayToast("Speed Sensor Successfully Connected!");
                            }
                        }
                    });
        }
    }
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == REQUEST_FIT_AUTHORIZATION) {
            googleFitAuthorizationInProgress = false;
            if (resultCode == getActivity().RESULT_OK) {
                if (!googleApiClient.isConnecting() && !googleApiClient.isConnected()) {
                    googleApiClient.connect();
                }
            } else if (resultCode == getActivity().RESULT_CANCELED) {
                displayToast("Connection to Google Cancelled");
            } else {
                displayToast("Error Connecting to Google");
            }
        }
    }

    private void displayToast(String s) {
        Toast.makeText(getActivity(), s, Toast.LENGTH_LONG).show();
    }

    private void getAvailableDataSources()
    {
        //Set up the needed Data Sources
       DataSourcesRequest speedDataSource = new DataSourcesRequest.Builder()
               .setDataTypes(DataType.TYPE_SPEED)
               .setDataSourceTypes(DataSource.TYPE_RAW)
               .build();

        DataSourcesRequest distanceDataSource = new DataSourcesRequest.Builder()
                .setDataTypes(DataType.TYPE_DISTANCE_CUMULATIVE)
                .setDataSourceTypes(DataSource.TYPE_DERIVED)
                .build();

        DataSourcesRequest caloriesDataSource = new DataSourcesRequest.Builder()
                .setDataTypes(DataType.TYPE_CALORIES_EXPENDED)
                .setDataSourceTypes(DataSource.TYPE_RAW)
                .build();

        //Set up the result callbacks

        ResultCallback<DataSourcesResult> speedResultCallback = new ResultCallback<DataSourcesResult>() {
            @Override
            public void onResult(@NonNull DataSourcesResult dataSourcesResult) {
                for (DataSource ds : dataSourcesResult.getDataSources()) {
                    if (DataType.TYPE_SPEED.equals(ds.getDataType())) {
                        registerFitnessDataListener(ds.getDataType(), ds);
                    }
                }
            }
        };

        ResultCallback<DataSourcesResult> distanceResultCallback = new ResultCallback<DataSourcesResult>() {
            @Override
            public void onResult(@NonNull DataSourcesResult dataSourcesResult) {
                for (DataSource ds : dataSourcesResult.getDataSources()) {
                    if (DataType.TYPE_DISTANCE_CUMULATIVE.equals(ds.getDataType())) {
                        registerFitnessDataListener(ds.getDataType(), ds);
                    }
                }
            }
        };

        ResultCallback<DataSourcesResult> caloriesResultCallback = new ResultCallback<DataSourcesResult>() {
            @Override
            public void onResult(@NonNull DataSourcesResult dataSourcesResult) {
                for (DataSource ds : dataSourcesResult.getDataSources()) {
                    if (DataType.TYPE_CALORIES_EXPENDED.equals(ds.getDataType())) {
                        registerFitnessDataListener(ds.getDataType(), ds);
                    }
                }
            }
        };

        //Find the data sources
        Fitness.SensorsApi.findDataSources(googleApiClient, speedDataSource).setResultCallback(speedResultCallback);
        Fitness.SensorsApi.findDataSources(googleApiClient, distanceDataSource).setResultCallback(distanceResultCallback);
        Fitness.SensorsApi.findDataSources(googleApiClient, caloriesDataSource).setResultCallback(caloriesResultCallback);
    }

    private void createFitnessSession()
    {
        fitnessSession = new Session.Builder()
                .setName("iBaleka Running Session")
                .setIdentifier(getString(R.string.app_name) + " " + System.currentTimeMillis())
                .setDescription("iBaleka Running Session")
                .setStartTime(Calendar.getInstance().getTimeInMillis(), TimeUnit.MILLISECONDS)
                .setActivity(FitnessActivities.RUNNING_JOGGING)
                .build();
    }

    private void stopFitnessSession()
    {
        Fitness.SessionsApi.stopSession(googleApiClient, "iBaleka Running Session");
    }


    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.startRunButton:
                createFitnessSession();
                break;
            case R.id.endRunButton:
                stopFitnessSession();
                break;
        }
    }
}