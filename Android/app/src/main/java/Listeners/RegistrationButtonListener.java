package Listeners;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.view.ActionMode;
import android.view.View;
import android.widget.Spinner;
import android.widget.TextView;

import Fragments.CreateAccountStepTwoFragment;
import Utilities.TextSanitizer;
import allblacks.com.ibaleka_android_prototype.R;

/**
 * Created by Okuhle on 6/26/2016.
 */
public class RegistrationButtonListener implements View.OnClickListener {

    private Activity currentActivity;
    private SharedPreferences appSharedPreferences;
    private SharedPreferences.Editor editor;

    public RegistrationButtonListener(Activity currentActivity) {
        this.currentActivity = currentActivity;
        appSharedPreferences = currentActivity.getSharedPreferences("iBalekaRegistration", Context.MODE_PRIVATE);
        editor = appSharedPreferences.edit();
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.NextStepButton:
                TextView enteredName = (TextView) currentActivity.findViewById(R.id.NameEditText);
                TextView enteredSurname = (TextView) currentActivity.findViewById(R.id.SurnameEditText);
                TextView enteredEmail = (TextView) currentActivity.findViewById(R.id.EmailEditText);
                Spinner accountType = (Spinner) currentActivity.findViewById(R.id.AccountTypeSpinner);

                if (enteredName.getText().toString().trim() != null && enteredSurname.getText().toString().trim() != null && enteredEmail.getText().toString().trim() != null || accountType.getSelectedItem().toString().trim() != null) {
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

                        CreateAccountStepTwoFragment nextStepFrag = new CreateAccountStepTwoFragment();
                        FragmentManager mgr = currentActivity.getFragmentManager();
                        FragmentTransaction transaction = mgr.beginTransaction();
                        transaction.replace(R.id.LoginActivityContentArea, nextStepFrag, "NextStepFragment");
                        transaction.addToBackStack("NextStepFragment");
                        transaction.commit();
                    }

                } else {
                    displayMessage("Please Enter Text", "Please note that all fields are required");
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
