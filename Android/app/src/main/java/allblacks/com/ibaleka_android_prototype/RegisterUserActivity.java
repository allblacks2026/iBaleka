package allblacks.com.ibaleka_android_prototype;

import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuInflater;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;

import org.w3c.dom.Text;

import Fragments.CreateAccountStepOneFragment;
import Listeners.ButtonListener;

public class RegisterUserActivity extends AppCompatActivity {

    private CreateAccountStepOneFragment stepOneFragment;
    private Toolbar toolbar;
    private FrameLayout contentAreaFrameLayout;
    private TextView toolbarTextView;
    private ImageView toolbarImageView;

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.register_user_layout);

        toolbar = (Toolbar) findViewById(R.id.RegisterUserToolbar);
        toolbarTextView = (TextView) findViewById(R.id.RegisterUserToolbarTextView);
        toolbarImageView = (ImageView) findViewById(R.id.RegisterUserToolbarImage);
        contentAreaFrameLayout = (FrameLayout) findViewById(R.id.RegistrationContentArea);

        toolbarTextView.setText("Create Account - Step 1 of 2");
        toolbarImageView.setImageResource(R.drawable.ibaleka_logo);
        setSupportActionBar(toolbar);
        getSupportActionBar().setTitle(null);

        stepOneFragment = new CreateAccountStepOneFragment();
        FragmentManager manager = getSupportFragmentManager();
        FragmentTransaction transaction = manager.beginTransaction();
        transaction.replace(R.id.RegistrationContentArea, stepOneFragment, "Step1");
        transaction.addToBackStack("Step1");
        transaction.commit();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.app_settings_menu, menu);
        return true;
    }

    @Override
    public void onBackPressed() {

        FragmentManager mgr = getSupportFragmentManager();
        //To check if there are no fragments in the list - if there are fragments, the previous
        // fragment will be loaded
        //Glitch here with step 2 loading
        if (mgr.getBackStackEntryCount() != 0) {
            mgr.popBackStack();
        } else {
            finish();
        }
    }
}
