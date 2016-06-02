package allblacks.com.ibaleka_android_prototype;

import android.os.Bundle;
import android.preference.PreferenceActivity;
import android.preference.PreferenceFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import allblacks.com.ibaleka_android_prototype.R;

/**
 * Created by Okuhle on 4/17/2016.
 */
public class AppPreferencesActivity extends PreferenceActivity {

    public AppPreferencesActivity()
    {

    }
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        addPreferencesFromResource(R.xml.application_preferences);
    }
}
