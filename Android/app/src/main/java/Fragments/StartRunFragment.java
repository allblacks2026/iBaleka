package Fragments;


import android.Manifest;
import android.annotation.TargetApi;
import android.app.AlertDialog;
import android.app.Fragment;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
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
import com.google.android.gms.fitness.FitnessStatusCodes;
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
import com.google.android.gms.fitness.result.SessionStopResult;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.location.LocationSettingsRequest;
import com.google.android.gms.location.LocationSettingsResult;
import com.google.android.gms.location.LocationSettingsStates;
import com.google.android.gms.location.LocationSettingsStatusCodes;
import com.google.android.gms.maps.CameraUpdate;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.MapsInitializer;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.search.SearchAuth;

import java.util.Calendar;
import java.util.concurrent.RunnableFuture;
import java.util.concurrent.TimeUnit;

import Utilities.iBalekaSingleton;
import allblacks.com.ibaleka_android_prototype.R;

public class StartRunFragment extends Fragment implements GoogleApiClient
        .OnConnectionFailedListener, GoogleApiClient.ConnectionCallbacks,
        com.google.android.gms.location.LocationListener, View.OnClickListener, OnMapReadyCallback {

    private GoogleApiClient googleApiClient;
    private LocationSettingsRequest.Builder locationSettingsBuilder;
    private PendingResult<LocationSettingsResult> locationSettingsResult;
    private PendingResult<SessionStopResult> fitnessStopResult;
    private TextView distanceCoveredTextView;
    private TextView totalTimeTextView;
    private TextView caloriesBurntTextView;
    private TextView runnerSpeedTextView;
    private Location runnerLocation;
    private boolean keepRecording = false;
    private Button startRun, pauseRun, endRun;
    private LocationRequest locationRequest;
    private MapView startRunMapLayout;
    private GoogleMap mapObject;
    private static final int OAUTH_REQUEST_CODE = 1;
    private static final String AUTHORIZATION_PENDING = "auth_state_pending";
    private static final int GPS_REQUEST_CODE = 3;
    private boolean authorizationInProgress = false;
    private boolean accessToFitness = true;
    private static final int REQUEST_SETTINGS = 2;
    private boolean initiated = false;
    private float distanceCovered = 0, caloriesBurnt = 0, stepCount = 0;
    private Session startRunSession; //Session to start run
    private boolean canUpdateLabels = false;


    private PendingResult<Status> fitnessSessionResult;
    private DataType [] dataTypes;
    private final static int REQUEST_APPLICATION_PERMISSIONS = 200;
    private boolean accessGranted;
    private OnDataPointListener stepCountDeltaListener;
    private OnDataPointListener distanceDeltaListener;
    private OnDataPointListener caloriesExpendedListener;


    public StartRunFragment() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View myView = inflater.inflate(R.layout.fragment_start_run, container, false);
        initializeComponents(savedInstanceState, myView);
        buildFitnessApi();
        buildLocationSettingsRequest();
        return myView;
    }



    public void initializeComponents(Bundle savedInstanceState, View view) {
        MapsInitializer.initialize(this.getActivity());
        if (savedInstanceState != null) {
            authorizationInProgress = savedInstanceState.getBoolean("AUTHORIZATION_PENDING");
        }
        startRunMapLayout = (MapView) view.findViewById(R.id.startRunMapLayout);
        startRunMapLayout.getMapAsync(this);
        startRunMapLayout.onCreate(savedInstanceState);
        distanceCoveredTextView = (TextView) view.findViewById(R.id.distanceCoveredText);
        totalTimeTextView = (TextView) view.findViewById(R.id.elapsedTimeLabel);
        caloriesBurntTextView = (TextView) view.findViewById(R.id.totalCaloriesBurntTextView);
        runnerSpeedTextView = (TextView) view.findViewById(R.id.runnerSpeedTextView);

        startRun = (Button) view.findViewById(R.id.startRunButton);
        pauseRun = (Button) view.findViewById(R.id.pauseRunButton);
        endRun = (Button) view.findViewById(R.id.endRunButton);

        startRun.setOnClickListener(this);
        endRun.setOnClickListener(this);
        pauseRun.setOnClickListener(this);
        iBalekaSingleton.setContext(this.getActivity());
        createLocationRequest();
    }

    private void buildFitnessApi() {

        Scope activityRead = new Scope(Scopes.FITNESS_ACTIVITY_READ);
        Scope fitnessActivityReadWrite = new Scope(Scopes.FITNESS_ACTIVITY_READ_WRITE);
        Scope bodyRead = new Scope(Scopes.FITNESS_BODY_READ);
        Scope bodyReadWrite = new Scope(Scopes.FITNESS_BODY_READ_WRITE);
        Scope locationRead = new Scope(Scopes.FITNESS_LOCATION_READ);
        Scope locationReadWrite = new Scope(Scopes.FITNESS_LOCATION_READ_WRITE);

            googleApiClient = new GoogleApiClient.Builder(this.getActivity()).addApi(LocationServices.API)
                    .addApi(Fitness.SENSORS_API).addApi(Fitness.HISTORY_API).addApi(Fitness.SESSIONS_API)
                            .addApi(Fitness
                            .RECORDING_API).addScope(activityRead).addScope
                            (fitnessActivityReadWrite).addScope(bodyRead).addScope(bodyReadWrite)
                    .addScope(locationRead).addScope(locationReadWrite)
                            .addConnectionCallbacks(this)
                    .addOnConnectionFailedListener(this).build();
    }

    //This method defines the location request - a location request specifies how location data
    // should be delivered
    private void createLocationRequest() {
        locationRequest = new LocationRequest();
        locationRequest.setInterval(3000); //Get location data every 10 seconds
        locationRequest.setFastestInterval(1000); //The fastest time to get a request is 1.5 seconds
        locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY); //Use the highest
        // accuracy possible - other accuracy settings aern't so fine.

    }

    //Reference: https://developer.android.com/training/location/change-location-settings.html
    private void buildLocationSettingsRequest() {
        locationSettingsBuilder = new LocationSettingsRequest.Builder().addLocationRequest
                (locationRequest);
        checkLocationSettingsResult();
    }

    private void checkLocationSettingsResult() {
        locationSettingsResult = LocationServices.SettingsApi.checkLocationSettings(googleApiClient,
                locationSettingsBuilder.build());
        locationSettingsResult.setResultCallback(new ResultCallback<LocationSettingsResult>() {
            @Override
            public void onResult(@NonNull LocationSettingsResult locationSettingsResult) {
                final Status status = locationSettingsResult.getStatus();
                final LocationSettingsStates states = locationSettingsResult
                        .getLocationSettingsStates();
                switch (status.getStatusCode()) {
                    case LocationSettingsStatusCodes.SUCCESS:
                        //If successfull
                        accessToFitness = true;

                        break;
                    case LocationSettingsStatusCodes.RESOLUTION_REQUIRED:
                        try {
                            status.startResolutionForResult(getActivity(), REQUEST_SETTINGS);


                        } catch (Exception error) {
                            displayMessage("Intent Error", error.getMessage());
                        }
                        break;
                    case LocationSettingsStatusCodes.SETTINGS_CHANGE_UNAVAILABLE:
                        displayMessage("Change Unavailable", "System cannot change settings. " +
                                "Please consider changing these manually");
                        break;
                }
            }
        });
    }

    private void receiveLocationUpdate() {//Check to see if this can be solved - android 6
        checkPermission();
        LocationServices.FusedLocationApi.requestLocationUpdates(googleApiClient,
                locationRequest, this);
    }

    @Override
    public void onStart() {
        super.onStart();
        googleApiClient.connect();

    }

    @Override
    public void onStop() {
        googleApiClient.disconnect();
        stopLocationUpdates();
        super.onStop();

    }

    @Override
    public void onConnected(Bundle bundle) {
        if (googleApiClient.isConnecting()) {
            googleApiClient.connect();
        } else {
            receiveLocationUpdate();
            getFitnessDataSources();
        }
    }

    @Override
    public void onConnectionSuspended(int i) {
        if (i == GoogleApiClient.ConnectionCallbacks.CAUSE_NETWORK_LOST) {
            displayMessage("Connection Lost", "Network connection has been lost");
        } else if (i == GoogleApiClient.ConnectionCallbacks.CAUSE_SERVICE_DISCONNECTED) {
            displayMessage("Connection Lost", "Connection has been disconnected");
        }
    }

    @Override
    public void onConnectionFailed(ConnectionResult connectionResult) {
        try {
            if (!authorizationInProgress) {
                authorizationInProgress = true;
                connectionResult.startResolutionForResult(getActivity(), OAUTH_REQUEST_CODE);

            } else {
                Log.i("Google Fit", "Authorization in progress");
            }
        } catch (Exception error) {
            displayMessage("Error", error.getMessage());
        }
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == OAUTH_REQUEST_CODE) {
            authorizationInProgress = false;
            if (resultCode == getActivity().RESULT_OK) {
                if (!googleApiClient.isConnecting() && !googleApiClient.isConnected()) {
                    googleApiClient.connect();
                    accessToFitness = true;
                } else if (resultCode == getActivity().RESULT_CANCELED) {
                    displayMessage("Connection Refused", "You have refused access to fitness " +
                            "information.");
                    accessToFitness = false;
                }
            }
        }
    }

    @Override
    public void onDestroy() {
        startRunMapLayout.onDestroy();
        super.onDestroy();
    }

    @Override
    public void onPause() {
        startRunMapLayout.onPause();
        super.onPause();
    }

    @Override
    public void onResume() {
        super.onResume();
        buildFitnessApi();
        startRunMapLayout.onResume();
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        startRunMapLayout.onSaveInstanceState(outState);
        outState.putBoolean("AUTHORIZATION_PENDING", authorizationInProgress);
        super.onSaveInstanceState(outState);
    }

    @Override
    public void onLowMemory() {
        startRunMapLayout.onLowMemory();
        super.onLowMemory();
    }

    public void displayMessage(String title, String message) {
        AlertDialog.Builder messageBox = new AlertDialog.Builder(this.getActivity());
        messageBox.setTitle(title);
        messageBox.setMessage(message);
        messageBox.setPositiveButton("Got It", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

            }
        });
        messageBox.show();
    }

    @Override
    public void onLocationChanged(Location location) {
        runnerLocation = location;
        mapObject.moveCamera(CameraUpdateFactory.newLatLngZoom(new LatLng(runnerLocation
                        .getLatitude(), runnerLocation.getLongitude()), 16));
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.startRunButton:
                canUpdateLabels = true;
                //createFitnessSession();



                break;
            case R.id.pauseRunButton:



                break;
            case R.id.endRunButton:
                //stopFitnessSession();
                //unsubscripeFitnessData();
                canUpdateLabels = false;

                break;

        }
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        checkPermission();
        mapObject = googleMap;
        mapObject.setMyLocationEnabled(true);

    }
    
    private void stopLocationUpdates()
    {
        LocationServices.FusedLocationApi.removeLocationUpdates(googleApiClient, this);
    }

    //Reference:
    private void getFitnessDataSources()
    {
                Fitness.SensorsApi.findDataSources(googleApiClient, new DataSourcesRequest.Builder()
                        .setDataTypes(DataType.TYPE_LOCATION_SAMPLE)
                        .setDataTypes(DataType.TYPE_STEP_COUNT_DELTA)
                        .setDataTypes(DataType.TYPE_DISTANCE_DELTA)
                        .setDataTypes(DataType.TYPE_CALORIES_EXPENDED)
                        .setDataTypes(DataType.TYPE_ACTIVITY_SEGMENT)
                        .setDataSourceTypes(DataSource.TYPE_RAW)
                        .build())
                        .setResultCallback(new ResultCallback<DataSourcesResult>() {
                            public void onResult(@NonNull DataSourcesResult dataSourcesResult) {
                                displayMessage("Data Sources Result", dataSourcesResult.getStatus()
                                        .toString());
                                for (DataSource currentSource : dataSourcesResult.getDataSources()) {
                                    displayToast("Data Source Found: "+currentSource.getDataType().getName());
                                    if (currentSource.getDataType().equals(DataType.TYPE_STEP_COUNT_DELTA)) {
                                        stepCountDeltaListener = new OnDataPointListener() {
                                            @Override
                                            public void onDataPoint(DataPoint dataPoint)  {
                                                for (final Field currentField : dataPoint.getDataType()
                                                        .getFields()) {
                                                    final Value currentValue = dataPoint.getValue
                                                            (currentField);
                                                    getActivity().runOnUiThread(new Runnable() {
                                                        @Override
                                                        public void run() {
                                                            displayToast("Steps Count: " + currentValue);
                                                            stepCount = stepCount + currentValue.asFloat();
                                                            updateLabels();


                                                        }

                                                    });



                                                }
                                            }
                                        };
                                        registerFitnessDataListener(currentSource, DataType.TYPE_STEP_COUNT_DELTA, stepCountDeltaListener);
                                    }
                                    else if (currentSource.getDataType().equals(DataType
                                            .TYPE_DISTANCE_DELTA)) {
                                        distanceDeltaListener = new OnDataPointListener() {
                                            @Override
                                            public void onDataPoint(DataPoint dataPoint) {
                                                for (final Field currentField : dataPoint.getDataType()
                                                        .getFields()) {
                                                    final Value currentValue = dataPoint.getValue
                                                            (currentField);
                                                    getActivity().runOnUiThread(new Runnable() {
                                                        @Override
                                                        public void run() {
                                                            displayToast("Distance Count: "+ currentValue);
                                                            distanceCovered = distanceCovered + currentValue.asFloat();
                                                            updateLabels();
                                                        }
                                                    });

                                                }
                                            }
                                        };
                                        registerFitnessDataListener(currentSource, DataType
                                                .TYPE_DISTANCE_DELTA, distanceDeltaListener);
                                    } else if (currentSource.getDataType().equals(DataType
                                            .TYPE_CALORIES_EXPENDED)) {
                                        caloriesExpendedListener = new OnDataPointListener() {
                                            @Override
                                            public void onDataPoint(DataPoint dataPoint) {
                                                for (final Field currentField : dataPoint.getDataType()
                                                        .getFields()) {
                                                    final Value currentValue = dataPoint.getValue
                                                            (currentField);
                                                    getActivity().runOnUiThread(new Runnable() {
                                                        @Override
                                                        public void run() {
                                                            displayToast("Calories Count: "+currentValue);
                                                            caloriesBurnt = caloriesBurnt + currentValue
                                                                    .asFloat();
                                                            updateLabels();
                                                        }
                                                    });
                                                }
                                            }
                                        };
                                        registerFitnessDataListener(currentSource, DataType.TYPE_CALORIES_EXPENDED, caloriesExpendedListener);
                                    }
                                }
                            }
                        });
    }

    private void registerFitnessDataListener(DataSource dataSource, DataType dataType,
                                             OnDataPointListener listener) {
        Fitness.SensorsApi.add(googleApiClient, new SensorRequest.Builder()
                .setDataSource(dataSource)
                .setDataType(dataType)
                .setSamplingRate(1, TimeUnit.SECONDS).build(), listener)
                .setResultCallback(new ResultCallback<Status>() {
            @Override
            public void onResult(@NonNull Status status) {
                if (status.isSuccess()) {
                    displayToast("Listener is registered: "+status.getStatusMessage());
                } else {
                    displayToast("Listener is not registered: "+status.getStatusMessage());
                }
            }
        });
        subscribeToFitnessData(dataType);
    }

    private void displayToast(String message) {
        Toast.makeText(getActivity(), message, Toast.LENGTH_LONG).show();
    }

    //Register to fitness recording data
    private void subscribeToFitnessData(DataType dataType) {
        Fitness.RecordingApi.subscribe(googleApiClient, dataType).setResultCallback(new ResultCallback<Status>() {
            @Override
            public void onResult(@NonNull Status status) {
                if (status.isSuccess()) {
                    if (status.getStatusCode() == FitnessStatusCodes.SUCCESS_ALREADY_SUBSCRIBED) {
                        displayToast("You have already been subscribed to fitness data");
                    } else {
                        displayMessage("Error Subscribing to Recording API", "An error occurred " +
                                "while subscribing to recording API");
                    }
                }
            }
        });
    }

    //This method starts a new session
    private void createFitnessSession() {
        if (startRunSession == null) {
            startRunSession = new Session.Builder()
                    .setName("iBaleka_NewRun")
                    .setIdentifier("allblacks.com.ibaleka_android_prototype")
                    .setDescription("This is a new run session started by our athlete")
                    .setStartTime(Calendar.getInstance().getTimeInMillis(), TimeUnit.MILLISECONDS)
                    .setActivity(FitnessActivities.RUNNING)
                    .build();
            fitnessSessionResult = Fitness.SessionsApi.startSession(googleApiClient,
                    startRunSession);
            fitnessSessionResult.setResultCallback(new ResultCallback<Status>() {
                @Override
                public void onResult(@NonNull Status status) {
                    if (status.getStatusCode() == FitnessStatusCodes.SUCCESS_ALREADY_SUBSCRIBED) {
                        displayToast("The session has already been started");
                    } else if (status.getStatusCode() == FitnessStatusCodes.SUCCESS) {
                        displayToast("The session has been started");
                    } else if (status.getStatusCode() == FitnessStatusCodes.ERROR) {
                        displayToast("Error Starting Session");
                    }
                }
            });
        }
    }

    private void stopFitnessSession()
    {
        fitnessStopResult = Fitness.SessionsApi.stopSession(googleApiClient, startRunSession
                .getIdentifier());
        startRunSession = null;
    }

    private void unsubscripeFitnessData()
    {
        for (DataType currentType : dataTypes) {
            Fitness.RecordingApi.unsubscribe(googleApiClient, currentType);
        }
    }

    private void updateLabels()
    {
        distanceCoveredTextView.setText(Float.toString(distanceCovered) + "metres");
        runnerSpeedTextView.setText(Float.toString(stepCount) +" steps");
        caloriesBurntTextView.setText(Float.toString(caloriesBurnt) +" kcal");
    }

    @TargetApi(Build.VERSION_CODES.M)
    private void checkPermission()
    {
       if (ActivityCompat.checkSelfPermission(getActivity(), Manifest.permission.ACCESS_FINE_LOCATION) ==
               PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission
               (getActivity(), Manifest.permission.ACCESS_COARSE_LOCATION) == PackageManager
               .PERMISSION_GRANTED) {
            accessGranted = true;

       } else {
           accessGranted = false;
           if (ActivityCompat.shouldShowRequestPermissionRationale(getActivity(),Manifest.permission
                   .ACCESS_FINE_LOCATION)) {
               displayMessage("Access GPS Receiver", "To collect your running information, we " +
                       "need to make use of your GPS receiver, do you want to grant us permission");
           }
           ActivityCompat.requestPermissions(getActivity(), new String[] {Manifest.permission
                   .ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, REQUEST_APPLICATION_PERMISSIONS);
       }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        if (requestCode == REQUEST_APPLICATION_PERMISSIONS) {
            if (grantResults[0] == PackageManager.PERMISSION_GRANTED && grantResults[1] ==
                    PackageManager.PERMISSION_GRANTED) {
                //Set the needed booleans

            } else {
                displayMessage("Permission Denied", "You have denied permission for access to the" +
                        " GPS Receiver. The application cannot continue");
            }
        } else {
            super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}