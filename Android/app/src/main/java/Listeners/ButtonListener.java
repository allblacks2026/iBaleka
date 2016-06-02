package Listeners;

import android.app.Activity;


import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.TextView;

import com.afrigis.map.GeoLocation;

import java.util.ArrayList;

import Fragments.AthleteLandingFragment;
import Fragments.CreateAccountStepTwoFragment;
import allblacks.com.ibaleka_android_prototype.MainActivity;
import allblacks.com.ibaleka_android_prototype.R;
import allblacks.com.ibaleka_android_prototype.RegisterUserActivity;

/**
 * Created by Okuhle on 3/25/2016.
 */
public class ButtonListener implements View.OnClickListener {
    private FragmentActivity currentActivity;
    private FragmentManager mgr;
    
    public ButtonListener(FragmentActivity currentActivity) {
        this.currentActivity = currentActivity;
        mgr = currentActivity.getSupportFragmentManager();
    }

    public void onClick(View v) {
       //Code to process any click events
        switch (v.getId()) {
            case R.id.registerAccountbtn:
                Intent registerAccountIntent = new Intent(currentActivity.getApplicationContext()
                        , RegisterUserActivity.class);
                currentActivity.startActivity(registerAccountIntent);
                currentActivity.finish();
                break;
            case R.id.forgotPasswordButton:
                //code for forgotten password
                break;
            case R.id.loginButton:
                //Login validation code should be placed here
                AthleteLandingFragment thisFrag = new AthleteLandingFragment();
                Intent newIntent = new Intent(currentActivity,MainActivity.class);
                currentActivity.startActivity(newIntent);
                currentActivity.finish();
                break;
            case R.id.NextStepButton:
                //Some validation steps will need to happen here - these have not been coded just
                // yet
                TextView cur = (TextView) currentActivity.findViewById(R.id
                        .RegisterUserToolbarTextView);
                cur.setText("Create Account - Step 2 of 2");
                CreateAccountStepTwoFragment stepTwoFragment = new CreateAccountStepTwoFragment();
                FragmentTransaction transaction = mgr.beginTransaction();
                transaction.replace(R.id.RegistrationContentArea, stepTwoFragment, "Step2");
                transaction.addToBackStack("Step2");
                transaction.commit();
                break;
        }
    }


}
