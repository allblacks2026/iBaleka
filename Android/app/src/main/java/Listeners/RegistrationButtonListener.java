package Listeners;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.view.View;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import Fragments.CreateAccountStepTwoFragment;
import Models.User;
import Utilities.TextSanitizer;
import allblacks.com.Activities.R;

/**
 * Created by Okuhle on 6/26/2016.
 */
public class RegistrationButtonListener implements View.OnClickListener {

    private Activity currentActivity;
    private SharedPreferences appSharedPreferences;
    private SharedPreferences.Editor editor;
    private int selectedDay, selectedMonth, selectedYear;
    private TextView selectedDOB;

    public RegistrationButtonListener(Activity currentActivity) {
        this.currentActivity = currentActivity;
        appSharedPreferences = currentActivity.getSharedPreferences("iBalekaRegistration", Context.MODE_PRIVATE);
        editor = appSharedPreferences.edit();

    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.SelectDateOfBirthButton:
            selectedDOB = (TextView) currentActivity.findViewById(R.id.SelectedDateOfBirthLabel);
                DatePickerDialog dateDialog = new DatePickerDialog(currentActivity, new DatePickerDialog.OnDateSetListener() {
                    @Override
                    public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
                        selectedDay = dayOfMonth;
                        selectedMonth = monthOfYear;
                        selectedYear = year;
                        selectedDOB.setText(selectedDay+"-"+selectedMonth+"-"+selectedYear);
                        editor.putString("DateOfBirth", selectedDay+"-"+selectedMonth+"-"+selectedYear);
                    }}, 2015, 10, 10);
             dateDialog.show();
                break;
            case R.id.NextStepButton:
                TextView enteredName = (TextView) currentActivity.findViewById(R.id.NameEditText);
                TextView enteredSurname = (TextView) currentActivity.findViewById(R.id.SurnameEditText);
                TextView enteredEmail = (TextView) currentActivity.findViewById(R.id.EmailEditText);
                Spinner selectedCountry = (Spinner) currentActivity.findViewById(R.id.CountrySpinner);
                //DatePicker selectedDate = (DatePicker) currentActivity.findViewById(R.id.DateOfBirthPicker);

                if (enteredName.getText().toString().length() != 0 && enteredSurname.getText().toString().trim() != null && enteredEmail.getText().toString().trim() != null || selectedCountry.getSelectedItem().toString().trim() != null) {


                    String name = TextSanitizer.sanitizeText(enteredName.getText().toString(), true);
                    String surname = TextSanitizer.sanitizeText(enteredSurname.getText().toString(), true);
                    String email = TextSanitizer.sanitizeText(enteredEmail.getText().toString(), true);

                    boolean isValidName = TextSanitizer.isValidText(name, 1, 100);
                    boolean isValidSurname = TextSanitizer.isValidText(surname, 1, 100);
                    boolean isValidEmail = TextSanitizer.isValidEmail(email, 1, 100);
                    boolean isSameNameSurname = TextSanitizer.isSameText(name, surname);

                    if (!isValidName) {
                        displayMessage("Invalid Name", "The entered name must be between 1 and 100 characters");
                    } else if (!isValidSurname) {
                        displayMessage("Invalid Email", "The entered surname must be betweeen 1 and 100 characters");
                    } else if (!isValidEmail) {
                        displayMessage("Invalid EmailAddress", "Please ensure you enter a valid email address");
                    }
                    //If these three parameters have been correctly added, save current information, and move to the next fragment
                    if (isValidName && isValidSurname && isValidEmail) {
                        editor.putString("EnteredName", name);
                        editor.putString("EnteredSurname", surname);
                        editor.putString("EnteredEmail", email);
                        editor.putString("DateOfBirth", "jkk");
                        editor.commit();

                        CreateAccountStepTwoFragment nextStepFrag = new CreateAccountStepTwoFragment();
                        FragmentManager mgr = currentActivity.getFragmentManager();
                        FragmentTransaction transaction = mgr.beginTransaction();
                        transaction.replace(R.id.LoginActivityContentArea, nextStepFrag, "NextStepFragment");
                        transaction.addToBackStack("NextStepFragment");
                        transaction.commit();
                    } else {
                        displayMessage("Invalid Registration", "Please note that all fields are required");
                    }
                } else {
                    displayMessage("Please Enter Text", "Please note that all fields are required");
                }
            break;
            case R.id.CreateAccountButton:

                EditText usernameEditText = (EditText) currentActivity.findViewById(R.id.UsernameEditText);
                EditText passwordEditText = (EditText) currentActivity.findViewById(R.id.PasswordEditText);
                EditText securityQuestionEditText = (EditText) currentActivity.findViewById(R.id.SecurityQuestionEditText);
                EditText securityAnswerEditText = (EditText) currentActivity.findViewById(R.id.SecurityAnswerEditText);

                if (usernameEditText.getText().toString() != null && passwordEditText.getText().toString() != null && securityQuestionEditText.getText().toString() != null && securityAnswerEditText.getText().toString() != null) {
                    String username = TextSanitizer.sanitizeText(usernameEditText.getText().toString(), false);
                    String password = TextSanitizer.sanitizeText(passwordEditText.getText().toString(), false);
                    String question = TextSanitizer.sanitizeText(securityQuestionEditText.getText().toString(), false);
                    String answer = TextSanitizer.sanitizeText(securityAnswerEditText.getText().toString(), false);

                    boolean [] isValid = new boolean[3];
                    isValid[0] = TextSanitizer.isValidText(username, 1, 10);
                    isValid[1] = TextSanitizer.isValidText(password, 1, 10);
                    isValid[2] = TextSanitizer.isValidText(question, 1, 10);
                    isValid[3] = TextSanitizer.isValidText(answer, 1, 10);

                    if (isValid[0] && isValid[1] && isValid[2] && isValid[3]) {
                       User newUser = new User("", appSharedPreferences.getString("EnteredName", ""), appSharedPreferences.getString("EnteredSurname", ""), appSharedPreferences.getString("EmailAddress", ""), appSharedPreferences.getString("EnteredCountry", ""), "A", appSharedPreferences.getString("DateOfBirth", ""));
                        //Background task or retrofit
                        //Submit to server
                    } else {
                        displayMessage("Invalid Data Detected", "One or more text fields contain insufienient / invalid data. Please ensure data entered is between 1 and 100 characters");
                    }
                } else {
                    displayMessage("All Fields Required", "One or more fields have missing data. Please note that all fields are required");
                }

        }
    }

    public void displayMessage(String title, String message) {
        AlertDialog.Builder messageBox = new AlertDialog.Builder(currentActivity);
        messageBox.setTitle(title);
        messageBox.setMessage(message);
        messageBox.setPositiveButton("Got it", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

            }
        });
        messageBox.show();
    }
}
