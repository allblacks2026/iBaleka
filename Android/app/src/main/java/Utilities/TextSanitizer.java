package Utilities;

import android.content.Context;

/**
 * Created by Okuhle on 5/16/2016.
 */
public class TextSanitizer {

   public TextSanitizer()
   {

   }

    public static boolean isValidText(String text, int minLength, int maxLength) {
        if (text.length() > maxLength  || text.length() < minLength) {
            return false;
        } else {
            return true;
        }
    }

    public static String sanitizeText(String text, boolean toUpper) {
        if (toUpper) {
            text = text.toUpperCase().trim();
            return text;
        } else {
            text = text.trim();
            return text;
        }
    }
}
