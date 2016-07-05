package BackgroundTasks;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.os.AsyncTask;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import Fragments.AthleteLandingFragment;
import Models.User;
import Utilities.iBalekaSingleton;

/**
 * Created by Okuhle on 5/6/2016.
 */
public class UserGatewayBackgroundTask extends AsyncTask<String, String, String> {

    private Activity currentContext;
    private AlertDialog.Builder messageBox;
    private int actionMode; //0 for log in, 1 for registration, 2 for forgot password
    private ProgressDialog progressDialog;

    public UserGatewayBackgroundTask(Activity currentContext) {
        this.currentContext = currentContext;
        iBalekaSingleton singleton = iBalekaSingleton.getInstance();

    }

    @Override
    protected void onPreExecute() {
        if (actionMode == 0) {
            progressDialog = new ProgressDialog(currentContext);
            progressDialog.setTitle("Login Action");
            progressDialog.setMessage("Please wait while we process your login request");
            progressDialog.show();
        } else if (actionMode == 1) {
            progressDialog.setTitle("Registration Action");
            progressDialog.setMessage("Please wait while we process your registration");
            progressDialog.show();
        } else if (actionMode == 2) {
            progressDialog.setTitle("Forgot Password Action");
            progressDialog.setMessage("Please wait while we locate your account details");
            progressDialog.show();
        }

    }
    //0 for login, 1 for registration, 2 for forgotten password
    public void setExecutionMode(int mode) {
        this.actionMode = mode;
    }

    protected String doInBackground(String... params) {
        String line = null;
        String response = null;

        try {
            if (actionMode == 0) { //login action
                response = null;
                line = null;

                String loginUrl = "http://sict-iis.nmmu.ac.za/AllBlacks/login_user.aspx";
                String enteredUsername = params[0];
                String enteredPassword = params[1];

                URL loginLink = new URL(loginUrl);
                HttpURLConnection loginConnection = (HttpURLConnection) loginLink.openConnection();
                loginConnection.setRequestMethod("POST");
                loginConnection.setDoInput(true);
                loginConnection.setDoOutput(true);
                //Write data to the server
                String loginString = URLEncoder.encode("Username", "utf-8")+"="+URLEncoder.encode
                        (enteredUsername, "utf-8")+URLEncoder.encode("Password", "utf-8")
                        +"="+URLEncoder.encode(enteredPassword, "utf-8");
                OutputStream toServerStream = loginConnection.getOutputStream();
                BufferedWriter toServerWriter = new BufferedWriter(new OutputStreamWriter
                        (toServerStream, "utf-8"));
                toServerWriter.write(loginString);
                toServerWriter.flush();
                toServerWriter.close();
                //Read response from the server
                InputStream fromServerStream = loginConnection.getInputStream();
                BufferedReader fromServerReader = new BufferedReader(new InputStreamReader
                        (fromServerStream, "iso-8859-1"));
                while ((line = fromServerReader.readLine()) != null) {
                    response = response + line;
                }
                fromServerReader.close();
                loginConnection.disconnect();
                return response;
            } else if (actionMode == 1) { //Registration action
                response = null;
                line = null;

                String registerUrl = "http://sict-iis.nmmu.ac.za/AllBlacks/register_user.aspx";

                String enteredName = params[0]; //Name
                String enteredSurname = params[1]; //Surname
                String enteredEmail = params[2]; //Email Address
                String enteredUsername = params[3]; //Username
                String enteredPassword = params[4];
                String securityQuestion = params[5];
                String securityAnswer = params[6];

                String registerString = URLEncoder.encode("Name", "utf-8")+"="+URLEncoder.encode
                        (enteredName, "utf-8")+URLEncoder.encode("Surname", "utf-8")
                        +"="+URLEncoder.encode(enteredSurname, "utf-8")+URLEncoder.encode
                        ("EmailAddress", "utf-8")+"="+URLEncoder.encode(enteredEmail, "utf-8")
                        +URLEncoder.encode("Username", "utf-8")+"="+URLEncoder.encode
                        (enteredUsername, "utf-8")+URLEncoder.encode("Password", "utf-8")
                        +"="+URLEncoder.encode(enteredPassword, "utf-8")+URLEncoder.encode
                        ("SecurityQuestion", "utf-8")+"="+URLEncoder.encode(securityQuestion,
                        "utf-8")+URLEncoder.encode("SecurityAnswer", "utf-8")+"="+URLEncoder
                        .encode(securityAnswer, "utf-8");
                URL registerLink = new URL(registerUrl);
                HttpURLConnection registerConnection = (HttpURLConnection) registerLink
                        .openConnection();
                registerConnection.setRequestMethod("POST");
                registerConnection.setDoOutput(true);
                registerConnection.setDoInput(true);
                //Send data to the server
                OutputStream toServerStream = registerConnection.getOutputStream();
                BufferedWriter toServerWriter = new BufferedWriter(new OutputStreamWriter
                        (toServerStream, "utf-8"));
                toServerWriter.write(registerString);
                toServerWriter.flush();
                toServerWriter.close();

                //Read response from the server
                InputStream fromServerStream = registerConnection.getInputStream();
                BufferedReader fromServerReader = new BufferedReader(new InputStreamReader
                        (fromServerStream, "iso-8859-1"));
                while ((line = fromServerReader.readLine()) != null) {
                    response = response + line;
                }
                fromServerReader.close();
                registerConnection.disconnect();

                return response;
            } else if (actionMode == 2) { //For the forgotten password

                line = null;
                response = null;
                String enteredEmailAddress = params[0];

                String forgotPasswordUrl = "http://sict-iis.nmmu.ac.za/AllBlacks/forgot_password" +
                        ".aspx";

                URL forgotPasswordLink = new URL(forgotPasswordUrl);
                HttpURLConnection forgetPasswordConnection = (HttpURLConnection)
                        forgotPasswordLink.openConnection();
                forgetPasswordConnection.setRequestMethod("POST");
                forgetPasswordConnection.setDoOutput(true);
                forgetPasswordConnection.setDoInput(true);

                String forgotPasswordString = URLEncoder.encode("EmailAddress", "utf-8")
                        +"="+URLEncoder.encode(enteredEmailAddress, "utf-8")+URLEncoder.encode
                        ("Username", "utf-8");
                OutputStream toServerStream = forgetPasswordConnection.getOutputStream();
                BufferedWriter toServerWriter = new BufferedWriter(new OutputStreamWriter
                        (toServerStream, "UTF-8"));
                toServerWriter.write(forgotPasswordString);
                toServerWriter.flush();
                toServerWriter.close();

                InputStream fromServerStream = forgetPasswordConnection.getInputStream();
                BufferedReader fromServerReader = new BufferedReader(new InputStreamReader
                        (fromServerStream, "iso-8859-1"));
                while ((line = fromServerReader.readLine()) != null) {
                    response = response + line;
                }
                fromServerReader.close();
                forgetPasswordConnection.disconnect();
                return response;
            }


            return response;

        } catch (Exception error) {
            if (actionMode == 0) {
                displayMessage("Error Logging In", "Error logging athlete in "+error.getMessage());
            } else if (actionMode == 1) {
                displayMessage("Error Registering Athlete", "Error registering the athlete "+error
                        .getMessage());
            } else if (actionMode == 2) {
                displayMessage("Error Getting Password", "Error getting password information "+error
                        .getMessage());
            }

            return null;
        }
    }

