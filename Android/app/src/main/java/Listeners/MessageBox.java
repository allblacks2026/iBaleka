package Listeners;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;

/**
 * Created by Okuhle on 5/1/2016.
 */
public class MessageBox {

    public Context currentContext;

    public MessageBox(Context currentContext) {
        this.currentContext = currentContext;
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

    public void displayMessage(String title, String message, DialogInterface.OnClickListener
            listener) {
        AlertDialog.Builder messageBox = new AlertDialog.Builder(currentContext);
        messageBox.setTitle(title);
        messageBox.setMessage(message);
        messageBox.setPositiveButton("Got It", listener);
        messageBox.show();
    }

}
