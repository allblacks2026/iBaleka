package RetrofitClient;

import java.util.List;

<<<<<<< HEAD
=======
import Models.Athlete;
>>>>>>> 8be16b6dcc6c546a4e3487ad4a1ee759e9d2a362
import Models.User;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

/**
 * Created by Okuhle on 6/18/2016.
 */
public interface IUser {

    //Get the user's details based on supplied ID
    @GET("User/GetUser/{id}")
    Call<User> getUser(@Path("id") int userId);

    //Add a user's details to the system
    @POST("User/Add/user")
<<<<<<< HEAD
    Call<User> addUser(@Body User newUser);
=======
    Call<User> addUser(@Body User newUser, @Body Athlete newAthlete);
>>>>>>> 8be16b6dcc6c546a4e3487ad4a1ee759e9d2a362


}