    //This is the after math - lets parse the json
    @Override
    protected void onPostExecute(String s) {
        if (progressDialog.isShowing()) {
            progressDialog.cancel();
        }
        try {
            if (actionMode == 0) { //Login JSON object - this will hold all the user's data
                //This array will hold the JSON array which contains all the user's data
                JSONArray loginArray = new JSONArray(s);
                JSONObject loginObject = loginArray.getJSONObject(0); //We are only getting one
                // login object
                //Let us load the object data into an athlete model
                User loggedInUser = new User(loginObject.getString("UserID"), loginObject.getString("Name"), loginObject.getString("Surname"), loginObject.getString("EmailAddress"), loginObject.getString("Country"), loginObject.getString("UserType"),loginObject.getString("DateOfBirth"));



            }
        } catch (Exception error) {
            if (actionMode == 0){
                displayMessage("Login Error", "Please ensure you have entered the correct " +
                        "username and password");
            } else if (actionMode ==1) {
                displayMessage("Registration Error", "Error Processing Registration"+error.getMessage());
            } else if (actionMode ==2) {
                displayMessage("Forgot Password Error", "Error Processing Forgot Password"+error
                        .getMessage());
            }
        }
    }

    public void displayMessage(String title, String message) {
        AlertDialog.Builder messageBox = new AlertDialog.Builder(currentContext);
        messageBox.setTitle(title);
        messageBox.setMessage(message);
        messageBox.setPositiveButton("Got It", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

            }
        });
        messageBox.show();
    }
}
