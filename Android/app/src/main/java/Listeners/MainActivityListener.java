package Listeners;

import android.app.Activity;
import android.view.View;
import android.widget.CheckBox;
import android.widget.TextView;

import Utilities.TextSanitizer;
import allblacks.com.ibaleka_android_prototype.R;

/**
 * Created by Okuhle on 6/26/2016.
 */
public class MainActivityListener implements View.OnClickListener {

    private Activity currentActivity;
    private TextView mainActivityText;

    public MainActivityListener(Activity currentActivity) {
        this.currentActivity = currentActivity;
        mainActivityText = (TextView) currentActivity.findViewById(R.id.MainActivityTextView);
    }

    @Override
    public void onClick(View v) {
       switch (v.getId()) {
           case R.id.searchEvents:
               processSearch();
               break;

       }
    }

    private void processSearch()
    {
        TextView searchParams = (TextView) currentActivity.findViewById(R.id.SearchCriteriaEditText);
        CheckBox currentLocationCheckBox = (CheckBox) currentActivity.findViewById(R.id.SearchNearEventsCheckBox);
        CheckBox sortByDateCheckBox = (CheckBox) currentActivity.findViewById(R.id.SortByDateCheckBox);
        CheckBox currentCityCheckBox = (CheckBox) currentActivity.findViewById(R.id.CurrentCityEvents);

        String searchParameters = TextSanitizer.sanitizeText(searchParams.getText().toString().trim(), true);
        boolean searchCurrentLocation = currentLocationCheckBox.isChecked();
        boolean sortByDate = sortByDateCheckBox.isChecked();
        boolean searchCurrentCity = currentCityCheckBox.isChecked();

        if (searchParameters != null || searchParameters.length() != 0){


        }
    }
}
