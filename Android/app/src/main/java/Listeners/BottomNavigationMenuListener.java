package Listeners;

import android.app.Activity;
import android.content.Context;
import android.support.annotation.IdRes;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.widget.TextView;

import com.aurelhubert.ahbottomnavigation.AHBottomNavigation;

import Fragments.MapRouteFragment;
import Fragments.ProfileFragment;
import Fragments.StartRunFragment;
import allblacks.com.ibaleka_android_prototype.R;

/**
 * Created by Okuhle on 4/9/2016.
 */
public class BottomNavigationMenuListener implements AHBottomNavigation.OnTabSelectedListener {

    private AppCompatActivity currentActivity;
    private FragmentManager fragmentManager;
    private TextView toolbarTextView;

    public BottomNavigationMenuListener(AppCompatActivity currentActivity) {
        this.currentActivity = currentActivity;
        fragmentManager = currentActivity.getSupportFragmentManager();
        toolbarTextView = (TextView) currentActivity.findViewById(R.id.MainActivityTextView);

    }

    @Override
    public void onTabSelected(int position, boolean wasSelected) {

        switch(position) {
            case 1:
                MapRouteFragment mapRouteFragment = new MapRouteFragment();
                FragmentTransaction secondTrans = fragmentManager.beginTransaction();
                secondTrans.replace(R.id.MainActivityContentArea, mapRouteFragment,
                        "MapRouteFragment");
                secondTrans.addToBackStack("MapRouteFragment");
                secondTrans.commit();
                toolbarTextView.setText("Map a Route");
                break;
            case 0:
                ProfileFragment profileFragment = new ProfileFragment();
                FragmentTransaction thirdTrans = fragmentManager.beginTransaction();
                thirdTrans.replace(R.id.MainActivityContentArea, profileFragment,
                        "ProfileFragment");
                thirdTrans.addToBackStack("ProfileFragment");
                toolbarTextView.setText("Athlete Profile Details");
                thirdTrans.commit();
                break;
            case 2:
                StartRunFragment startRunFragment = new StartRunFragment();
                FragmentTransaction fourthTrans = fragmentManager.beginTransaction();
                fourthTrans.replace(R.id.MainActivityContentArea, startRunFragment);
                toolbarTextView.setText("Start Running");
                fourthTrans.addToBackStack("StartRunFragment");
                fourthTrans.commit();
                break;
        }
    }
}
