package Fragments;


import android.os.Bundle;
import android.app.Fragment;
import android.preference.PreferenceFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import allblacks.com.ibaleka_android_prototype.R;

/**
 * A simple {@link Fragment} subclass.
 */
public class ApplicationPreferencesFragment extends PreferenceFragment {

    public ApplicationPreferencesFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        addPreferencesFromResource(R.xml.application_preferences);
    }
}
