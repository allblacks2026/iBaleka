package Fragments;


import android.Manifest;
import android.app.AlertDialog;
import android.app.Fragment;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
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
    private float speed, caloriesBurnt, stepCount;
    private Session startRunSession; //Session to start run
    private boolean canUpdateLabels = false;

    private OnDataPointListener dataPointListener; //Data Listener for Google FIT api
    private PendingResult<Status> fitnessSessionResult;
    private DataType [] dataTypes;

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
        initializeSessionDataTypes();
        return myView;
    }


    private void initializeSessionDataTypes()
    {
        dataTypes = new DataType[] {DataType.AGGREGATE_CALORIES_EXPENDED, DataType
                .TYPE_CALORIES_EXPENDED, DataType.TYPE_STEP_COUNT_DELTA, DataType
                .TYPE_STEP_COUNT_CUMULATIVE, DataType.TYPE_DISTANCE_DELTA, DataType
                .AGGREGATE_DISTANCE_DELTA, DataType.TYPE_SPEED, DataType.AGGREGATE_SPEED_SUMMARY};
    }

    public void initializeComponents(Bundle savedInstanceState, View view) {
        MapsInitializer.initialize(this.getActivity());
        if (savedInstanceState != null) {
            authorizationInProgress = savedInstanceState.getBoolean("AUTHORIZATION_PENDING");
        }
        startRunMapLayout = (MapView) view.findViewById(R.id.startRunMapLayout);
        startRunMapLayout.getMapAsync(this);
        startRunMapLayout.onCreate(savedInstanceState);
        distanceCoveredTextView = (TextView) view.findViewById(R.id.distanceCoveredLabel);
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
                    .addApi(Fitness.SENSORS_API).addApi(Fitness.SESSIONS_API).addApi(Fitness
                            .RECORDING_API).addScope(activityRead).addScope
                            (fitnessActivityReadWrite).addScope(bodyRead).addScope(bodyReadWrite)
                    .addScope(locationRead).addScope(locationReadWrite)
                            .addConnectionCallbacks(this)
                    .addOnConnectionFailedListener(this).build();
        googleApiClient.connect();
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
        removeFitnessDataListener();
        stopLocationUpdates();
        super.onStop();

    }

    @Override
    public void onConnected(Bundle bundle) {
        if (accessToFitness) {
            receiveLocationUpdate();
            //Get the Google Fit API Data Sources and Register these
            getFitnessDataSources();

        } else {
            displayMessage("Error","Please ensure you have granted access to the app for google " +
                    "fit");
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
        if (!initiated) {
            mapObject.moveCamera(CameraUpdateFactory.newLatLngZoom(new LatLng(runnerLocation
                    .getLatitude(), runnerLocation.getLongitude()), 16));
            initiated = true;
        }

    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.startRunButton:
                canUpdateLabels = true;
                createFitnessSession();



                break;
            case R.id.pauseRunButton:



                break;
            case R.id.endRunButton:
                stopFitnessSession();
                unsubscripeFitnessData();
                canUpdateLabels = false;

                break;

        }
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        mapObject = googleMap;
        mapObject.setMapType(GoogleMap.MAP_TYPE_NORMAL);
        mapObject.setMyLocationEnabled(true);

    }

    private Location getLastKnownLocation() {
        return LocationServices.FusedLocationApi.getLastLocation(googleApiClient);
    }

    private void stopLocationUpdates()
    {
        LocationServices.FusedLocationApi.removeLocationUpdates(googleApiClient, this);
    }

    //Reference:
    private void getFitnessDataSources()
    {
        Fitness.SensorsApi.findDataSources(googleApiClient, new DataSourcesRequest.Builder()
                .setDataTypes(dataTypes[0]).setDataTypes(dataTypes[1]).setDataTypes(dataTypes[2])
                .setDataTypes(dataTypes[3]).setDataTypes(dataTypes[4]).setDataTypes(dataTypes[5])
                .setDataTypes(dataTypes[6]).setDataTypes(dataTypes[7])
                .setDataSourceTypes(DataSource.TYPE_RAW)
                .build()).setResultCallback(new ResultCallback<DataSourcesResult>() {
            @Override public void onResult(@NonNull DataSourcesResult dataSourcesResult) {
                     for (DataSource dataSource : dataSourcesResult.getDataSources()) {
                        displayToast("Data source found: "+dataSource.getName());
                         registerFitnessDataListener(dataSource, dataSource.getDataType());
                     }
            }
        });
    }

    private void createDataPointListener() {
        if (dataPointListener == null) {
            dataPointListener = new OnDataPointListener() {
                public void onDataPoint(DataPoint dataPoint) {
                    for (final Field field : dataPoint.getDataType().getFields()) {

                        final Value dataValue = dataPoint.getValue(field);
                        Runnable runnable = new Runnable() {
                            @Override
                            public void run() {
                                displayToast("Data Field: "+field.getName() +" Value: "+dataValue);
                            }
                        };

                    }

                }
            };
        }
    };

    private void registerFitnessDataListener(DataSource dataSource, DataType dataType) {

        createDataPointListener(); //Set up the data point listener
        //Register listener
        Fitness.SensorsApi.add(googleApiClient, new SensorRequest.Builder().setDataSource
                (dataSource).setDataType(dataType).setSamplingRate(1, TimeUnit.SECONDS).build(),
                dataPointListener).setResultCallback(new ResultCallback<Status>() {
            @Override
            public void onResult(@NonNull Status status) {
                if (status.isSuccess()) {
                    displayToast("Listener is registered");
                } else {
                    displayToast("Listener is not registered");
                }
            }
        });
        //Add subscription to recording API
        subscribeToFitnessData(dataType);
    }

    private void removeFitnessDataListener()
    {
        Fitness.SensorsApi.remove(googleApiClient, dataPointListener);
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
                        displayToast("Already Subscribed");
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
                        displayToast("Already subscribed");
                    } else if (status.getStatusCode() == FitnessStatusCodes.SUCCESS) {
                        displayToast("Subscribed");
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
        totalTimeTextView.setText(Long.toString(startRunSession.getActiveTime(TimeUnit.SECONDS)));
        displayToast("Total Steps taken: "+stepCount);
        caloriesBurntTextView.setText(Double.toString(caloriesBurnt));
    }
}