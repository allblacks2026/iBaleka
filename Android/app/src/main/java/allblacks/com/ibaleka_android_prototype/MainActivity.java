package allblacks.com.ibaleka_android_prototype;
import android.content.Intent;
import android.support.annotation.Nullable;
import android.support.design.widget.NavigationView;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;

import Fragments.ApplicationPreferencesFragment;
import Fragments.AthleteLandingFragment;
import Listeners.NavigationMenuOnItemSelectedListener;
import Utilities.iBalekaSingleton;

public class MainActivity extends AppCompatActivity implements View.OnClickListener{
    private Toolbar mainActivityToolbar;
    private TextView toolbarTextView;
    private ImageView toolbarImage;
    private FragmentManager mgr;
    private NavigationView navigationView;
    private DrawerLayout drawerLayout;
    private ActionBarDrawerToggle drawerToggle;
    private Menu navigationMenu;
    private ArrayList menuItems;

    protected void onCreate(Bundle savedInstanceState) {
        //If this activity was created - rotation of screen
        if (savedInstanceState == null) {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_main);
            initializeControls();
            loadLandingScreenFragment();
            iBalekaSingleton.setContext(this.getApplicationContext());
        } else {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_main);
            initializeControls();
        }
    }

    public void initializeControls()
    {
        mainActivityToolbar = (Toolbar) findViewById(R.id.MainActivityToolbar);
        toolbarTextView = (TextView) findViewById(R.id.MainActivityTextView);
        toolbarImage = (ImageView) findViewById(R.id.MainActivityImageView);
        drawerLayout = (DrawerLayout) findViewById(R.id.menuDrawerLayout);
        navigationView = (NavigationView) findViewById(R.id.mainActivityNavigationView);
        setSupportActionBar(mainActivityToolbar);
        getSupportActionBar().setTitle(null);
        getSupportActionBar().setHomeButtonEnabled(true);
        getSupportActionBar().setDefaultDisplayHomeAsUpEnabled(true);
        //Code to determine the type of login - switch menu according to the user
        drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, mainActivityToolbar, R.string
                .app_name, R
                .string
                .app_name);
        drawerLayout.setDrawerListener(drawerToggle);
        navigationView.inflateHeaderView(R.layout.navigation_menu_header);
        navigationView.inflateMenu(R.menu.athlete_navigation_menu);
        toolbarImage.setOnClickListener(this);

        NavigationMenuOnItemSelectedListener listener = new NavigationMenuOnItemSelectedListener
                (this);
        navigationView.setNavigationItemSelectedListener(listener);

        toolbarImage.setImageResource(R.drawable.ibaleka_logo);
        toolbarTextView.setText("Welcome");
        mgr = getFragmentManager();

    }
    public void loadLandingScreenFragment()
    {//This was a testing method - this should be determined on the button click
        AthleteLandingFragment landingFragment = new AthleteLandingFragment();
        FragmentTransaction transaction = mgr.beginTransaction();
        transaction.replace(R.id.MainActivityContentArea, landingFragment, "UserStats");
        transaction.addToBackStack("UserStats");
        transaction.commit();
    }
    @Override
    protected void onPause() {
        super.onPause();
    }
    protected void onResume() {
        super.onResume();
    }
    @Override
    public void onBackPressed() {
        if (mgr.getBackStackEntryCount() > 0) {
            mgr.popBackStack();
        } else {
            super.onBackPressed();
        }

    }
    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

    }
    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);
    }

    @Override
    protected void onPostCreate(@Nullable Bundle savedInstanceState) {
        super.onPostCreate(savedInstanceState);
        drawerToggle.syncState();
    }

    @Override
    public void onClick(View v) {
        drawerLayout.closeDrawers();
        navigationView.getMenu().clear();
        navigationView.inflateMenu(R.menu.athlete_navigation_menu);
        AthleteLandingFragment thisOne = new AthleteLandingFragment();
        FragmentTransaction transaction = mgr.beginTransaction();
        transaction.replace(R.id.MainActivityContentArea, thisOne, "HomeFragment");
        transaction.addToBackStack("HomeFragment");
        toolbarTextView.setText("Welcome");
        transaction.commit();
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.app_settings_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
           case (R.id.applicationPreferences):
               ApplicationPreferencesFragment preferencesFragment = new
                       ApplicationPreferencesFragment();
            FragmentTransaction preferencesTransaction = mgr.beginTransaction();
               preferencesTransaction.replace(R.id.MainActivityContentArea, preferencesFragment,
                       "PreferencesFragment");
               toolbarTextView.setText("Application Settings");
               preferencesTransaction.commit();
            break;
        }
        return true;
    }
}
